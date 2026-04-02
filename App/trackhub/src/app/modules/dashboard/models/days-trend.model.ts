export interface DaysTrendAggregation {
  id: string;
  build_date: string; 
  bars?: DayTrendBar[];
}

export interface DayTrendBar {
  play_date: string; 

  rhythm_aggregation: number;
  solo_aggregation: number;
  both_aggregation: number;

  warmup_aggregation?: number;
  song_aggregation?: number;
  improvisation_aggregation?: number;
  exercise_aggregation?: number;
  composing_aggregation?: number;
}