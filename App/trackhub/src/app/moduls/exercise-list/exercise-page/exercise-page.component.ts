import { Component, inject, OnInit } from "@angular/core";
import { ExerciseItem, ExerciseItemView, FilterDateModel, FiltersModel as FilterModel } from "../exercise-list.models";
import { ActivatedRoute, Router } from "@angular/router";
import { ExerciseListService } from "../exercise-list.service";
import { MatDialog } from '@angular/material/dialog';
import { ModalResult, openDeleteConfirmationModal } from "../../../components/mat-modal/mat-modal.component";
import { LoadingService } from "../../../providers/services/loading.service";

@Component({
  selector: 'trackhub-exercise-page',
  templateUrl: './exercise-page.component.html',
  styleUrls: ['./exercise-page.component.scss'],
  standalone: false
})
export class ExercisePageComponent implements OnInit {
  public exercises: ExerciseItemView[] = [];
  public filter!: FilterModel;

  private router = inject(Router);
  private matDialog = inject(MatDialog);
  private activeRoute = inject(ActivatedRoute);
  private exerciseListService = inject(ExerciseListService);
  private loadingService = inject(LoadingService);

  public get isAnyExerciseShown(): boolean {
    if (this.exercises.length == 0) {
      return false;
    }

    if (this.exercises.every(x => x.isHidden)) {
      return false;
    }

    return true;
  }

  public ngOnInit(): void {
    this.activeRoute.queryParams.subscribe(params => {
      let year: number;
      let month: number;

      const currentDate = new Date();
      year = params['year'] || currentDate.getFullYear();
      month = params['month'] || currentDate.getMonth() + 1;

      this.filter = {
        showPlayedOnly: true,
        showExpanded: true,
        dateFilter: {
          year: year,
          month: month
        }
      };

      this.setData();
    });
  }

  public onCardExpand(exercise: ExerciseItemView): void {
    exercise.isExpanded = !exercise.isExpanded;
  }

  public openDialog(item: ExerciseItemView): void {
    const modal = openDeleteConfirmationModal(this.matDialog);
    modal.afterClosed().subscribe((result: ModalResult) => {
      if (result == ModalResult.Confirmed) {
        this.exercises = this.exercises.filter(x => x != item);
        this.loadingService.show();
        this.exerciseListService
          .deleteExercise(item.exerciseId)
          .subscribe({
            complete: () => this.loadingService.complete()
          });
      }
    });
  }

  public onExerciseEdit(item: ExerciseItemView): void {
    this.router.navigateByUrl("/app/commit?exerciseId=" + item.exerciseId);
  }

  public onDateChanged(dateFilter: FilterDateModel): void {
    let query = `month=${dateFilter.month}&year=${dateFilter.year}`;
    this.router.navigateByUrl("/app/list?" + query);
  }

  public onShowPlayedOnlyEmitter(isExpandAsksed: boolean): void {
    this.exercises
      .filter(x => x.exerciseId == "-1")
      .forEach(x => x.isHidden = isExpandAsksed);
  }

  public onExpandChanged(isExpandAsksed: boolean): void {
    this.exercises
      .filter(x => x.exerciseId != '-1')
      .forEach(x => x.isExpanded = isExpandAsksed);
  }

  private setData(): void {
    this.loadingService.show();
    this.exerciseListService.getExercisesByDate(this.filter.dateFilter?.year, this.filter.dateFilter!.month)
      .subscribe({
        next: (result) => {
          this.exercises = result;
          this.exercises.forEach(x => {
            x.totalPlayed = x.records ? x.records.map(r => r.duration).reduce((sum, duration) => sum + duration, 0) : 0;
            x.isExpanded = this.filter.showExpanded;
          });

          this.fillNonPlayedDays(
            this.filter.dateFilter!.year,
            this.filter.dateFilter!.month,
            this.exercises,
            this.filter.showPlayedOnly!);
        },
        complete: () => this.loadingService.complete()
      });
  }

  private fillNonPlayedDays(year?: number, month?: number, items?: ExerciseItem[], isHidden?: boolean): void {
    for (let dayOfMonth = 1; dayOfMonth <= new Date(year!, month!, 0).getDate(); dayOfMonth++) {
      let dateToFill = new Date(year!, month! - 1, dayOfMonth);
      if (!items!.some(x => new Date(x.playDate).getDate() == dateToFill.getDate())) {
        this.exercises.push({
          exerciseId: "-1",
          playDate: dateToFill,
          records: null,
          isHidden: isHidden
        });
      }
    }

    this.exercises.sort((a, b) => new Date(b.playDate) >= a.playDate ? -1 : 1);
  }
}