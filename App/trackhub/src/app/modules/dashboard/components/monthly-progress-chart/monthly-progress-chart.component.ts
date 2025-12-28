import { Component, input } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';

import { PieChart } from 'echarts/charts';
import { TooltipComponent, LegendComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { ExerciseAggregation } from '../../models/exercise-aggregation.model';

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

  public getHeader(): string {
    return this.chartHeader() ?? 'Monthly Chart';
  }

  public options = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      top: '95%',
      left: 'center',
    },
    series: [
      {
        name: 'Access From',
        type: 'pie',
        radius: ['40%', '70%'],
        avoidLabelOverlap: false,
        padAngle: 4,
        itemStyle: {
          borderRadius: 10
        },
        label: {
          show: false,
          position: 'center'
        },
        labelLine: {
          show: false
        },
        data: [
          { value: 1048, name: 'Search Engine' },
          { value: 735, name: 'Direct' },
          { value: 580, name: 'Email' },
          { value: 484, name: 'Union Ads' },
          { value: 300, name: 'Video Ads' }
        ]
      }
    ]
  };


  mergeOptions: any = null;
}