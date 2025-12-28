
export interface ExerciseAggregation {
  id: string;
  user_id: string;
  aggregation_year: number;
  aggregation_month: number;
  total_played: number;

  warmup_aggregation?: ByRecordTypeAggregation | null;
  song_aggregation?: ByRecordTypeAggregation | null;
  improvisation_aggregation?: ByRecordTypeAggregation | null;
  exercise_aggregation?: ByRecordTypeAggregation | null;
  composing_aggregation?: ByRecordTypeAggregation | null;

  rhythm_aggregation?: ByPlayTypeAggregation | null;
  solo_aggregation?: ByPlayTypeAggregation | null;
  both_aggregation?: ByPlayTypeAggregation | null;
}

export interface ByRecordTypeAggregation {
  record_type_name: string;
  times_played: number;
  total_played: number;
}

export interface ByPlayTypeAggregation {
  play_type_name: string;
  times_played: number;
  total_played: number;
}
