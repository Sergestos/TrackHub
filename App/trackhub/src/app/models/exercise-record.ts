export interface ExerciseRecord {
  recordId?: string;
  name?: string | null;
  author?: string | null;
  recordType?: string;
  playDuration?: number;
  bitsPerMinute?: number;
  playType?: string;
  isRecorded?: boolean;
}
