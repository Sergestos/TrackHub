import { Component, QueryList, ViewChildren } from '@angular/core';
import { RecordModel } from './commit.models';
import { ExerciseComponent } from './exercise/exercise.component';

@Component({
	selector: 'trh-commit',
	templateUrl: './commit.component.html',
	styleUrls: ['./commit.component.css']
})
export class CommitComponent {	
	public isUseTodaysDate: boolean = true;
	public selectedDate: Date = new Date();

	public exercises: RecordModel[] = [];

	@ViewChildren(ExerciseComponent) exerciseViews!: QueryList<ExerciseComponent>;

	public onAddClick(): void {
		this.exercises.push({
			recordType: 'Warmup',
			playType: 'Rhythm',
			isRecorded: false
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
