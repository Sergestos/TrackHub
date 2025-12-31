export interface ExerciseAggregation {
  aggregationId: string;
  type: string;
  userId: string;

  year: number;
  month: number;

  totalPlayed: number;

  warmupAggregation?: ByRecordTypeAggregation | null;
  songAggregation?: ByRecordTypeAggregation | null;
  improvisationAggregation?: ByRecordTypeAggregation | null;
  practicalExerciseAggregation?: ByRecordTypeAggregation | null;
  composingAggregation?: ByRecordTypeAggregation | null;

  rhythmAggregation?: ByPlayTypeAggregation | null;
  soloAggregation?: ByPlayTypeAggregation | null;
  bothAggregation?: ByPlayTypeAggregation | null;
}

export interface ByRecordTypeAggregation {
  recordTypeName: string;
  timesPlayed: number;
  totalPlayed: number;
}

export interface ByPlayTypeAggregation {
  playTypeName: string;
  timesPlayed: number;
  totalPlayed: number;
}