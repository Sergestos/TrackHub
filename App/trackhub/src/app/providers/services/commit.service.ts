import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExerciseModel, RecordModel } from '../../moduls/commit/commit.models';
import { environment } from '../../environments/environment';

@Injectable()
export class CommitService {
    private baseUrl: string = environment.apiUrl + '/api/exercise';
    private headers: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) { }

    public saveExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.post<any>(this.baseUrl, exerciseModel, {
            headers: this.headers, 
            responseType: 'text' as 'json' 
        });
    }

    public updateExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.put<any>(this.baseUrl, exerciseModel, { 
            headers: this.headers,
            responseType: 'text' as 'json' 
        });
    }

    public getExerciseRecordById(exerciseId: string): Observable<ExerciseModel> {
        const params = new HttpParams()
            .set('exerciseId', exerciseId)

        return this.http.get<ExerciseModel>(this.baseUrl, { params});
    }

    public getExerciseRecordByDate(date: Date): Observable<ExerciseModel> {
        const params = new HttpParams()
            .set('date', date.toDateString())

        return this.http.get<ExerciseModel>(this.baseUrl + '/by-date', { params});
    }

    public deleteExercise(exerciseId: string): Observable<void> {
        const params = new HttpParams()
            .set('exerciseId', exerciseId);

        return this.http.delete<void>(this.baseUrl, { params });
    }

    public deleteRecords(exerciseId: string, recordIds: string[]): Observable<void> {
        const url = this.baseUrl + '/' + exerciseId + '/records';
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

    public getSongSuggestrions(pattern: string): Observable<string[]> {
        // return this.http.get<string[]>('https://jsonplaceholder.typicode.com/todos');
        return of([
            'Megalsay',
            'Magakiller',
            'Mugamen',
            'Michaluch',
            'MinuteOf5'
        ])
    }

    public getAuthorSuggestrions(pattern: string): Observable<string[]> {
        return of([
            'Metallica',
            'Megadeth',
            'MegamozG',
            'Michuga',
            'Mackintosh'
        ])
    }
}