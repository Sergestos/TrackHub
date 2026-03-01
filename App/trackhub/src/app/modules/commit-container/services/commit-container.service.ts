import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ApiService } from '../../../providers/services/api.service';
import { Exercise } from '../../../models/exercise';
import { Observable } from 'rxjs';

const LAST_COUNT = 5;

@Injectable()
export class CommitContainerExerciseService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/exercises';

  private apiService = inject(ApiService);

  public getLastUserExercises(): Observable<Exercise[]> {
    const query = `${this.exercisesUrl}/recent?lastCount=${LAST_COUNT}`;
    return this.apiService.get<Exercise[]>(query);
  }
}
