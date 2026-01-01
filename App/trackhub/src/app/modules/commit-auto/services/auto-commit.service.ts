import { Injectable, inject } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { PreviewState } from "../models/preview-state.model";
import { Exercise } from "../../../models/exercise";
import { ApiService } from "../../../providers/services/api.service";

@Injectable()
export class AutoCommitService {
  readonly previeweUrl: string = environment.apiUrl + '/api/preview';
  readonly exerciseUrl: string = environment.apiUrl + '/api/exercises';

  private apiService = inject(ApiService);

  public previewExerice(previewText: string): Observable<PreviewState> {
    return this.apiService.post<PreviewState>(
      this.previeweUrl,
      { previewText }
    );
  }

  public saveExercise(exerciseModel: Exercise): Observable<Exercise> {
    return this.apiService.post<Exercise>(
      this.exerciseUrl,
      exerciseModel
    );
  }
}