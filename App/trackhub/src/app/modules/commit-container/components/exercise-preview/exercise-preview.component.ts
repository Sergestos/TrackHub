import { Component, input } from '@angular/core';
import { Exercise } from '../../../../models/exercise';
import { DatePipe } from '@angular/common';
import { RecordTypes } from '../../../../models/recordy-types-enum';
import { ExerciseRecord } from '../../../../models/exercise-record';

@Component({
  selector: 'trh-commit-container-preview[exercise]',
  templateUrl: './exercise-preview.component.html',
  styleUrl: './exercise-preview.component.scss',
  imports: [DatePipe],
})
export class ExercisePreviewComponent {
  readonly RecordTypes = RecordTypes;

  public exercise = input<Exercise>();

  public getRecordTypeField(record: ExerciseRecord): string {
    return RecordTypes[record.recordType!];
  }

  public getNameField(record: ExerciseRecord): string {
    if (record.recordType == RecordTypes.Warmup)
      return record.warmupSongs?.join(', ')!;

    return record.name!;
  }
}
