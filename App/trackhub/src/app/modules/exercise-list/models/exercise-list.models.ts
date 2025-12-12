import { RecordTypes } from "../../../models/recordy-types-enum";

export class ExerciseItem {
  public exerciseId!: string;
  public records: RecordDetailsItem[] | null = null;
  public playDate!: Date;
}

export class ExerciseItemView extends ExerciseItem {
  public isExpanded?: boolean = false;
  public isHidden?: boolean = false;
  public IsPlayedDay?: boolean = true;
  public totalPlayed?: number = 0;
}

export class RecordDetailsItem {
  public recordType!: RecordTypes;
  public duration!: number;
  public name?: string;
  public author!: string;
  public warmupSongs?: string[];
}

export class FilterModel {
  public dateFilter?: FilterDateModel;
  public showPlayedOnly?: boolean;
  public showExpanded?: boolean;
}

export class FilterDateModel {
  public year?: number;
  public month?: number;
}