export interface ExerciseRecord {
  recordId?: string;
  name?: string | null;
  author?: string | null;
  recordType?: number;
  playDuration?: number;
  bitsPerMinute?: number;
  playType?: number;
  isRecorded?: boolean;
  warmupSongs?: string[];
}
