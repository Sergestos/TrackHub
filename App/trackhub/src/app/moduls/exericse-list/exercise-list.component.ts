import { Component, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView } from "./exercise-list.models";

@Component({
	selector: 'trh-exercise-list',
	templateUrl: './exercise-list.component.html',
	styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit {
	public exercises: ExerciseItemView[] = [];

	public ngOnInit(): void {
		this.exercises = [
			{
				exerciseId: "1",
				totalPlayed: 10,
				playDate: new Date(),
				isExpanded: false
			},
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			},		
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			},
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			},
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			},
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			},
			{
				exerciseId: "1",
				totalPlayed: 60,
				playDate: new Date(),
				isExpanded: false
			}
		];
	}	

	public onCardExpand(exercise: ExerciseItemView): void {		
		exercise.isExpanded = !exercise.isExpanded;
	}
}