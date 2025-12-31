import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "./components/dashboard.component";
import { MonthlyProgressChartComponent } from "./components/monthly-progress-chart/monthly-progress-chart.component";
import { RangeProgressChartComponent } from "./components/range-progress-chart/range-progress-chart.component";
import { ProgressCardsComponent } from "./components/progress-cards/progress-cards.component";
import { AggregationService } from "./services/aggregation.service";
import { SongChartComponent } from "./components/song-chart/song-chart.component";

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    MonthlyProgressChartComponent,
    RangeProgressChartComponent,
    ProgressCardsComponent,
    SongChartComponent,
    RouterModule.forChild([
      { path: '', component: DashboardComponent }
    ])
  ],
  providers: [
    AggregationService
  ],
  exports: []
})
export class DashboardModule { }
