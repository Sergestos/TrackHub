import { Component, input } from '@angular/core';

@Component({
  selector: 'trh-linear-trend',
  templateUrl: './linear-trend.component.html',
  standalone: true,
})
export class LinearTrendComponent {
  public description = input.required<string>();
  public currentIndicator = input.required<number>();
  public previousIndicator = input.required<number>();
}
