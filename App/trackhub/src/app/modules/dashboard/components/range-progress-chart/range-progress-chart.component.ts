import { Component, OnInit, inject } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { BarChart } from 'echarts/charts';
import {
  GridComponent,
  TitleComponent,
  TooltipComponent,
} from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../../components/button/button.component';
import {
  ByRecordTypeAggregation,
  ExerciseAggregation,
} from '../../models/exercise-aggregation.model';
import * as echarts from 'echarts/core';
import { AlertService } from '../../../../providers/services/alert.service';
import { AggregationService } from '../../services/aggregation.service';

type ChartMetric = 'totalPlayed' | 'timesPlayed';

const SERIES_KEYS = [
  { key: 'warmupAggregation', name: 'Warmup' },
  { key: 'songAggregation', name: 'Song' },
  { key: 'improvisationAggregation', name: 'Improvisation' },
  { key: 'practicalExerciseAggregation', name: 'Exercise' },
  { key: 'composingAggregation', name: 'Composing' },
] as const;

type SeriesKey = (typeof SERIES_KEYS)[number]['key'];

echarts.use([
  BarChart,
  GridComponent,
  TooltipComponent,
  CanvasRenderer,
  TitleComponent,
]);

@Component({
  selector: 'thr-range-progress-chart',
  templateUrl: './range-progress-chart.component.html',
  standalone: true,
  imports: [NgxEchartsDirective, CommonModule, ButtonComponent],
  providers: [provideEchartsCore({ echarts })],
})
export class RangeProgressChartComponent implements OnInit {
  public chartData?: ExerciseAggregation[];
  public options: any;

  public startDate!: Date;
  public endDate!: Date;
  public isDateRangeValid: boolean = true;

  public isApplyFiltersAllowed(): boolean {
    return this.isDateRangeValid;
  }

  private aggregationService = inject(AggregationService);
  private alertService = inject(AlertService);

  private chartDisplayType: ChartMetric = 'totalPlayed';

  constructor() {
    this.endDate = new Date();
    this.startDate = new Date();
    this.startDate.setFullYear(this.startDate.getFullYear() - 1);
  }

  public ngOnInit(): void {
    this.fetchData();
  }

  public onApplyPressed(): void {
    this.fetchData();
  }

  public onTypeChanged($event: Event): void {
    this.chartDisplayType = ($event.target as HTMLSelectElement).value as
      | 'totalPlayed'
      | 'timesPlayed';

    if (this.chartData) {
      this.buildChart();
    }
  }

  public onStartDateChanged($event: Event): void {
    const value = ($event.target as HTMLInputElement).value;
    this.startDate = value ? new Date(value) : this.startDate;

    this.isDateRangeValid = this.checkDateRange();
  }

  public onEndDateChanged($event: Event): void {
    const value = ($event.target as HTMLInputElement).value;
    this.endDate = value ? new Date(value) : this.startDate;

    this.isDateRangeValid = this.checkDateRange();
  }

  private fetchData(): void {
    this.aggregationService
      .getMonthRangeAggregation(this.startDate, this.endDate)
      .subscribe({
        next: (result: ExerciseAggregation[]) => {
          this.chartData = result;
          this.buildChart();
        },
      });
  }

  private checkDateRange(): boolean {
    const from = this.startDate < this.endDate ? this.startDate : this.endDate;
    const to = this.startDate < this.endDate ? this.endDate : this.startDate;

    const plus3Years = new Date(from);
    plus3Years.setFullYear(plus3Years.getFullYear() + 3);

    if (to > plus3Years) {
      this.alertService.show('warning', 'Maximum range is 3 year is exceeded');
      return false;
    }

    if (this.startDate > this.endDate) {
      this.alertService.show(
        'warning',
        'Start Date should not exceed End Date',
      );
      return false;
    }

    return true;
  }

  private buildChart(): void {
    this.options = buildStackedBarOptions(
      this.chartData!,
      this.chartDisplayType,
    );
  }
}

export function buildStackedBarOptions(
  months: ExerciseAggregation[],
  metric: ChartMetric = 'totalPlayed',
  radius = 8,
) {
  const xLabels = months.map(
    (m) => `${m.year}-${String(m.month).padStart(2, '0')}`,
  );

  const valuesBySeries: Record<SeriesKey, number[]> = {
    warmupAggregation: [],
    songAggregation: [],
    improvisationAggregation: [],
    practicalExerciseAggregation: [],
    composingAggregation: [],
  };

  for (const m of months) {
    for (const s of SERIES_KEYS) {
      const agg = (m as any)[s.key] as
        | ByRecordTypeAggregation
        | null
        | undefined;
      valuesBySeries[s.key].push(agg ? ((agg as any)[metric] ?? 0) : 0);
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
        borderRadius:
          topSeriesIndexPerMonth[i] === sIdx ? [radius, radius, 0, 0] : 0,
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
          .filter((p) => p?.value > 0)
          .map((p) => `${p.marker} ${p.seriesName}: ${p.value}`)
          .join('<br/>');
        const total = params.reduce(
          (sum, p) => sum + (Number(p.value) || 0),
          0,
        );
        return [header, lines, `<br/><b>Total:</b> ${total}`]
          .filter(Boolean)
          .join('<br/>');
      },
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
      type: 'value',
      splitLine: {
        show: true,
        lineStyle: {
          color: 'rgba(255,255,255,0.15)',
          width: 1,
          type: 'solid',
        },
      },
    },
    series,
  };
}
