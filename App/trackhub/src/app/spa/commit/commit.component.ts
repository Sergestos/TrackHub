import { Component, QueryList, ViewChildren } from '@angular/core';
import { ExerciseModel } from './commit.models';
import { ExerciseComponent } from './exercise/exercise.component';

@Component({
	selector: 'trh-commit',
	templateUrl: './commit.component.html',
	styleUrls: ['./commit.component.css']
})
export class CommitComponent {	
	public isUseTodaysDate: boolean = true;
	public selectedDate: Date = new Date();

	public exercises: ExerciseModel[] = [];

	@ViewChildren(ExerciseComponent) exerciseViews!: QueryList<ExerciseComponent>;

	//TODO remove 
	private tempCounter = 1;

	public onAddClick(): void {
		this.exercises.push({
			id: this.tempCounter++,
			song: 'random'
		});
	}

	public onRemoveClick(): void {
		var exercisesToRemove = this.exerciseViews.filter(x => x.isSelected).map(x => x.model.id);
		this.exercises = this.exercises.filter(x => !exercisesToRemove.includes(x.id));
	}

	public onAllSelectedChanged(event: any): void {		
		this.exerciseViews.forEach(x => x.toggleIsSelected(event.target.checked));
	}
}
