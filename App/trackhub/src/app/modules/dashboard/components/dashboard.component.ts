import { Component, inject, signal } from "@angular/core";
import { ExerciseAggregation } from "../models/exercise-aggregation.model";
import { StatisticsService } from "../services/statistics.service";

@Component({
  selector: 'trh-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
  standalone: false
})
export class DashboardComponent {
  private statisticsService = inject(StatisticsService);

  public currentMonthStatistics: ExerciseAggregation | null = null;
  public lastMonthStatistics = signal<ExerciseAggregation | null>(null);

  constructor() {
    this.statisticsService.getCurrentMonthStatistics()
      .subscribe({
        next: (result) => {
          this.currentMonthStatistics = result;
        }
      });
  }
}