import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ExerciseModel } from '../commit.models';

@Component({
    selector: 'trh-exercise',
    templateUrl: './exercise.component.html',
    styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent {

    @Input()
    public model!: ExerciseModel;
    public isSelected: boolean = false;

    constructor() {

    } 

    public toggleIsSelected(value: boolean): void {
        this.isSelected = value;
    }
}
