import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable, of } from "rxjs";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { ApiService } from "../../../providers/services/api.service";
import { SongAggregation } from "../models/song-aggregation.model";

@Injectable()
export class AggregationService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/aggregations';

  private apiService = inject(ApiService);

  public getMonthAggregation(date: Date): Observable<ExerciseAggregation> {
    return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + `?date=${date.toDateString()}`);
  }

  public getMonthRangeAggregation(startDate: Date, endDate: Date): Observable<ExerciseAggregation[]> {
    const query = `?startDate=${startDate.toDateString()}&endDate=${endDate.toDateString()}`
    return this.apiService.get<ExerciseAggregation[]>(this.exercisesUrl + '/range' + query);
  }

  public getSongAggregations(page: number, pageSize: number): Observable<SongAggregation[]> {
    const fakeSongAggregations: SongAggregation[] = [
      {
        aggregationId: 'agg-1',
        type: 'song',
        userId: 'user-1',
        totalPlayed: 120,
        timesPlayed: 45,
        author: 'Metallica',
        name: 'Nothing Else Matters',
        songsByDateAggregations: [
          { year: 2024, month: 1, timesPlayed: 10, totalDuration: 2300 },
          { year: 2024, month: 2, timesPlayed: 15, totalDuration: 3450 }
        ]
      },
      {
        aggregationId: 'agg-2',
        type: 'song',
        userId: 'user-1',
        totalPlayed: 80,
        timesPlayed: 30,
        author: 'Pink Floyd',
        name: 'Comfortably Numb',
        songsByDateAggregations: [
          { year: 2024, month: 3, timesPlayed: 12, totalDuration: 4200 }
        ]
      },
      {
        aggregationId: 'agg-3',
        type: 'song',
        userId: 'user-2',
        totalPlayed: 150,
        timesPlayed: 60,
        author: 'Nirvana',
        name: 'Smells Like Teen Spirit',
        songsByDateAggregations: [
          { year: 2023, month: 11, timesPlayed: 20, totalDuration: 3600 },
          { year: 2023, month: 12, timesPlayed: 18, totalDuration: 3240 },
          { year: 2024, month: 1, timesPlayed: 22, totalDuration: 3960 }
        ]
      },
      {
        aggregationId: 'agg-4',
        type: 'song',
        userId: 'user-3',
        totalPlayed: 40,
        timesPlayed: 15,
        author: 'Daft Punk',
        name: 'Get Lucky',
        songsByDateAggregations: [
          { year: 2024, month: 5, timesPlayed: 15, totalDuration: 3300 }
        ]
      },
      {
        aggregationId: 'agg-5',
        type: 'song',
        userId: 'user-4',
        totalPlayed: 200,
        timesPlayed: 90,
        author: 'AC/DC3 ',
        name: 'Back In Black 3 ',
        songsByDateAggregations: null
      },
      {
        aggregationId: 'agg-8',
        type: 'song',
        userId: 'user-4',
        totalPlayed: 200,
        timesPlayed: 90,
        author: 'AC/DC',
        name: 'Back In Black',
        songsByDateAggregations: null
      },
      {
        aggregationId: 'agg-6',
        type: 'song',
        userId: 'user-4',
        totalPlayed: 200,
        timesPlayed: 90,
        author: 'AC/DC cover',
        name: 'Back In Black2 ',
        songsByDateAggregations: null
      },
      
      {
        aggregationId: 'agg-65',
        type: 'song',
        userId: 'user-4',
        totalPlayed: 200,
        timesPlayed: 90,
        author: 'AC/DC cover22',
        name: 'Back In Black222 ',
        songsByDateAggregations: null
      },
      {
        aggregationId: 'agg-62',
        type: 'song',
        userId: 'user-4',
        totalPlayed: 200,
        timesPlayed: 90,
        author: 'AC/DC cov555er',
        name: 'Back In Black2225 ',
        songsByDateAggregations: null
      },
    ];

    return of(fakeSongAggregations);
  }
}