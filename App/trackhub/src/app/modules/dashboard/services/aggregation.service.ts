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

  public getSongAggregation(): Observable<SongAggregation> {
    return of({} as SongAggregation);
  }
}