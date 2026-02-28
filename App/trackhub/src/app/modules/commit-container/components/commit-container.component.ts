import { Component, OnInit, inject, output } from '@angular/core';
import { ExercisePreviewComponent } from './exercise-preview/exercise-preview.component';
import { CommitContainerExerciseService } from '../services/commit-container.service';
import { Exercise } from '../../../models/exercise';
import { ExerciseRecord } from '../../../models/exercise-record';

@Component({
  selector: 'trh-commit-container',
  templateUrl: './commit-container.component.html',
  styles: `
    :host {
    display: block;
    height: 100%;
    min-height: 0;
  }`,
  imports: [ExercisePreviewComponent],
  providers: [CommitContainerExerciseService],
})
export class CommitContainerComponent implements OnInit {
  protected recentExercises: Exercise[] = [];

  private readonly exerciseService = inject(CommitContainerExerciseService);

  public applyTemplate = output<Exercise>();
  public applyRecord = output<ExerciseRecord>();

  public ngOnInit(): void {
    this.exerciseService.getLastUserExercises().subscribe({
      next: (exercises) => {
        exercises.forEach((x) => {
          x.exerciseId = undefined;
          x.records.forEach((r) => (r.recordId = undefined));
        });

        this.recentExercises = exercises;
      },
    });
  }

  public onTemplateApplied(exercise: Exercise): void {
    this.applyTemplate.emit(exercise);
  }

  public onRecordApplied(record: ExerciseRecord): void {
    this.applyRecord.emit(record);
  }
}
