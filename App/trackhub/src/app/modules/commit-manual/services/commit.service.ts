import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Exercise } from '../../../models/exercise';
import { ExerciseRecord } from '../../../models/exercise-record';
import { SuggestionResult } from '../models/suggestion-result.model';

@Injectable()
export class CommitService {
  private baseExerciseUrl: string = environment.apiUrl + '/api/exercise';
  private baseSuggestionUrl: string = environment.apiUrl + '/api/suggestion';

  private httpClient = inject(HttpClient);

  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public saveExercise(exerciseModel: Exercise): Observable<Exercise> {
    return this.httpClient.post<any>(this.baseExerciseUrl, exerciseModel, {
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }

  public updateExercise(exerciseModel: Exercise): Observable<ExerciseRecord[]> {
    return this.httpClient.put<any>(this.baseExerciseUrl, exerciseModel, {
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }

  public getExerciseRecordById(exerciseId: string): Observable<Exercise> {
    const params = new HttpParams()
      .set('exerciseId', exerciseId)

    return this.httpClient.get<Exercise>(this.baseExerciseUrl, { params });
  }

  public getExerciseRecordByDate(date: Date): Observable<Exercise> {
    const params = new HttpParams()
      .set('date', date.toDateString())

    return this.httpClient.get<Exercise>(`${this.baseExerciseUrl}/by-date`, { params });
  }

  public deleteExercise(exerciseId: string): Observable<void> {
    const params = new HttpParams()
      .set('exerciseId', exerciseId);

    return this.httpClient.delete<void>(this.baseExerciseUrl, { params });
  }

  public deleteRecords(exerciseId: string, recordIds: string[]): Observable<void> {
    const url = `${this.baseExerciseUrl}/${exerciseId}/records`;
    let params = new HttpParams();
    recordIds.forEach((item) => {
      params = params.append('recordId', item);
    });

    return this.httpClient.delete<void>(url, {
      params: params,
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }

  public getSongSuggestrions(pattern: string, author?: string | null): Observable<SuggestionResult[]> {
    const params = new HttpParams()
      .set('pattern', pattern);

    if (author) {
      params.set('author', author);
    }

    return this.httpClient.get<SuggestionResult[]>(`${this.baseSuggestionUrl}/songs`, { params });
  }

  public getAuthorSuggestrions(pattern: string): Observable<SuggestionResult[]> {
    const params = new HttpParams()
      .set('pattern', pattern)

    return this.httpClient.get<SuggestionResult[]>(`${this.baseSuggestionUrl}/authors`, { params });
  }
}