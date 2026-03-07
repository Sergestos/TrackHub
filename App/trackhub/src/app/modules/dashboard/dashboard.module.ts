import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { AggregationService } from './services/aggregation.service';
import { SongChartComponent } from './components/charts/song-chart/song-chart.component';
import { MonthlyProgressChartComponent } from './components/charts/monthly-progress-chart/monthly-progress-chart.component';
import { RangeProgressChartComponent } from './components/charts/range-progress-chart/range-progress-chart.component';
import { ProgressCardsComponent } from './components/charts/progress-cards/progress-cards.component';

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    MonthlyProgressChartComponent,
    RangeProgressChartComponent,
    ProgressCardsComponent,
    SongChartComponent,
    RouterModule.forChild([{ path: '', component: DashboardComponent }]),
  ],
  providers: [AggregationService],
  exports: [],
})
export class DashboardModule {}
