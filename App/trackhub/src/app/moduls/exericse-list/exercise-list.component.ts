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
		this.setExerciseGrid({
			year: 2024,
			month: 6,
			showNonPlayed: true
		});
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
		this.setExerciseGrid(filter);
	}

	public onShowNonPlayedChanged(isExpandAsksed: boolean): void {
		this.exercises.forEach(x => x.isHidden = isExpandAsksed);
	}

	public onExpandChanged(isExpandAsksed: boolean): void {
		this.exercises
			.filter(x => x.exerciseId != '-1')
			.forEach(x => x.isExpanded = isExpandAsksed);
	}

	private setExerciseGrid(filter: FilterModel): void {
		this.exerciseListService.getFilteredExercises(filter.year, filter.month)
			.subscribe(result => {				
				this.exercises = result;	
				if (filter.showNonPlayed) {
					this.fillNonPlayedDays(filter.year, filter.month, this.exercises);			
				}

				this.exercises.sort((a, b) => a.playDate >= b.playDate ? 1 : -1);

				if (filter.showExpanded) {
					// this.exercises.filter(x => x.exerciseId != '-1')
				}
			})
	}

	private fillNonPlayedDays(year: number, month: number, items: ExerciseItem[]): void {
		for (let dayOfMonth = 1; dayOfMonth <= new Date(year, month, 0).getDate(); dayOfMonth++) {
			let dateToFill = new Date(year, month - 1, dayOfMonth);
			if (!items.some(x => x.playDate.getDate() == dateToFill.getDate())) {
				this.exercises.push({
					exerciseId: "-1",
					totalPlayed: 0,
					playDate: dateToFill					
				});
			}
		}
	}
}