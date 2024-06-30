import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { RecordModel } from './commit.models';
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

	public recordModels: RecordModel[] = [];

	@ViewChildren(ExerciseComponent) exerciseViews!: QueryList<ExerciseComponent>;

	constructor(
		private commitService: CommitService,
		private activatedRoute: ActivatedRoute) {

		}

	public ngOnInit(): void {
		this.activatedRoute.queryParams.subscribe(params => {
			const exerciseId = params['exerciseId'];
			
			if (exerciseId) {
				this.commitService.getExerciseRecords(exerciseId).subscribe(items => {
					this.recordModels = items;
				})
			}
		  });
	}

	public onAddClick(): void {
		this.recordModels.push({
			recordType: 'Warmup',
			playType: 'Rhythm',
			isRecorded: false
		});
	}

	public onSaveClick(): void {
        this.commitService.saveExercise({ 
			playDate: this.isUseTodaysDate ? new Date() : this.selectedDate,
			records: this.exerciseViews.map(x => x.model)
		}).subscribe();
    }

	public onRemoveClick(): void {
		var exercisesToRemove = this.exerciseViews.filter(x => x.isSelected).map(x => x.model.id);
		this.recordModels = this.recordModels.filter(x => !exercisesToRemove.includes(x.id));
	}

	public onAllSelectedChanged(event: any): void {		
		this.exerciseViews.forEach(x => x.toggleIsSelected(event.target.checked));
	}
}
