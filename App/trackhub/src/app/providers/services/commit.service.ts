import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExerciseModel, RecordModel } from '../../moduls/commit/commit.models';

@Injectable()
export class CommitService {
    constructor(private http: HttpClient) { }

    public saveExercise(exerciseModel: ExerciseModel): Observable<RecordModel[]> {
        return of();
    }

    public getExerciseRecords(exerciseId: number): Observable<RecordModel[]> {
        return of([
           {
                id: 1,
                name: 'Father of Nutella',
                author: 'Beertalica',
                rectorType: 'song',
                duration: 30,
                bpm: 120,
                playType: 'solo',
                isRecorded: false
           },
           {
                id: 2,
                name: 'Middle of the Night',
                author: 'Beertalica',
                rectorType: 'song',
                duration: 30,
                bpm: 120,
                playType: 'solo',
                isRecorded: false
            },
            {
                id: 3,
                name: 'Master of Cookies',
                author: 'Nontallica',
                rectorType: 'song',
                duration: 30,
                bpm: 120,
                playType: 'rhythm',
                isRecorded: false
            } 
        ]);
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