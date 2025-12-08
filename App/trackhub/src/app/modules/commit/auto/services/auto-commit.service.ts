import { Injectable, inject } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { PreviewState } from "../models/preview-state.model";

@Injectable()
export class AutoCommitService {
  private baseExerciseUrl: string = environment.apiUrl + '/api/exercise';

  private httpClient = inject(HttpClient);

  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public previewExerice(previewText: string): Observable<PreviewState> {
    return this.httpClient.post<PreviewState>(this.baseExerciseUrl + '/preview', {
      previewText
    }, {
      headers: this.headers,
    });
  }

  public saveExercise(exerciseText: string): Observable<any> {
    return this.httpClient.post<any>(this.baseExerciseUrl + '/auto', exerciseText, {
      headers: this.headers,
      responseType: 'text' as 'json'
    });
  }
}