import { Component, effect, input } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { PieChart } from 'echarts/charts';
import { TooltipComponent, LegendComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { ExerciseAggregation } from '../../models/exercise-aggregation.model';
import * as echarts from 'echarts/core';

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
  imports: [NgxEchartsDirective],
  providers: [
    provideEchartsCore({ echarts })
  ]
})
export class MonthlyProgressChartComponent {
  public chartHeader = input<string>();
  public monthlyStatistics = input<ExerciseAggregation | null>();

  public options: any;
  public mergeOptions: any = null;

  public chartDisplayType: 'record' | 'play' = 'record';

  public getHeader(): string {
    return this.chartHeader() ?? 'Monthly Chart';
  }

  constructor() {
    effect(() => {
      if (this.monthlyStatistics()) {
        this.buildChart();
      }
    })
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'record' | 'play';

    if (this.monthlyStatistics()) {
      this.buildChart();
    }
  }

  private buildData(): {
    value: number,
    name: string
  }[] {
    if (this.chartDisplayType == 'record') {
      return [
        {
          value: this.monthlyStatistics()?.warmup_aggregation?.total_played ?? 0,
          name: 'warmup'
        },
        {
          value: this.monthlyStatistics()?.song_aggregation?.total_played ?? 0,
          name: 'songs'
        },
        {
          value: this.monthlyStatistics()?.composing_aggregation?.total_played ?? 0,
          name: 'composition'
        },
        {
          value: this.monthlyStatistics()?.exercise_aggregation?.total_played ?? 0,
          name: 'exercises'
        },
        {
          value: this.monthlyStatistics()?.improvisation_aggregation?.total_played ?? 0,
          name: 'improvisation'
        },
      ];
    } else {
      return [
        {
          value: this.monthlyStatistics()?.rhythm_aggregation?.total_played ?? 0,
          name: 'rhythm'
        },
        {
          value: this.monthlyStatistics()?.solo_aggregation?.total_played ?? 0,
          name: 'solo'
        },
        {
          value: this.monthlyStatistics()?.both_aggregation?.total_played ?? 0,
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