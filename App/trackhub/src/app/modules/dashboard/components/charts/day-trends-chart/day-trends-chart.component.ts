import { NgxEchartsDirective, provideEchartsCore } from "ngx-echarts";
import * as echarts from 'echarts/core';
import {
  TooltipComponent,
  LegendComponent,
  TitleComponent,
  GridComponent,
} from 'echarts/components';
import { Component, inject, OnInit, signal } from "@angular/core";
import { CanvasRenderer } from "echarts/renderers";
import { AggregationService } from "../../../services/aggregation.service";
import { DaysTrendAggregation } from "../../../models/days-trend.model";
import { LineChart, ScatterChart } from "echarts/charts";

echarts.use([
  TooltipComponent,
  LegendComponent,
  TitleComponent,
  GridComponent,
  LineChart,
  ScatterChart,
  CanvasRenderer,
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
  public chartDisplayType: 'record' | 'play' = 'record';

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

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as
      | 'record'
      | 'play';

    if (this.chartData) {
      this.buildChart();
    }
  }

  private buildChart(): void {
    const isRecordTypeDisplay = this.chartDisplayType == 'record';

    const bars = this.chartData?.daysTrendBarList ?? [];
    const map = new Map<string, number>();

    for (const x of bars) {
      const key = this.normalizeDate(x.playDate);

      const value = isRecordTypeDisplay
        ? x.totalPlayedRhythmDuration + x.totalPlayedSoloDuration + x.totalPlayedBothDuration
        : (x.totalWarmupDuration ?? 0) +
        (x.totalSongDuration ?? 0) +
        (x.totalImprovisationDuration ?? 0) +
        (x.totalPracticalExerciseDuration ?? 0) +
        (x.totalComposingDuration ?? 0);

      map.set(key, value);
    }

    const xData: string[] = [];
    const yData: number[] = [];

    for (let i = 29; i >= 0; i--) {
      const d = new Date();
      d.setDate(d.getDate() - i);

      const key = this.normalizeDate(d);

      xData.push(key);
      yData.push(map.get(key) ?? 0);
    }

    const trendData = this.buildMovingAverage(yData);

    this.options = {
      tooltip: { trigger: 'axis' },
      grid: { left: 40, right: 20, top: 20, bottom: 40 },
      xAxis: {
        type: 'category',
        data: xData.map(x => x.slice(5))
      },
      yAxis: {
        type: 'value',
        boundaryGap: [0, 0.01],
        splitLine: {
          show: true,
          lineStyle: {
            color: 'rgba(255,255,255,0.15)',
            width: 1,
            type: 'solid',
          },
        },
      },
      series: [
        {
          type: 'scatter',
          data: yData
        },
        {
          type: 'line',
          data: trendData,
          smooth: false,
          symbol: 'none',
          lineStyle: {
            width: 2,
            type: 'dashed'
          },
          tooltip: {
            show: false
          }
        }
      ]
    };
  }

  private buildMovingAverage(values: number[], windowSize = 5): number[] {
    return values.map((_, i) => {
      const start = Math.max(0, i - windowSize + 1);
      const slice = values.slice(start, i + 1);
      const sum = slice.reduce((a, b) => a + b, 0);
      return sum / slice.length;
    });
  }

  private normalizeDate(date: string | Date): string {
    const d = new Date(date);
    return d.toISOString().slice(0, 10);
  }
}