import { Component, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView, FiltersModel as FilterModel } from "./exercise-list.models";
import { Router } from "@angular/router";
import { ExerciseListService } from "../../providers/services/exercise-list.service";

@Component({
	selector: 'trh-exercise-list',
	templateUrl: './exercise-list.component.html',
	styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit {
	public exercises: ExerciseItemView[] = [];
 
	constructor(
		private router: Router,
		private exerciseListService: ExerciseListService) {
		
	}

	public ngOnInit(): void {
		for (let i = 0; i < 30; i++) {
			this.exercises.push(
				{
					exerciseId: i.toString(),
					totalPlayed: 20,
					playDate: new Date(),
					isExpanded: false
				}
			)
		}
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

	public onDateChanged(filter: FilterModel): void {
		this.exerciseListService.getMonthExercises(filter.year, filter.month)
			.subscribe(result => {				
				this.exercises = result;				
			})
	}
}