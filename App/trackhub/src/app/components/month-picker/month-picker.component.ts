import { Component, effect, forwardRef, input } from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { MonthNamePipe } from '../../providers/pipes/month.pipe';
import { MonthPickerModel } from './month-picker.model';

@Component({
  selector: 'trh-month-picker',
  templateUrl: './month-picker.component.html',
  imports: [FormsModule, MonthNamePipe],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MonthPickerComponent),
      multi: true,
    },
  ],
  standalone: true,
})
export class MonthPickerComponent implements ControlValueAccessor {
  public selectedYear: number | undefined;
  public selectedMonth: number | undefined;

  public startYear = input<number>();

  public yearSelectValues: number[] = [];
  public monthSelectValues: number[] = [];

  private onChange = (_: any) => {};
  private onTouched = () => {};

  constructor() {
    effect(() => {
      const start = this.startYear()!;
      const count = new Date().getFullYear() - start + 1;
      for (let i = 0; i < count; i++) {
        this.yearSelectValues.push(start + i);
      }

      this.monthSelectValues = Array.from({ length: 12 }, (_, i) => i + 1);
    });
  }

  public writeValue(obj: any): void {
    const model: MonthPickerModel = obj;

    if (model) {
      if (model.year) {
        this.selectedYear = model.year;
      }

      if (model.month) {
        this.selectedMonth = model.month;
      }
    }
  }

  public registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  public updateValue(): void {
    this.onTouched();
    this.onChange({
      year: this.selectedYear,
      month: this.selectedMonth,
    });
  }
}
