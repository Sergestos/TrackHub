import { Injectable, inject } from '@angular/core';
import { Exercise } from '../../models/exercise';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from './api.service';

@Injectable()
export class ExportService {
  readonly exercisesUrl: string = environment.apiUrl + '/api/exercises';

  private apiService = inject(ApiService);

  public exportExercises(month: number, year: number) {
    const fileName = `report_${month}_${year}`;

    this.getExercises(month, year).subscribe({
      next: (exercises) => {
        const data = this.buildExerciseExportData(exercises);
        this.download(data, fileName);
      },
    });
  }

  private getExercises(month: number, year: number): Observable<Exercise[]> {
    const range = this.getMonthDateRange(year, month);

    const params = new HttpParams().set('from', range.from).set('to', range.to);

    return this.apiService.get<Exercise[]>(this.exercisesUrl, params);
  }

  private download(data: string, fileName: string) {
    const bom = '\uFEFF';
    const blob = new Blob([bom + data], { type: 'text/plain;charset=utf-8' });

    const url = URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);

    URL.revokeObjectURL(url);
  }

  private buildExerciseExportData(exercises: Exercise[]): string {
    return 'dsdas';
  }

  private getMonthDateRange(
    year: number,
    month: number
  ): { from: string; to: string } {
    const first = new Date(year, month - 1, 1);

    const last = new Date(year, month, 0);

    return {
      from: this.toIsoDateOnly(first),
      to: this.toIsoDateOnly(last),
    };
  }

  private toIsoDateOnly(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
