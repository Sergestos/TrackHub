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
        //this.recentExercises = exercises;

        this.recentExercises = [
          {
            exerciseId: 1,
            playDate: new Date('2026-02-25'),
            records: [
              {
                recordId: '1-1',
                name: 'Warmup Beat',
                author: 'Trainer A',
                recordType: 1,
                playDuration: 30,
                bitsPerMinute: 120,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song A1', 'Song A2'],
              },
              {
                recordId: '1-2',
                name: 'Cardio Blast',
                author: 'Trainer A',
                recordType: 2,
                playDuration: 60,
                bitsPerMinute: 140,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song A3'],
              },
              {
                recordId: '1-3',
                name: 'Cooldown Flow',
                author: 'Trainer A',
                recordType: 3,
                playDuration: 24,
                bitsPerMinute: 90,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 2,
            playDate: new Date('2026-02-26'),
            records: [
              {
                recordId: '2-1',
                name: 'Morning Stretch',
                author: 'Trainer B',
                recordType: 1,
                playDuration: 40,
                bitsPerMinute: 100,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song B1'],
              },
              {
                recordId: '2-2',
                name: 'HIIT Session',
                author: 'Trainer B',
                recordType: 2,
                playDuration: 80,
                bitsPerMinute: 160,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song B2', 'Song B3'],
              },
              {
                recordId: '2-3',
                name: 'Evening Relax',
                author: 'Trainer B',
                recordType: 3,
                playDuration: 30,
                bitsPerMinute: 85,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 3,
            playDate: new Date('2026-02-27'),
            records: [
              {
                recordId: '3-1',
                name: 'Power Start',
                author: 'Trainer C',
                recordType: 1,
                playDuration: 35,
                bitsPerMinute: 130,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song C1'],
              },
              {
                recordId: '3-2',
                name: 'Strength Circuit',
                author: 'Trainer C',
                recordType: 2,
                playDuration: 90,
                bitsPerMinute: 110,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song C2', 'Song C3'],
              },
              {
                recordId: '3-3',
                name: 'Deep Stretch',
                author: 'Trainer C',
                recordType: 3,
                playDuration: 28,
                bitsPerMinute: 75,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 3,
            playDate: new Date('2026-02-27'),
            records: [
              {
                recordId: '3-1',
                name: 'Power Start',
                author: 'Trainer C',
                recordType: 1,
                playDuration: 35,
                bitsPerMinute: 130,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song C1'],
              },
              {
                recordId: '3-2',
                name: 'Strength Circuit',
                author: 'Trainer C',
                recordType: 2,
                playDuration: 90,
                bitsPerMinute: 110,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song C2', 'Song C3'],
              },
              {
                recordId: '3-3',
                name: 'Deep Stretch',
                author: 'Trainer C',
                recordType: 3,
                playDuration: 28,
                bitsPerMinute: 75,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 3,
            playDate: new Date('2026-02-27'),
            records: [
              {
                recordId: '3-1',
                name: 'Power Start',
                author: 'Trainer C',
                recordType: 1,
                playDuration: 35,
                bitsPerMinute: 130,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song C1'],
              },
              {
                recordId: '3-2',
                name: 'Strength Circuit',
                author: 'Trainer C',
                recordType: 2,
                playDuration: 90,
                bitsPerMinute: 110,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song C2', 'Song C3'],
              },
              {
                recordId: '3-3',
                name: 'Deep Stretch',
                author: 'Trainer C',
                recordType: 3,
                playDuration: 28,
                bitsPerMinute: 75,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 3,
            playDate: new Date('2026-02-27'),
            records: [
              {
                recordId: '3-1',
                name: 'Power Start',
                author: 'Trainer C',
                recordType: 1,
                playDuration: 35,
                bitsPerMinute: 130,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song C1'],
              },
              {
                recordId: '3-2',
                name: 'Strength Circuit',
                author: 'Trainer C',
                recordType: 2,
                playDuration: 90,
                bitsPerMinute: 110,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song C2', 'Song C3'],
              },
              {
                recordId: '3-3',
                name: 'Deep Stretch',
                author: 'Trainer C',
                recordType: 3,
                playDuration: 28,
                bitsPerMinute: 75,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
          {
            exerciseId: 3,
            playDate: new Date('2026-02-27'),
            records: [
              {
                recordId: '3-1',
                name: 'Power Start',
                author: 'Trainer C',
                recordType: 1,
                playDuration: 35,
                bitsPerMinute: 130,
                playType: 1,
                isRecorded: true,
                warmupSongs: ['Song C1'],
              },
              {
                recordId: '3-2',
                name: 'Strength Circuit',
                author: 'Trainer C',
                recordType: 2,
                playDuration: 90,
                bitsPerMinute: 110,
                playType: 2,
                isRecorded: true,
                warmupSongs: ['Song C2', 'Song C3'],
              },
              {
                recordId: '3-3',
                name: 'Deep Stretch',
                author: 'Trainer C',
                recordType: 3,
                playDuration: 28,
                bitsPerMinute: 75,
                playType: 3,
                isRecorded: false,
                warmupSongs: [],
              },
            ],
          },
        ];
        // const temp = new Exercise();
        // temp.exerciseId = 1;
        // temp.records = [];
        // temp.playDate = new Date();
        // this.recentExercises = [temp];
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
