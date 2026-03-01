import { inject, Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { map, Observable, of, switchMap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Exercise } from '../../../models/exercise';
import { ExerciseRecord } from '../../../models/exercise-record';
import { ApiService } from '../../../providers/services/api.service';

@Injectable()
export class CommitService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/exercises';

  private apiService = inject(ApiService);

  public saveExercise(exerciseModel: Exercise): Observable<Exercise> {
    return this.apiService.post<Exercise>(this.exercisesUrl, exerciseModel);
  }

  public updateExercise(exerciseModel: Exercise): Observable<ExerciseRecord[]> {
    return this.apiService.put<ExerciseRecord[]>(
      `${this.exercisesUrl}/${exerciseModel.exerciseId}`,
      exerciseModel
    );
  }

  public getExerciseRecordById(exerciseId: string): Observable<Exercise> {
    return this.apiService.get<Exercise>(`${this.exercisesUrl}/${exerciseId}`);
  }

  public getExerciseRecordByDate(date: Date): Observable<Exercise> {
    const iso = this.toIsoDateOnly(date);

    const params = new HttpParams().set('from', iso).set('to', iso);

    return this.apiService
      .get<Exercise[]>(this.exercisesUrl, params)
      .pipe(map((result) => result[0] ?? null));
  }

  public deleteExercise(exerciseId: string): Observable<void> {
    return this.apiService.delete<void>(`${this.exercisesUrl}/${exerciseId}`);
  }

  public deleteRecords(
    exerciseId: string,
    recordIds: string[]
  ): Observable<void> {
    const url = `${this.exercisesUrl}/${exerciseId}/records`;
    let params = new HttpParams();
    recordIds.forEach((item) => {
      params = params.append('recordId', item);
    });

    return this.apiService.delete<void>(url, params);
  }

  private toIsoDateOnly(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
