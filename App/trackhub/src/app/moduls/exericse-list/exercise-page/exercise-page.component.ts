import { Component, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView, FiltersModel as FilterModel } from "../exercise-list.models";
import { ActivatedRoute, Router } from "@angular/router";
import { ExerciseListService } from "../exercise-list.service";
import { MatDialog } from '@angular/material/dialog';
import { ModalResult, openDeleteConfirmationModal } from "../../../components/mat-modal/mat-modal.component";
import { LoadingService } from "../../../providers/services/loading.service";

@Component({
	selector: 'trackhub-exercise-page',
	templateUrl: './exercise-page.component.html',
	styleUrls: ['./exercise-page.component.css']
})
export class ExercisePageComponent implements OnInit {
	public exercises: ExerciseItemView[] = [];

	constructor(
		private router: Router,
		private matDialog: MatDialog,
		private activeRoute: ActivatedRoute,
		private exerciseListService: ExerciseListService,
        private loadingService: LoadingService
	) { }

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
				showNonPlayed: false,
				showExpanded: true
			});
		});
	}

	public onCardExpand(exercise: ExerciseItemView): void {
		exercise.isExpanded = !exercise.isExpanded;
	}

	public openDialog(item: ExerciseItemView): void {
		const modal = openDeleteConfirmationModal(this.matDialog);
		modal.afterClosed().subscribe((result: ModalResult) => {
			if (result == ModalResult.Confirmed) {
				this.exercises = this.exercises.filter(x => x != item);		
				this.loadingService.show();		
				this.exerciseListService
					.deleteExercise(item.exerciseId)
					.subscribe({ complete: () => this.loadingService.hide()});
			}
		});
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
		this.loadingService.show();
		this.exerciseListService.getExercisesByDate(filter.year, filter.month)		
			.subscribe({
				next: (result) => {
					this.exercises = result;
					this.exercises.forEach(x => {
						x.totalPlayed = x.records ? x.records.map(r => r.duration).reduce((sum, duration) => sum + duration, 0) : 0;
						x.isExpanded = filter.showExpanded;
					});
	
					this.fillNonPlayedDays(filter.year, filter.month, this.exercises, !filter.showNonPlayed!);
				},
				complete: () => this.loadingService.hide()
			});
	}

	private fillNonPlayedDays(year: number, month: number, items: ExerciseItem[], isHidden: boolean): void {
		for (let dayOfMonth = 1; dayOfMonth <= new Date(year, month, 0).getDate(); dayOfMonth++) {
			let dateToFill = new Date(year, month - 1, dayOfMonth);
			if (!items.some(x => new Date(x.playDate).getDate() == dateToFill.getDate())) {
				this.exercises.push({
					exerciseId: "-1",
					playDate: dateToFill,
					records: null,
					isHidden: isHidden
				});
			}
		}

		this.exercises.sort((a, b) => new Date(b.playDate) >= a.playDate ? -1 : 1);
	}
}