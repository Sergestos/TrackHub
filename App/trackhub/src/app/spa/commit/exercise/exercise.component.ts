import { Component, Input } from '@angular/core';
import { RecordModel } from '../commit.models';
import { Subject } from 'rxjs';

@Component({
    selector: 'trh-exercise',
    templateUrl: './exercise.component.html',
    styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent {
    @Input()
    public model!: RecordModel;
    public isSelected: boolean = false;
    public isSuggestionsAsked: boolean = false;

    public songSuggestions: string[] = [];
    public currentSongSelected!: string; 

    public authorSuggestions: string[] = [];
    public currentAuthorSelected!: string;

    public suggestions$: Subject<string[]> = new Subject<string[]>();

    constructor() {

    } 

    public toggleIsSelected(value: boolean): void {
        this.isSelected = value;
    }

    public onSongInput(searchValue: string): void {  
        console.log(searchValue);
    }

    public onSongInputClick(event: any): void {
        if ((this.model.type == 'Song' || this.model.type == 'Warmup') && this.songSuggestions.length > 0) {
            this.isSuggestionsAsked = true;
        }
    }
}
