import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExerciseModel, RecordModel, SuggestionResult } from './commit.models';
import { environment } from '../../environments/environment';

@Injectable()
export class CommitService {
    private baseExerciseUrl: string = environment.apiUrl + '/api/exercise';
    private baseSuggestionUrl: string = environment.apiUrl + '/api/suggestion';

    private headers: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) { }

    public saveExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.post<any>(this.baseExerciseUrl, exerciseModel, {
            headers: this.headers, 
            responseType: 'text' as 'json' 
        });
    }

    public updateExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.put<any>(this.baseExerciseUrl, exerciseModel, { 
            headers: this.headers,
            responseType: 'text' as 'json' 
        });
    }

    public getExerciseRecordById(exerciseId: string): Observable<ExerciseModel> {
        const params = new HttpParams()
            .set('exerciseId', exerciseId)

        return this.http.get<ExerciseModel>(this.baseExerciseUrl, { params});
    }

    public getExerciseRecordByDate(date: Date): Observable<ExerciseModel> {
        const params = new HttpParams()
            .set('date', date.toDateString())

        return this.http.get<ExerciseModel>(this.baseExerciseUrl + '/by-date', { params});
    }

    public deleteExercise(exerciseId: string): Observable<void> {
        const params = new HttpParams()
            .set('exerciseId', exerciseId);

        return this.http.delete<void>(this.baseExerciseUrl, { params });
    }

    public deleteRecords(exerciseId: string, recordIds: string[]): Observable<void> {
        const url = this.baseExerciseUrl + '/' + exerciseId + '/records';
        let params = new HttpParams();
        recordIds.forEach((item) => {
            params = params.append('recordId', item);
        });

        return this.http.delete<void>(url, {
            params: params,
            headers: this.headers,
            responseType: 'text' as 'json' 
        });
    }

    public getSongSuggestrions(pattern: string, author: string | null | undefined): Observable<SuggestionResult[]> {
         const params = new HttpParams()
            .set('pattern', pattern);
        
        if (author) {
            params.set('author', author);
        }

        return this.http.get<SuggestionResult[]>(this.baseSuggestionUrl + '/songs', { params });        
    }

    public getAuthorSuggestrions(pattern: string): Observable<SuggestionResult[]> {
        const params = new HttpParams()
            .set('pattern', pattern)

        return this.http.get<SuggestionResult[]>(this.baseSuggestionUrl + '/authors', { params });
    }
}