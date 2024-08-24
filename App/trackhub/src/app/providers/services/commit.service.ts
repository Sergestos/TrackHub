import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExerciseModel, RecordModel } from '../../moduls/commit/commit.models';

@Injectable()
export class CommitService {
    constructor(private http: HttpClient) { }

    public saveExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        const headers: HttpHeaders = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        const url = 'http://localhost:5044/api/exercise';
        return this.http.post<any>(url, exerciseModel, { headers, responseType: 'text' as 'json' })
    }

    public getExerciseRecords(exerciseId: string): Observable<ExerciseModel> {
        const url = 'http://localhost:5044/api/exercise';
        const params = new HttpParams()
            .set('exerciseId', exerciseId)

        return this.http.get<ExerciseModel>(url, { params});
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