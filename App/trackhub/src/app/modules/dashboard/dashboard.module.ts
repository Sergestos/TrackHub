import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "./components/dashboard.component";
import { MontlyProgressChartComponent } from "./components/montly-progress-chart/montly-progress-chart.component";
import { RangeProgressChartComponent } from "./components/range-progress-chart/range-progress-chart.component";
import { ProgressCardsComponent } from "./components/progress-cards/progress-cards.component";

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    MontlyProgressChartComponent,
    RangeProgressChartComponent,
    ProgressCardsComponent,
    RouterModule.forChild([
      { path: '', component: DashboardComponent }
    ])
  ],
  providers: [

  ],
  exports: []
})
export class DashboardModule { }
