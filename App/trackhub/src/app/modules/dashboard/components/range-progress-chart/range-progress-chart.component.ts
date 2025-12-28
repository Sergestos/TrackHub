import { Component, effect, inject, input, output } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { BarChart } from 'echarts/charts';
import { GridComponent, TooltipComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../../components/button/button.component';
import { ByRecordTypeAggregation, ExerciseAggregation } from '../../models/exercise-aggregation.model';
import * as echarts from 'echarts/core';
import { AlertService } from '../../../../providers/services/alert.service';

export type RangeRequest = { start: Date; end: Date };

type ChartMetric = 'total_played' | 'times_played';

const SERIES_KEYS = [
  { key: 'warmup_aggregation', name: 'Warmup' },
  { key: 'song_aggregation', name: 'Song' },
  { key: 'improvisation_aggregation', name: 'Improvisation' },
  { key: 'exercise_aggregation', name: 'Exercise' },
  { key: 'composing_aggregation', name: 'Composing' },
] as const;

type SeriesKey = typeof SERIES_KEYS[number]['key'];

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
  imports: [
    NgxEchartsDirective,
    CommonModule,
    ButtonComponent],
  providers: [provideEchartsCore({ echarts })],
})
export class RangeProgressChartComponent {
  private chartDisplayType: ChartMetric = 'total_played';

  public chartData = input<ExerciseAggregation[] | null>();
  public chartHeader = input<string>();

  public applyClicked = output<RangeRequest>();

  private alertService = inject(AlertService);

  public options: any;

  public startDate!: Date;
  public endDate!: Date;

  public getHeader(): string {
    return this.chartHeader() ?? 'Range Chart';
  }

  constructor() {
    this.endDate = new Date();
    this.startDate = new Date();
    this.startDate.setMonth(this.startDate.getMonth() - 1);
    effect(() => {
      if (this.chartData()) {
        this.buildChart();
      }
    })
  }

  public onApplyPressed(): void {
    this.alertService.show('success', 'hello');

    if (this.chartData()) {
      this.buildChart();
    }
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as 'total_played' | 'times_played';

    if (this.chartData()) {
      this.buildChart();
    }
  }

  public onStartDateChanged($event: Event): void {
    const value = ($event.target as HTMLInputElement).value;
    this.startDate = value ? new Date(value) : this.startDate;
  }

  public onEndDateChanged($event: Event): void {
    const value = ($event.target as HTMLInputElement).value;
    this.endDate = value ? new Date(value) : this.startDate;
  }

  private buildChart(): void {
    this.options = buildStackedBarOptions(this.chartData()!, this.chartDisplayType);
  }
}

export function buildStackedBarOptions(
  months: ExerciseAggregation[],
  metric: ChartMetric = 'total_played',
  radius = 8
) {
  const xLabels = months.map(m =>
    `${m.aggregation_year}-${String(m.aggregation_month).padStart(2, '0')}`
  );

  const valuesBySeries: Record<SeriesKey, number[]> = {
    warmup_aggregation: [],
    song_aggregation: [],
    improvisation_aggregation: [],
    exercise_aggregation: [],
    composing_aggregation: [],
  };

  for (const m of months) {
    for (const s of SERIES_KEYS) {
      const agg = (m as any)[s.key] as ByRecordTypeAggregation | null | undefined;
      valuesBySeries[s.key].push(agg ? (agg as any)[metric] ?? 0 : 0);
    }
  }

  const topSeriesIndexPerMonth = xLabels.map((_, i) => {
    for (let s = SERIES_KEYS.length - 1; s >= 0; s--) {
      const key = SERIES_KEYS[s].key;
      if ((valuesBySeries[key][i] ?? 0) > 0) return s;
    }
    return -1;
  });

  const series = SERIES_KEYS.map((s, sIdx) => {
    const data = valuesBySeries[s.key].map((v, i) => ({
      value: v,
      itemStyle: {
        borderRadius: topSeriesIndexPerMonth[i] === sIdx ? [radius, radius, 0, 0] : 0,
      },
    }));

    return {
      name: s.name,
      type: 'bar',
      stack: 'total',
      barWidth: 22,
      emphasis: { focus: 'series' },
      data,
    };
  });

  return {
    tooltip: {
      trigger: 'axis',
      axisPointer: { type: 'shadow' },
      formatter: (params: any[]) => {
        const header = `<b>${params?.[0]?.axisValue ?? ''}</b>`;
        const lines = params
          .filter(p => p?.value > 0)
          .map(p => `${p.marker} ${p.seriesName}: ${p.value}`)
          .join('<br/>');
        const total = params.reduce((sum, p) => sum + (Number(p.value) || 0), 0);
        return [header, lines, `<br/><b>Total:</b> ${total}`].filter(Boolean).join('<br/>');
      }
    },
    legend: { top: 0 },
    grid: { top: 40, left: 40, right: 20, bottom: 40, containLabel: true },
    xAxis: {
      type: 'category',
      data: xLabels,
      axisTick: { alignWithLabel: true },
      triggerEvent: true,
    },
    yAxis: {
      type: 'value', splitLine: {
        show: true,
        lineStyle: {
          color: 'rgba(255,255,255,0.15)',
          width: 1,
          type: 'solid'
        }
      }
    },
    series,
  };
}
