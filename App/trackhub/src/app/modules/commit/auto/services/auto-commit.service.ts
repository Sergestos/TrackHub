import { Injectable, inject } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class AutoCommitService {
  private baseExerciseUrl: string = environment.apiUrl + '/api/exercise';

  private httpClient = inject(HttpClient);

  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public previewExerice(previewText: string): Observable<any> {
    return this.httpClient.post<any>(this.baseExerciseUrl + '/preview', {
      previewText
    }, {
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }

  public saveExercise(exerciseText: string): Observable<any> {
    return this.httpClient.post<any>(this.baseExerciseUrl + '/auto', exerciseText, {
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }
}