import { Instruments } from '../../../models/instruments';
import { PlayTypes } from '../../../models/play-types-enum';
import { RecordTypes } from '../../../models/recordy-types-enum';

export class PreviewState {
  isValid!: boolean;
  playDate?: Date;
  records?: PreviewRecord[];
  validationIssues?: ValidationIssue[];
}

export class PreviewRecord {
  recordType?: RecordTypes;
  instrument?: Instruments;
  playType?: PlayTypes;
  playDuration?: number;
  name?: string;
  author?: string;
  bitsPerMinute?: number;
  isRecorded?: boolean;
  warmupSongs?: string[];
}

export class ValidationIssue {
  fieldName!: string;
  lineNumber!: number;
  errorReason?: string;
}
