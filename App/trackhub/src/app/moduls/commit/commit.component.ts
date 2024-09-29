import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ExerciseModel, RecordModel } from './commit.models';
import { ExerciseComponent } from './exercise/exercise.component';
import { CommitService } from '../../providers/services/commit.service';
import { ActivatedRoute } from '@angular/router';

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

	@ViewChildren(ExerciseComponent) exerciseViews!: QueryList<ExerciseComponent>;

	constructor(
		private commitService: CommitService,
		private activatedRoute: ActivatedRoute) { }

	public ngOnInit(): void {
		this.activatedRoute.queryParams.subscribe(params => {
			const exerciseId = params['exerciseId'];

			if (exerciseId) {
				this.commitService.getExerciseRecords(exerciseId).subscribe(response => {
					this.exercise = response;
					this.pageMode = "Edit";
				})
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
		var exercisesToRemove = this.exerciseViews.filter(x => x.isSelected).map(x => x.model.recordId);
		this.exercise!.records = this.exercise!.records!.filter(x => !exercisesToRemove.includes(x.recordId));
	}

	public onAllSelectedChanged(event: any): void {
		this.exerciseViews.forEach(x => x.toggleIsSelected(event.target.checked));
	}
}
