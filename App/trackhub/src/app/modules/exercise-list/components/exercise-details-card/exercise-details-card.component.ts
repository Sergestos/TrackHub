import { Component, input } from '@angular/core';
import { RecordTypes } from '../../../../models/recordy-types-enum';
import { RecordDetailsItem } from '../../models/exercise-list.models';

@Component({
  selector: 'trh-details-exercise-card',
  templateUrl: './exercise-details-card.component.html',
  styles: `
    p {
      @apply text-c-text-grid;
    }
  `,
  standalone: false,
})
export class DetailsExerciseItemComponent {
  readonly RecordTypes = RecordTypes;

  public exerciseId = input.required<string>();
  public exerciseDetailsModels = input<RecordDetailsItem[] | null>();

  public getRecordTypeField(record: RecordDetailsItem): string {
    return RecordTypes[record.recordType];
  }

  public getNameField(record: RecordDetailsItem): string {
    if (record.recordType == RecordTypes.Warmup)
      return record.warmupSongs?.join(', ')!;

    return record.name!;
  }
}
