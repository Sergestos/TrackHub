import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable()
export class CommitService {
    constructor(private http: HttpClient) { }

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