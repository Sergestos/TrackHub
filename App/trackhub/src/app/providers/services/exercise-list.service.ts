import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { ExerciseItemView, UserExerciseProfile } from "../../moduls/exericse-list/exercise-list.models";
import { environment } from "../../environments/environment";

@Injectable()
export class ExerciseListService {
    constructor(private http: HttpClient) { }

    public getUserExerciseProfile(): Observable<UserExerciseProfile> {
        return of({
            firstExerciseDate: new Date("09/03/2018")
        })
    }

    public getExercisesByDate(year: number, month: number): Observable<ExerciseItemView[]> {
        const url = environment.apiUrl + '/api/exercise/list';
        const params = new HttpParams()
            .set('year', year)
            .set('month', month);

        return this.http.get<ExerciseItemView[]>(url, { params});
    }    
}