export class SongAggregation {
  aggregationId!: string;
  type!: string;
  userId!: string;

  totalPlayed: number = 0;
  timesPlayed: number = 0;

  author!: string;
  name!: string;

  songsByDateAggregations: SongsByDateAggregation[] | null = null;
}

export class SongsByDateAggregation {
  year: number = 0;
  month: number = 0;

  timesPlayed: number = 0;
  totalDuration: number = 0;
}
