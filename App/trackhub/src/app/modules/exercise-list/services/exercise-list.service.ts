import { HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ExerciseItemView } from '../models/exercise-list.models';
import { ApiService } from '../../../providers/services/api.service';
import { UserSettings } from '../models/user-settings.models';

@Injectable()
export class ExerciseListService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/exercises';
  readonly userUrl: string = environment.apiUrl + '/api/users';

  private apiService = inject(ApiService);

  public getUserExerciseSettings(): Observable<UserSettings> {
    return this.apiService.get<UserSettings>(
      this.userUrl + '/current/settings',
    );
  }

  public getExercisesByDate(
    year?: number,
    month?: number,
  ): Observable<ExerciseItemView[]> {
    const params = new HttpParams()
      .set('year', year ?? '')
      .set('month', month ?? '');

    return this.apiService.get<ExerciseItemView[]>(
      this.exercisesUrl + '/summary',
      params,
    );
  }

  public deleteExercise(exerciseId: string): Observable<void> {
    return this.apiService.delete<void>(`${this.exercisesUrl}/${exerciseId}`);
  }
}
