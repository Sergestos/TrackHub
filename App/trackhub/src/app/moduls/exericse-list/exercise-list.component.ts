import { Component, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView, FiltersModel as FilterModel } from "./exercise-list.models";
import { ActivatedRoute, Router } from "@angular/router";
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
		private activeRoute: ActivatedRoute,
		private exerciseListService: ExerciseListService) {
		
	}

	public ngOnInit(): void {		
		this.activeRoute.queryParams.subscribe(params => {
			let year: number;
			let month: number;

			const currentDate = new Date();
			year = params['year'] || currentDate.getFullYear();
			month = params['month'] || currentDate.getMonth() + 1;			

			this.setExerciseGrid({
				year: year,
				month: month,
				showNonPlayed: true
			});
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
		this.exercises.filter(x => x.exerciseId == "-1").forEach(x => x.isHidden = !isExpandAsksed);
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
				this.exercises.forEach(x => {
					x.totalPlayed = x.records ? x.records.map(r => r.duration).reduce((sum, duration) => sum + duration, 0) : 0;
				});

				this.fillNonPlayedDays(filter.year, filter.month, this.exercises);							

				if (filter.showExpanded) {
					// this.exercises.filter(x => x.exerciseId != '-1')
				}
			})
	}

	private fillNonPlayedDays(year: number, month: number, items: ExerciseItem[]): void {
		for (let dayOfMonth = 1; dayOfMonth <= new Date(year, month, 0).getDate(); dayOfMonth++) {
			let dateToFill = new Date(year, month - 1, dayOfMonth);
			if (!items.some(x => new Date(x.playDate).getDate() == dateToFill.getDate())) {
				this.exercises.push({
					exerciseId: "-1",
					playDate: dateToFill,
					records: null,
					isHidden: false
				});
			}
		}

		this.exercises.sort((a, b) => new Date(b.playDate) >= a.playDate ? -1 : 1);
	}
}