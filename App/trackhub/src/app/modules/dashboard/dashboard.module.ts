import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "./components/dashboard.component";
import { MonthlyProgressChartComponent } from "./components/monthly-progress-chart/monthly-progress-chart.component";
import { RangeProgressChartComponent } from "./components/range-progress-chart/range-progress-chart.component";
import { ProgressCardsComponent } from "./components/progress-cards/progress-cards.component";
import { StatisticsService } from "./services/statistics.service";

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    MonthlyProgressChartComponent,
    RangeProgressChartComponent,
    ProgressCardsComponent,
    RouterModule.forChild([
      { path: '', component: DashboardComponent }
    ])
  ],
  providers: [
    StatisticsService
  ],
  exports: []
})
export class DashboardModule { }
