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
  public chartData = input<ExerciseAggregation | null>();

  public options: any;
  public mergeOptions: any = null;

  public chartDisplayType: 'record' | 'play' = 'record';

  public getHeader(): string {
    return this.chartHeader() ?? 'Monthly Chart';
  }

  constructor() {
    effect(() => {
      if (this.chartData()) {
        this.buildChart();
      }
    })
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'record' | 'play';

    if (this.chartData()) {
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
          value: this.chartData()?.warmup_aggregation?.total_played ?? 0,
          name: 'warmup'
        },
        {
          value: this.chartData()?.song_aggregation?.total_played ?? 0,
          name: 'songs'
        },
        {
          value: this.chartData()?.composing_aggregation?.total_played ?? 0,
          name: 'composition'
        },
        {
          value: this.chartData()?.exercise_aggregation?.total_played ?? 0,
          name: 'exercises'
        },
        {
          value: this.chartData()?.improvisation_aggregation?.total_played ?? 0,
          name: 'improvisation'
        },
      ];
    } else {
      return [
        {
          value: this.chartData()?.rhythm_aggregation?.total_played ?? 0,
          name: 'rhythm'
        },
        {
          value: this.chartData()?.solo_aggregation?.total_played ?? 0,
          name: 'solo'
        },
        {
          value: this.chartData()?.both_aggregation?.total_played ?? 0,
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