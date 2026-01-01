import { Component, OnInit, inject } from "@angular/core";
import { AggregationService } from "../../services/aggregation.service";
import { SongAggregation } from "../../models/song-aggregation.model";
import * as echarts from 'echarts/core';
import { NgxEchartsDirective, provideEchartsCore } from "ngx-echarts";
import { BarChart } from 'echarts/charts';
import { GridComponent, TooltipComponent } from "echarts/components";
import { CanvasRenderer } from "echarts/renderers";

const PAGE_SIZE = 10;

type ChartMetric = 'total_played' | 'times_played';

echarts.use([
  BarChart,
  GridComponent,
  TooltipComponent,
  CanvasRenderer
]);

@Component({
  selector: 'thr-song-chart',
  templateUrl: './song-chart.component.html',
  styleUrl: './song-chart.component.scss',
  standalone: true,
  providers: [provideEchartsCore({ echarts })],
  imports: [
    NgxEchartsDirective,
  ]
})
export class SongChartComponent implements OnInit {
  public chartData: SongAggregation[] | null = null;
  public options!: any;

  private aggregationService = inject(AggregationService);

  private chartDisplayType: ChartMetric = 'total_played';
  private currentPage: number = 1;

  public ngOnInit(): void {
    this.aggregationService
      .getSongAggregations(this.currentPage, PAGE_SIZE)
      .subscribe({
        next: (result) => {
          if (result) {
            this.chartData = result;
            this.buildChart();
          }
        }
      });
  }

  public onTypeChanged($event: Event) {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'total_played' | 'times_played';

    if (this.chartData) {
      this.buildChart();
    }
  }

  public onNextSongsLoad(): void {
    this.currentPage++;

    this.aggregationService
      .getSongAggregations(this.currentPage, PAGE_SIZE)
      .subscribe({
        next: (result) => {
          this.chartData?.concat(result);
        }
      })
  }

  public onPreviousSongsLoad(): void {
    this.currentPage--;

    this.aggregationService
      .getSongAggregations(this.currentPage, PAGE_SIZE)
      .subscribe({
        next: (result) => {
          this.chartData?.concat(result);
        }
      })
  }

  private buildChart(): void {
    this.options = {
      title: {
        text: 'Songs'
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow'
        },
      },
      legend: {
        orient: 'vertical',
        left: '2.5%',
        top: '5%'
      },
      grid: { top: 40, bottom: 35, left: 60, right: 20, containLabel: true },
      xAxis: {
        type: 'value',
        boundaryGap: [0, 0.01],
        splitLine: {
          show: true,
          lineStyle: {
            color: 'rgba(255,255,255,0.15)',
            width: 1,
            type: 'solid'
          }
        }
      },
      yAxis: {
        type: 'category',
        data: this.chartData!.map(x => x.name),
      },
      series: [
        {
          name: 'Total played (in minutes)',
          type: 'bar',
          data: this.buildData()
        }
      ]
    };
  }

  private buildData(): any {
    return this.chartDisplayType == 'total_played' ?
      this.chartData!.map(x => x.totalPlayed) : this.chartData!.map(x => x.timesPlayed);
  }
}