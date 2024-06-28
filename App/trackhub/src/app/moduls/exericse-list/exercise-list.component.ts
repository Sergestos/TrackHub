import { Component, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView } from "./exercise-list.models";
import { Router } from "@angular/router";

@Component({
	selector: 'trh-exercise-list',
	templateUrl: './exercise-list.component.html',
	styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit {
	public exercises: ExerciseItemView[] = [];
 
	constructor(private router: Router) {
		
	}

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

	public onRemoveItem(item: ExerciseItemView): void {
		this.exercises = this.exercises.filter(x => x != item);
	}

	public onExerciseEdit(item: ExerciseItemView): void {
		this.router.navigateByUrl("/app/commit?exerciseId=" + item.exerciseId);
	}
}