import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { PreviewState } from "../models/preview-state.model";
import { Exercise } from "../../../models/exercise";
import { ExerciseRecord } from "../../../models/exercise-record";

@Injectable()
export class AutoCommitService {
  private basePrevieweUrl: string = environment.apiUrl + '/api/preview';
  private baseExerciseUrl: string = environment.apiUrl + '/api/exercise';

  private httpClient = inject(HttpClient);

  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public previewExerice(previewText: string): Observable<PreviewState> {
    return this.httpClient.post<PreviewState>(this.basePrevieweUrl, {
      previewText
    }, {
      headers: this.headers,
    });
  }

  public saveExercise(exerciseModel: Exercise): Observable<Exercise> {
    return this.httpClient.post<any>(this.baseExerciseUrl, exerciseModel, {
      headers: this.headers,
    });
  }
}