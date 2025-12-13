import { Component, inject, input, OnInit, output } from "@angular/core";
import { ExerciseListService } from "../../services/exercise-list.service";
import { FilterDateModel, FilterModel } from "../../models/exercise-list.models";

@Component({
  selector: 'trh-exercise-filter',
  templateUrl: './exercise-filter.component.html',
  standalone: false
})
export class FilterComponent implements OnInit {
  public userFirstYear!: number;

  public filter = input<FilterModel>();

  public onDateChangedEmmiter = output<FilterDateModel>();
  public onExpandAllEmitter = output<boolean>();
  public onShowPlayedOnlyEmitter = output<boolean>();

  private exerciseListService = inject(ExerciseListService);

  public ngOnInit(): void {
    if (!this.filter()?.dateFilter) {
      this.filter()!.dateFilter = {
        year: new Date().getFullYear(),
        month: new Date().getMonth() + 1
      }
    }

    this.exerciseListService.getUserExerciseSettings()
      .subscribe(item => {
        this.userFirstYear = new Date(item.firstPlayDate).getFullYear();
      })
  }

  public onExpandAllClicked(event: any): void {
    this.onExpandAllEmitter.emit(event.target.checked);
  }

  public onShowPlayedOnlyClicked(event: any): void {
    this.onShowPlayedOnlyEmitter.emit(event.target.checked);
  }

  public onFilterDateChanges(event: any) {
    this.onDateChangedEmmiter.emit(event);
  }
}