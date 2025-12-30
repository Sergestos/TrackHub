import { Component, OnInit, effect, inject, input } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { PieChart } from 'echarts/charts';
import { TooltipComponent, LegendComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { ExerciseAggregation } from '../../models/exercise-aggregation.model';
import * as echarts from 'echarts/core';
import { ChartMonthPickerComponent } from './month-picker/month-picker.component';
import { AggregationService } from '../../services/aggregation.service';

echarts.use([
  PieChart,
  TooltipComponent,
  LegendComponent,
  CanvasRenderer
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
  public chartData?: ExerciseAggregation;

  public options: any;
  public mergeOptions: any = null;

  public chartDisplayType: 'record' | 'play' = 'record';
  public isMonthPickerShown: boolean = false;

  private selectedDate: Date = new Date();

  private aggregationService = inject(AggregationService);

  public getHeader(): string {
    const now = new Date();
    if (this.selectedDate.getFullYear() == now.getFullYear() &&
      this.selectedDate.getMonth() == now.getMonth()) {
      return 'Current month';
    } else if (Math.abs(
      (this.selectedDate.getFullYear() * 12 + this.selectedDate.getMonth()) -
      (now.getFullYear() * 12 + now.getMonth())
    ) === 1) {
      return 'Last month';
    } else {
      return `${this.selectedDate.getFullYear()}/${this.selectedDate.getMonth() + 1}`;
    }
  }

  public ngOnInit(): void {
    this.aggregationService.getCurrentMonthAggregation()
      .subscribe({
        next: (result) => {
          if (result) {
            this.chartData = result;
            this.buildChart();
          }
        }
      })
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'record' | 'play';

    if (this.chartData) {
      this.buildChart();
    }
  }

  public onMonthPickerClicked(): void {
    this.isMonthPickerShown = !this.isMonthPickerShown;
  }

  public onMonthSelected($event: Date) {
    this.isMonthPickerShown = false;
    this.selectedDate = $event;
  }

  private buildData(): {
    value: number,
    name: string
  }[] {
    if (this.chartDisplayType == 'record') {
      return [
        {
          value: this.chartData?.warmup_aggregation?.total_played ?? 0,
          name: 'warmup'
        },
        {
          value: this.chartData?.song_aggregation?.total_played ?? 0,
          name: 'songs'
        },
        {
          value: this.chartData?.composing_aggregation?.total_played ?? 0,
          name: 'composition'
        },
        {
          value: this.chartData?.exercise_aggregation?.total_played ?? 0,
          name: 'exercises'
        },
        {
          value: this.chartData?.improvisation_aggregation?.total_played ?? 0,
          name: 'improvisation'
        },
      ];
    } else {
      return [
        {
          value: this.chartData?.rhythm_aggregation?.total_played ?? 0,
          name: 'rhythm'
        },
        {
          value: this.chartData?.solo_aggregation?.total_played ?? 0,
          name: 'solo'
        },
        {
          value: this.chartData?.both_aggregation?.total_played ?? 0,
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