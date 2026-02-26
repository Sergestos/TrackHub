import { Component, OnInit, inject } from '@angular/core';
import { ExercisePreviewComponent } from './exercise-preview/exercise-preview.component';
import { CommitContainerExerciseService } from '../services/commit-container.service';
import { Exercise } from '../../../models/exercise';

@Component({
  selector: 'trh-commit-container',
  templateUrl: './commit-container.component.html',
  imports: [ExercisePreviewComponent],
  providers: [CommitContainerExerciseService],
})
export class CommitContainerComponent implements OnInit {
  protected recentExercises: Exercise[] = [];

  private readonly exerciseService = inject(CommitContainerExerciseService);

  public ngOnInit(): void {
    this.exerciseService.getLastUserExercises().subscribe({
      next: (exercises) => {
        this.recentExercises = exercises;
        // const temp = new Exercise();
        // temp.exerciseId = 1;
        // temp.records = [];
        // temp.playDate = new Date();
        // this.recentExercises = [temp];
      },
    });
  }
}
