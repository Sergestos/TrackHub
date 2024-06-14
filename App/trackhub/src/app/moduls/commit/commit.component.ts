import { Component, QueryList, ViewChildren } from '@angular/core';
import { RecordModel } from './commit.models';
import { ExerciseComponent } from './exercise/exercise.component';
import { CommitService } from './commit.service';

@Component({
	selector: 'trh-commit',
	templateUrl: './commit.component.html',
	styleUrls: ['./commit.component.css']
})
export class CommitComponent {	
	public isUseTodaysDate: boolean = true;
	public selectedDate: Date = new Date();

	public recordModels: RecordModel[] = [];

	@ViewChildren(ExerciseComponent) exerciseViews!: QueryList<ExerciseComponent>;

	constructor(private commitService: CommitService) { }

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
