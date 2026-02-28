import { Component, input, output } from '@angular/core';
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

  public exercise = input.required<Exercise>();

  public applyTemplate = output<Exercise>();
  public applyRecord = output<ExerciseRecord>();

  public getRecordTypeField(record: ExerciseRecord): string {
    return RecordTypes[record.recordType!];
  }

  public getNameField(record: ExerciseRecord): string {
    if (record.recordType == RecordTypes.Warmup)
      return record.warmupSongs?.join(', ')!;

    return record.name!;
  }

  public onRecordClicked(item: ExerciseRecord): void {
    this.applyRecord.emit(item);
  }

  public onTemplateClicked(): void {
    this.applyTemplate.emit(this.exercise()!);
  }
}
