import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable, of } from "rxjs";
import { ExerciseItemView } from "./exercise-list.models";
import { environment } from "../../environments/environment";

@Injectable()
export class ExerciseListService {
  readonly BaseExerciseUrl: string = environment.apiUrl + '/api/exercise';
  readonly BaseUserUrl: string = environment.apiUrl + '/api/user';

  constructor(private http: HttpClient) { }

  public getUserExerciseProfile(): Observable<Date> {
    const url = this.BaseUserUrl + '/first-play'
    return this.http.get<string>(url)
      .pipe(map(date => new Date(date)));
  }

  public getExercisesByDate(year?: number, month?: number): Observable<ExerciseItemView[]> {
    const url = this.BaseExerciseUrl + '/list'
    const params = new HttpParams()
      .set('year', year ?? '')
      .set('month', month ?? '');

    return this.http.get<ExerciseItemView[]>(url, { params });
  }

  public deleteExercise(exerciseId: string): Observable<void> {
    const params = new HttpParams()
      .set('exerciseId', exerciseId);

    return this.http.delete<void>(this.BaseExerciseUrl, { params });
  }
}