import { Component, Input } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';

import { BarChart } from 'echarts/charts';
import { GridComponent, TooltipComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';

echarts.use([
  BarChart,
  GridComponent,
  TooltipComponent,
  CanvasRenderer
]);

@Component({
  selector: 'thr-range-progress-chart',
  templateUrl: './range-progress-chart.component.html',
  standalone: true,
  imports: [NgxEchartsDirective],
  providers: [provideEchartsCore({ echarts })],
})
export class RangeProgressChartComponent {
  @Input() labels: string[] = [];
  @Input() values: number[] = [];

  options = {
    tooltip: {
      trigger: 'axis'
    },
    grid: {
      left: 40,
      right: 20,
      top: 20,
      bottom: 40
    },
    xAxis: {
      type: 'category',
      data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
    },
    yAxis: {
      type: 'value'
    },
    series: [
      {
        type: 'bar',
        name: 'Minutes played',
        data: [120, 200, 150, 80, 70, 110, 160],
        barMaxWidth: 40
      }
    ]
  };
}
