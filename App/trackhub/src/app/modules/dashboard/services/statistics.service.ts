import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable, of } from "rxjs";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { ApiService } from "../../../providers/services/api.service";

@Injectable()
export class StatisticsService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/statistics';

  private apiService = inject(ApiService);

  public getCurrentMonthStatistics(): Observable<ExerciseAggregation> {
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
    // return this.apiService.get<ExerciseAggregation>(this.exercisesUrl, {});
  }

  public getLastMonthStatistics(): Observable<ExerciseAggregation> {
    return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + '/previous', {});
  }
}