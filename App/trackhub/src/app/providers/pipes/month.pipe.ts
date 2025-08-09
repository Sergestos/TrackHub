import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'monthName' })
export class MonthNamePipe implements PipeTransform {
  private months = [
    'January', 'February', 'March', 'April',
    'May', 'June', 'July', 'August',
    'September', 'October', 'November', 'December'
  ];

  transform(value: number): string {
    return this.months[value - 1] || '';
  }
}