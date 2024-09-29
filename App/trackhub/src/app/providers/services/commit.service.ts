import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExerciseModel, RecordModel } from '../../moduls/commit/commit.models';

@Injectable()
export class CommitService {
    private url: string = 'http://localhost:5044/api/exercise';
    private headers: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) { }

    public saveExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.post<any>(this.url, exerciseModel, {
            headers: this.headers, 
            responseType: 'text' as 'json' 
        });
    }

    public updateExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return this.http.put<any>(this.url, exerciseModel, { 
            headers: 
            this.headers, responseType: 'text' as 'json' 
        });
    }

    public getExerciseRecords(exerciseId: string): Observable<ExerciseModel> {
        const params = new HttpParams()
            .set('exerciseId', exerciseId)

        return this.http.get<ExerciseModel>(this.url, { params});
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