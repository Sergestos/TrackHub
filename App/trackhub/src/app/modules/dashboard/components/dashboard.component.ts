import { Component, inject } from "@angular/core";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { AggregationService } from "../services/aggregation.service";
import { RangeRequest } from "./range-progress-chart/range-progress-chart.component";

@Component({
  selector: 'trh-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
  standalone: false
})
export class DashboardComponent {
  private aggregationService = inject(AggregationService);

  public currentMonthAggregation: ExerciseAggregation | null = null;
  public lastMonthAggregation: ExerciseAggregation | null = null;
  public monthRangeAggregations: ExerciseAggregation[] | null = null;

  constructor() {
    this.aggregationService.getCurrentMonthAggregation()
      .subscribe({
        next: (result) => {
          this.currentMonthAggregation = result;
        }
      });

    this.aggregationService.getLastMonthAggregation()
      .subscribe({
        next: (result) => {
          this.lastMonthAggregation = result;
        }
      });

    this.aggregationService.getMonthRangeAggregation(new Date(), new Date())
      .subscribe({
        next: (result) => {
          this.monthRangeAggregations = result;
        }
      })
  }

  public onMonthRangeApplyClicked($event: RangeRequest): void {
    this.aggregationService.getMonthRangeAggregation($event.start, $event.end)
      .subscribe({
        next: (result) => {
          this.monthRangeAggregations = result;
        }
      })
  }
}