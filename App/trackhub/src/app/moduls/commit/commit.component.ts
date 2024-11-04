import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ExerciseModel } from './commit.models';
import { ExerciseComponent, RecordStatusType } from './exercise/exercise.component';
import { CommitService } from '../../providers/services/commit.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ModalResult, openDeleteConfirmationModal } from '../../components/mat-modal/mat-modal.component';
import { LoadingService } from '../../providers/services/loading.service';

@Component({
	selector: 'trh-commit',
	templateUrl: './commit.component.html',
	styleUrls: ['./commit.component.css']
})
export class CommitComponent implements OnInit {
	public isUseTodaysDate: boolean = true;
	public selectedDate: Date = new Date();

	public exercise?: ExerciseModel;

	public pageMode: "Add" | "Edit" = "Add";

	@ViewChildren(ExerciseComponent)
	private exerciseViews!: QueryList<ExerciseComponent>;

	public isAnySelected: boolean = false;

	constructor(
		private commitService: CommitService,
		private loadingService: LoadingService,
		private matDialog: MatDialog,
		private router: Router,
		private activatedRoute: ActivatedRoute) { }

	public ngOnInit(): void {
		this.activatedRoute.queryParams.subscribe(params => {
			const exerciseId = params['exerciseId'];

			if (exerciseId) {
		        this.loadingService.show();
				this.commitService.getExerciseRecords(exerciseId).subscribe({
					next: (response) => {
						this.exercise = response;
						this.pageMode = "Edit";
					},
					complete: () => this.loadingService.hide()
				});			
			} else {
				this.exercise = new ExerciseModel();
				this.pageMode = "Add";
			}
		});
	}

	public onAddClick(): void {
		this.exercise!.records!.push({
			recordType: 'Warmup',
			playType: 'Rhythm',
			isRecorded: false
		});
	}

	public onSaveClick(): void {		
		if (this.pageMode === "Add") {
			this.commitService.saveExercise({
				playDate: this.isUseTodaysDate ? new Date() : this.selectedDate,
				records: this.exerciseViews.map(x => x.model)
			}).subscribe(_ => window.location.reload());
		} else {
			this.commitService.updateExercise(this.exercise!)
				.subscribe(_ => window.location.reload());
		}
	}

	public onRemoveClick(): void {
		let isEntirtExericeToDelete: boolean = false;

		if (this.pageMode == 'Edit') {
			if (this.exerciseViews.length == this.exerciseViews.filter(x => x.isSelected).length) {
				isEntirtExericeToDelete = true;
				const modal = openDeleteConfirmationModal(this.matDialog);
				modal.afterClosed().subscribe(result => {
					if (result == ModalResult.Confirmed) {
						this.commitService
							.deleteExercise(this.exercise?.exerciseId?.toString()!)
							.subscribe(_ => this.router.navigateByUrl("/app/list"));
					}
				});		
			} else {
				const exerciseIdsToRemove = this.exerciseViews
					.filter(x => x.isSelected && x.currectRecordStatusType != RecordStatusType.draft)
					.map(x => x.model.recordId!)					
				if (exerciseIdsToRemove.length > 0) {
					this.loadingService.show();
					this.commitService
						.deleteRecords(this.exercise!.exerciseId!.toString(), exerciseIdsToRemove)		        
						.subscribe({ complete: () => this.loadingService.hide()});	
				}				
			}
		}		

		if (!isEntirtExericeToDelete) {
			const seletedExercises = this.exerciseViews
				.filter(x => x.isSelected)
				.map(x => x.model);
			for (let index = 0; index < seletedExercises.length; index++) {					
				this.exercise!.records = this.exercise?.records.filter(x => x !== seletedExercises[index])!;
			}			
		}		
	}

	public onSelectToggle(): void {
		this.isAnySelected  = this.exerciseViews.some(x => x.isSelected === true);			
	}

	public onAllSelectedChanged(event: any): void {
		this.exerciseViews.forEach(x => x.toggleIsSelected(event.target.checked));
	}
}
