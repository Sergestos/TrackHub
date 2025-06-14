import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { RecordModel, SuggestionResult } from '../commit.models';
import { debounceTime } from 'rxjs';
import { FormControl } from '@angular/forms';
import { CommitService } from '../commit.service';

const MinSearchLength: number = 3;

enum SuggestionType {
    song,
    author
}

export enum RecordStatusType {
    changed,
    saved,
    draft
}

@Component({
    selector: 'trh-exercise',
    templateUrl: './exercise.component.html',
    styleUrls: ['./exercise.component.scss'],
    standalone: false
})
export class ExerciseComponent implements OnInit {
    public recordTypes: string[] = [ 'Warmup', 'Song', 'Improvisation', 'Exercise', 'Composing' ]
    public playTypes: string[] = [ 'Rhythm', 'Solo', 'Both' ]

    @Input()
    public model!: RecordModel;    
    public initialModel?: RecordModel;

    @Output()
    public onSelectToggle: EventEmitter<void> = new EventEmitter<void>();

    public isSelected: boolean = false;
    public isSuggestionsAsked: boolean = false;

    public searchSongField = new FormControl();
    public searchAuthorField = new FormControl();

    public songSuggestions: SuggestionResult[] = [];
    public authorSuggestions: SuggestionResult[] = [];

    suggestionTypeEnum: typeof SuggestionType = SuggestionType;
    public currentSuggestionsType: SuggestionType | null = null;

    recordStatusTypeEnum: typeof RecordStatusType = RecordStatusType;
    public currectRecordStatusType: RecordStatusType | null = null;

    constructor(
        private commitService: CommitService, 
        private eRef: ElementRef) { }
    
    @HostListener('document:click', ['$event'])
    public clickout(event: any) {
        if(!this.eRef.nativeElement.contains(event.target)) {
            this.isSuggestionsAsked = false;
        }
    }

    public ngOnInit(): void {
        this.initialModel = structuredClone(this.model);

        if (this.model.recordId) {
            this.currectRecordStatusType = RecordStatusType.saved;
        } else {
            this.currectRecordStatusType = RecordStatusType.draft;
        }

        setTimeout(() => {
            this.searchSongField.valueChanges
            .pipe(debounceTime(300))
            .subscribe(input => {
                if (input && input.length >= MinSearchLength) {
                    this.commitService.getSongSuggestrions(input, this.model.author)
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
                        .subscribe((result: SuggestionResult[]) => {
                            this.authorSuggestions = result;
                            this.displaySuggestions(SuggestionType.author)
                        });
                }                
            });
        }, 0);                
    }   

    public toggleIsSelected(value: boolean): void {
        this.isSelected = value;
        this.onSelectToggle.emit();
    }    

    public onIsSelectedChanged($event: any): void  {
        this.onSelectToggle.emit();
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

    public onModelChanged(fieldName?: string): void {
        if (fieldName) {
            this.trimFields(fieldName!);
        }

        if (JSON.stringify(this.model) !== JSON.stringify(this.initialModel) && this.model.recordId) {
            this.currectRecordStatusType = RecordStatusType.changed;
        }
    }

    private displaySuggestions(suggestionType: SuggestionType): void {
        this.currentSuggestionsType = suggestionType;
        this.isSuggestionsAsked = 
            (this.currentSuggestionsType == SuggestionType.song && this.songSuggestions.length > 0) ||
            (this.currentSuggestionsType == SuggestionType.author && this.authorSuggestions.length > 0);
    }

    private trimFields(fieldName: string): void {
        if (fieldName == 'name')
            this.model.name = this.model.name?.trim();
        if (fieldName == 'author')
            this.model.author = this.model.author?.trim();
    }
}
