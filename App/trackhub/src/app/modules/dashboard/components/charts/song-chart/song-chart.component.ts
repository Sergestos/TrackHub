import { Component, OnInit, inject } from '@angular/core';
import { AggregationService } from '../../../services/aggregation.service';
import { SongAggregation } from '../../../models/song-aggregation.model';
import * as echarts from 'echarts/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { BarChart } from 'echarts/charts';
import {
  GridComponent,
  TitleComponent,
  TooltipComponent,
} from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { ChartMonthPickerComponent } from '../../month-picker/month-picker.component';
import { DatePipe } from '@angular/common';

const PAGE_SIZE = 10;

type ChartSortMetric = 'total_played' | 'times_played';
type ChartAggregationMetric = 'total' | 'date';

echarts.use([
  BarChart,
  GridComponent,
  TooltipComponent,
  CanvasRenderer,
  TitleComponent,
]);

@Component({
  selector: 'thr-song-chart',
  templateUrl: './song-chart.component.html',
  styleUrl: './song-chart.component.scss',
  standalone: true,
  providers: [provideEchartsCore({ echarts })],
  imports: [NgxEchartsDirective, ChartMonthPickerComponent, DatePipe],
})
export class SongChartComponent implements OnInit {
  public chartData: SongAggregation[] | null = null;
  public options!: any;

  public isMonthPickerShown: boolean = false;

  private aggregationService = inject(AggregationService);

  private chartAggregationType: ChartAggregationMetric = 'total';
  private chartDisplayType: ChartSortMetric = 'total_played';
  private currentPage: number = 1;

  public selectedDate: Date = new Date();

  public get isAggregationByDate(): boolean {
    return this.chartAggregationType == 'date';
  }

  public get isPreviousPageAllowed(): boolean {
    if (!this.chartData) return false;

    if (this.currentPage == 1) return false;

    return true;
  }

  public get IsNextPageAllowed(): boolean {
    if (!this.chartData) return false;

    if (this.chartData.length < 10) return false;

    return true;
  }

  public ngOnInit(): void {
    this.requestData();
  }

  public onTypeChanged($event: Event) {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as
      | 'total_played'
      | 'times_played';

    if (this.chartData) {
      this.buildChart();
    }
  }

  public onAggregationChanged($event: Event): void {
    this.chartDisplayType = 'total_played';
    this.currentPage = 1;

    this.chartAggregationType = ($event.target as HTMLSelectElement).value as
      | 'total'
      | 'date';

    this.requestData();
  }

  public onNextSongsLoad(): void {
    if (!this.IsNextPageAllowed) return;

    this.currentPage++;
    this.requestData();
  }

  public onPreviousSongsLoad(): void {
    if (!this.isPreviousPageAllowed) return;

    this.currentPage--;
    this.requestData();
  }

  public onMonthPickerClicked(): void {
    this.isMonthPickerShown = !this.isMonthPickerShown;
  }

  public onMonthSelected($event: Date) {
    this.isMonthPickerShown = false;
    this.selectedDate = $event;

    this.requestData();
  }

  private requestData(): void {
    if (this.chartAggregationType == 'total') {
      this.aggregationService
        .getPagedSongAggregations(this.currentPage, PAGE_SIZE)
        .subscribe({
          next: (result) => {
            this.chartData = result.sort((a, b) => a.totalPlayed! - b.totalPlayed!);
            this.buildChart();
          },
        });
    } else {
      this.aggregationService
        .getSongAggregationsByDate(this.selectedDate)
        .subscribe({
          next: (result) => {
            this.chartData = result.sort((a, b) => {
              const authorCompare = b.author!.localeCompare(a.author!);
              if (authorCompare !== 0) return authorCompare;

              return b.name!.localeCompare(a.name!);
            });

            this.buildChart();
          },
        });
    }
  }

  private buildChart(): void {
    let series;
    if (this.chartDisplayType == 'times_played')
      series = [
        {
          name: 'Times played',
          type: 'bar',
          data: this.buildTimesPlayedChart(),
        },
      ];
    else
      series = [
        {
          name: 'Solo',
          type: 'bar',
          stack: 'played',
          data: this.buildStackedData('solo'),
        },
        {
          name: 'Rhythm',
          type: 'bar',
          stack: 'played',
          data: this.buildStackedData('rhythm'),
        },
        {
          name: 'Both',
          type: 'bar',
          stack: 'played',
          data: this.buildStackedData('both'),
        },
      ];


    this.options = {
      title: {
        text: 'Songs',
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow',
        },
      },
      dataZoom: [
        {
          type: 'inside',
          yAxisIndex: 0,
          startValue: 0,
          endValue: 10
        }
      ],
      legend: {
        orient: 'vertical',
        left: '1%',
        top: '90%',
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
            type: 'solid',
          },
        },
      },
      yAxis: {
        type: 'category',
        data: this.chartData!.map((x) => x.author + ' - ' + x.name),
      },
      series: series
    };
  }

  private buildTimesPlayedChart(): number[] {
    return this.chartData!.map((x) => x.timesPlayed!);
  }

  private buildStackedData(type: 'solo' | 'rhythm' | 'both'): number[] {
    return this.chartData!.map(song => {
      const dates = song.songsByDateAggregations ?? [];

      switch (type) {
        case 'solo':
          return dates.reduce((sum, x) => sum + (x.soloPlayed ?? 0), 0);

        case 'rhythm':
          return dates.reduce((sum, x) => sum + (x.rhythmPlayed ?? 0), 0);

        case 'both':
          return dates.reduce((sum, x) => sum + (x.bothPlayed ?? 0), 0);

        default:
          return 0;
      }
    });
  }
}
