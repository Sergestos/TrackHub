import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommitService {
    constructor(private http: HttpClient) { }

    public getSongSuggestrions(): Observable<string[]> {
        return this.http.get<string[]>('https://jsonplaceholder.typicode.com/todos');
    }

    public getAuthorSuggestrions(): Observable<string[]> {
        return this.http.get<string[]>('https://jsonplaceholder.typicode.com/todos');
    }
}