import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable, of } from "rxjs";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { ApiService } from "../../../providers/services/api.service";

@Injectable()
export class AggregationService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/aggregations';

  private apiService = inject(ApiService);

  public getCurrentMonthAggregation(): Observable<ExerciseAggregation> {
    return of({
      id: "1",
      user_id: "123",
      aggregation_month: 12,
      aggregation_year: 2025,

      total_played: 125,
      warmup_aggregation: {
        record_type_name: 'Exercise',
        times_played: 5,
        total_played: 100
      },
      song_aggregation: {
        record_type_name: 'Song',
        times_played: 2,
        total_played: 25
      }

    } as ExerciseAggregation);
    // return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + '/current-month, {});
  }

  public getLastMonthAggregation(): Observable<ExerciseAggregation> {
    return of({
      id: "1",
      user_id: "123",
      aggregation_month: 11,
      aggregation_year: 2025,

      total_played: 105,
      warmup_aggregation: {
        record_type_name: 'Exercise',
        times_played: 5,
        total_played: 80
      },
      song_aggregation: {
        record_type_name: 'Song',
        times_played: 2,
        total_played: 25
      }

    } as ExerciseAggregation);
    //return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + '/previous-month', {});
  }

  public getMonthRangeAggregation(startDate: Date, endDate: Date): Observable<ExerciseAggregation[]> {
    return of([
      {
        id: "1",
        user_id: "123",
        aggregation_month: 11,
        aggregation_year: 2025,

        total_played: 125,
        warmup_aggregation: {
          record_type_name: 'Exercise',
          times_played: 5,
          total_played: 100
        },
        song_aggregation: {
          record_type_name: 'Song',
          times_played: 2,
          total_played: 25
        }
      },
      {
        id: "1",
        user_id: "123",
        aggregation_month: 12,
        aggregation_year: 2025,

        total_played: 155,
        warmup_aggregation: {
          record_type_name: 'Exercise',
          times_played: 5,
          total_played: 130
        },
        song_aggregation: {
          record_type_name: 'Song',
          times_played: 2,
          total_played: 25
        }
      }] as ExerciseAggregation[]);
    //return this.apiService.get<ExerciseAggregation[]>(this.exercisesUrl + '/range', {});
  }
}