import { Component, ElementRef, HostListener, Input, OnInit } from '@angular/core';
import { RecordModel } from '../commit.models';
import { debounceTime } from 'rxjs';
import { FormControl } from '@angular/forms';
import { CommitService } from '../../../providers/services/commit.service';

const MinSearchLength: number = 3;

enum SuggestionType {
    song,
    author
}

enum RecordStatusType {
    changed,
    saved,
    draft
}

@Component({
    selector: 'trh-exercise',
    templateUrl: './exercise.component.html',
    styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent implements OnInit {
    public recordTypes: string[] = [ 'Warmup', 'Song', 'Improvisation', 'Exercise', 'Composition' ]
    public playTypes: string[] = [ 'Rhythm', 'Solo', 'Both' ]

    @Input()
    public model!: RecordModel;
    public initialModel?: RecordModel;

    public isSelected: boolean = false;
    public isSuggestionsAsked: boolean = false;

    public searchSongField = new FormControl();
    public searchAuthorField = new FormControl();

    public songSuggestions: string[] = [];
    public authorSuggestions: String[] = [];

    suggestionTypeEnum: typeof SuggestionType = SuggestionType;
    public currentSuggestionsType: SuggestionType | null = null;

    recordStatusTypeEnum: typeof RecordStatusType = RecordStatusType;
    public currectRecordStatusType: RecordStatusType | null = null;

    constructor(private commitService: CommitService, private eRef: ElementRef) { }
    
    @HostListener('document:click', ['$event'])
    public clickout(event: any) {
        if(!this.eRef.nativeElement.contains(event.target)) {
            this.isSuggestionsAsked = false;
        }
    }

    public ngOnInit(): void {
        this.initialModel = structuredClone(this.model);

        if (this.model.id) {
            this.currectRecordStatusType = RecordStatusType.saved;
        } else {
            this.currectRecordStatusType = RecordStatusType.draft;
        }

        this.searchSongField.valueChanges
            .pipe(debounceTime(300))
            .subscribe(input => {
                if (input && input.length >= MinSearchLength) {
                    this.commitService.getSongSuggestrions(input)
                        .subscribe(result => {
                            this.songSuggestions = result;
                            this.displaySuggestions(SuggestionType.song)
                        });
                }                
            });

        this.searchAuthorField.valueChanges
            .pipe(debounceTime(300))
            .subscribe(input => {
                if (input && input.length >= MinSearchLength) {
                    this.commitService.getAuthorSuggestrions(input)
                        .subscribe(result => {
                            this.authorSuggestions = result;
                            this.displaySuggestions(SuggestionType.author)
                        });
                }                
            });
    }

    public toggleIsSelected(value: boolean): void {
        this.isSelected = value;
    }

    public onSongInputClick(event: any): void {
        this.displaySuggestions(SuggestionType.song);
    }

    public onAuthorInputClick(event: any): void {
        this.displaySuggestions(SuggestionType.author);
    }

    public onSuggestionClick(value: string) {
        if (this.currentSuggestionsType == SuggestionType.song) {
            this.model.name = value;
        }
        if (this.currentSuggestionsType == SuggestionType.author) {
            this.model.author = value;
        }
    }

    public onModelChanged(): void {
        if (JSON.stringify(this.model) !== JSON.stringify(this.initialModel) && this.model.id) {
            this.currectRecordStatusType = RecordStatusType.changed;
        }
    }

    private displaySuggestions(suggestionType: SuggestionType): void {
        this.currentSuggestionsType = suggestionType;
        this.isSuggestionsAsked = 
            (this.currentSuggestionsType == SuggestionType.song && this.songSuggestions.length > 0) ||
            (this.currentSuggestionsType == SuggestionType.author && this.authorSuggestions.length > 0);
    }
}
