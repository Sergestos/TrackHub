import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { ExerciseDetails, UserExerciseProfile } from "../../moduls/exericse-list/exercise-list.models";

@Injectable()
export class ExerciseListService {
    constructor(private http: HttpClient) { }

    public getUserExerciseProfile(): Observable<UserExerciseProfile> {
        return of({
            firstExerciseDate: new Date("09/03/2018")
        })
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