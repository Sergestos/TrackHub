import { Component, OnInit, inject, signal } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { PieChart } from 'echarts/charts';
import { TooltipComponent, LegendComponent, TitleComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { ExerciseAggregation } from '../../models/exercise-aggregation.model';
import * as echarts from 'echarts/core';
import { ChartMonthPickerComponent } from './month-picker/month-picker.component';
import { AggregationService } from '../../services/aggregation.service';

echarts.use([
  PieChart,
  TooltipComponent,
  LegendComponent,
  CanvasRenderer,
  TitleComponent 
]);

@Component({
  selector: 'trh-monthly-progress-chart',
  templateUrl: './monthly-progress-chart.component.html',
  standalone: true,
  imports: [NgxEchartsDirective, ChartMonthPickerComponent],
  providers: [
    provideEchartsCore({ echarts })
  ]
})
export class MonthlyProgressChartComponent implements OnInit {
  public chartData = signal<ExerciseAggregation | null>(null);

  public options: any;
  public mergeOptions: any = null;

  public chartDisplayType: 'record' | 'play' = 'record';
  public isMonthPickerShown: boolean = false;

  private selectedDate: Date = new Date();

  private aggregationService = inject(AggregationService);

  public getHeader(): string {
    const now = new Date();
    if (this.isSameMonth(this.selectedDate, now)) {
      return 'Current month';
    } else if (this.isLastMonth(this.selectedDate, now)) {
      return 'Last month';
    } else {
      return `${this.selectedDate.getFullYear()}/${this.selectedDate.getMonth() + 1}`;
    }
  }

  public ngOnInit(): void {
    this.aggregationService.getMonthAggregation(this.selectedDate)
      .subscribe({
        next: (result: ExerciseAggregation) => {
          if (result) {
            this.chartData.set(result);
            this.buildChart();
          } else {
            this.chartData.set(null);
          }
        }
      })
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'record' | 'play';

    if (this.chartData()) {
      this.buildChart();
    }
  }

  public onMonthPickerClicked(): void {
    this.isMonthPickerShown = !this.isMonthPickerShown;
  }

  public onMonthSelected($event: Date) {
    this.isMonthPickerShown = false;
    this.selectedDate = $event;

    this.aggregationService
      .getMonthAggregation(this.selectedDate)
      .subscribe({
        next: (result: ExerciseAggregation) => {
          if (result) {
            this.chartData.set(result);
            this.buildChart();
          } else {
            this.chartData.set(null);
          }
        }
      });
  }

  private isSameMonth(a: Date, b: Date): boolean {
    return a.getFullYear() == b.getFullYear() &&
      a.getMonth() == b.getMonth();
  }

  private isLastMonth(a: Date, b: Date): boolean {
    return Math.abs(
      (a.getFullYear() * 12 + a.getMonth()) -
      (b.getFullYear() * 12 + b.getMonth())
    ) === 1;
  }

  private buildData(): {
    value: number,
    name: string
  }[] {
    if (this.chartDisplayType == 'record') {
      return [
        {
          value: this.chartData()?.warmupAggregation?.totalPlayed ?? 0,
          name: 'warmup'
        },
        {
          value: this.chartData()?.songAggregation?.totalPlayed ?? 0,
          name: 'songs'
        },
        {
          value: this.chartData()?.composingAggregation?.totalPlayed ?? 0,
          name: 'composition'
        },
        {
          value: this.chartData()?.practicalExerciseAggregation?.totalPlayed ?? 0,
          name: 'exercises'
        },
        {
          value: this.chartData()?.improvisationAggregation?.totalPlayed ?? 0,
          name: 'improvisation'
        },
      ];
    } else {
      return [
        {
          value: this.chartData()?.rhythmAggregation?.totalPlayed ?? 0,
          name: 'rhythm'
        },
        {
          value: this.chartData()?.soloAggregation?.totalPlayed ?? 0,
          name: 'solo'
        },
        {
          value: this.chartData()?.bothAggregation?.totalPlayed ?? 0,
          name: 'rhythm + solo'
        },
      ]
    }
  }

  private buildChart(): void {
    this.options = {
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        left: '2.5%',
        top: '5%'
      },
      series: [
        {
          type: 'pie',
          radius: ['40%', '70%'],
          avoidLabelOverlap: false,
          padAngle: 0,
          itemStyle: {
            borderRadius: 3
          },
          label: {
            show: false,
            position: 'center'
          },
          labelLine: {
            show: false
          },
          data: this.buildData()
        }
      ]
    };
  }
}