import { NgxEchartsDirective, provideEchartsCore } from "ngx-echarts";
import * as echarts from 'echarts/core';
import {
  TooltipComponent,
  LegendComponent,
  TitleComponent,
} from 'echarts/components';
import { Component, inject, OnInit, signal } from "@angular/core";
import { CanvasRenderer } from "echarts/renderers";
import { AggregationService } from "../../../services/aggregation.service";
import { DaysTrendAggregation } from "../../../models/days-trend.model";

echarts.use([
  TooltipComponent,
  LegendComponent,
  CanvasRenderer,
  TitleComponent,
]);

@Component({
  selector: 'trh-recent-period-chart',
  templateUrl: './day-trends-chart.component.html',
  standalone: true,
  imports: [NgxEchartsDirective],
  providers: [provideEchartsCore({ echarts })],
})
export class DaysTrendChartComponent implements OnInit {
  public options: any;
  public mergeOptions: any = null;

  private aggregationService = inject(AggregationService);

  public chartData?: DaysTrendAggregation;

  public ngOnInit(): void {
    this.aggregationService.getDaysTrendAggregations()
      .subscribe({
        next: (data) => {
          this.chartData = data;

          if (this.chartData) {
            this.buildChart();
          }
        }
      });
  }

  private buildChart(): void {

    this.options = {
      xAxis: {},
      yAxis: {},
      series: [
        {
          type: 'scatter',
          data: this.chartData
        },
        {
          type: 'line',
          data: null,
          smooth: true,
          symbol: 'none', // hide points
          lineStyle: {
            width: 2
          }
        }
      ]
    };
  }
}