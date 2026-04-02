export interface DaysTrendAggregation {
  aggregationId: string;
  buildDate: string;
  daysTrendBarList?: DayTrendBar[];
}

export interface DayTrendBar {
  playDate: string;

  totalPlayedRhythmDuration: number;
  totalPlayedSoloDuration: number;
  totalPlayedBothDuration: number;

  totalWarmupDuration?: number;
  totalSongDuration?: number;
  totalImprovisationDuration?: number;
  totalPracticalExerciseDuration?: number;
  totalComposingDuration?: number;
}