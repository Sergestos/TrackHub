import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { ApiService } from "../../../providers/services/api.service";

@Injectable()
export class AggregationService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/aggregations';

  private apiService = inject(ApiService);

  public getCurrentMonthAggregation(): Observable<ExerciseAggregation> {
    const date = Date.now().toString();
    return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + `?date=${date}`);
  }

  public getLastMonthAggregation(): Observable<ExerciseAggregation> {
    const date = new Date();
    date.setMonth(date.getMonth() - 1);

    return this.apiService.get<ExerciseAggregation>(this.exercisesUrl + `?date=${date}`);
  }

  public getMonthRangeAggregation(startDate: Date, endDate: Date): Observable<ExerciseAggregation[]> {
    const query = `?startDate=${startDate.toDateString()}&endDate=${endDate.toDateString()}`
    return this.apiService.get<ExerciseAggregation[]>(this.exercisesUrl + '/range' + query);
  }
}