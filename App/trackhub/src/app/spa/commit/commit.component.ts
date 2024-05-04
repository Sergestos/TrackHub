import { Component } from '@angular/core';
import { ExerciseModel } from './commit.models';

@Component({
	selector: 'trh-commit',
	templateUrl: './commit.component.html',
	styleUrls: ['./commit.component.css']
})
export class CommitComponent {
	public totalEcounter: number = 0;
	public selectedDate: Date = new Date();

	public exercises: ExerciseModel[] = 
	[
		{
			id: null,
			song: 'random'
		}
	];

	public onAddClick(): void {
		this.exercises.push({
			id: null,
			song: 'random'
		});
	}
}
