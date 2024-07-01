import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { ExerciseDetails, ExerciseItem, UserExerciseProfile } from "../../moduls/exericse-list/exercise-list.models";
import { randomInt } from "crypto";

@Injectable()
export class ExerciseListService {
    constructor(private http: HttpClient) { }

    public getUserExerciseProfile(): Observable<UserExerciseProfile> {
        return of({
            firstExerciseDate: new Date("09/03/2018")
        })
    }

    public getFilteredExercises(year: number, month: number): Observable<ExerciseItem[]> {
        return of([
            {
                exerciseId: "1",
                totalPlayed: 20,
                playDate: new Date(year, month - 1, Math.floor(Math.random() * 30))
            },
            {
                exerciseId: "2",
                totalPlayed: 30,
                playDate: new Date(year, month - 1, Math.floor(Math.random() * 30))
            }
        ])
    }

    public getExerciseDetails(exerciseId: string): Observable<ExerciseDetails[]> {
        // return this.http.get<string[]>('https://jsonplaceholder.typicode.com/todos');
        return of([
            {
                exerciseId: "1",
                exerciseType: "song",
                duration: 10,
                itemName: "master of batteries",
                authorName: "Black Mega Slayer",
                isRecoded: false,
                bpm: 140
            },
            {
                exerciseId: "1",
                exerciseType: "song",
                duration: 10,
                itemName: "master of batteries",
                authorName: "Black Mega Slayer",
                isRecoded: false,
                bpm: 140
            }
        ])
    }
}