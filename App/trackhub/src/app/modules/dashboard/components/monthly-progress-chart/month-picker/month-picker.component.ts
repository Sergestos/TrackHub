import { Component, output } from "@angular/core";
import { ButtonComponent } from "../../../../../components/button/button.component";
import { MonthPickerComponent } from "../../../../../components/month-picker/month-picker.component";
import { MonthPickerModel } from "../../../../../components/month-picker/month-picker.model";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'trh-chart-month-picker',
  templateUrl: './month-picker.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonComponent,
    MonthPickerComponent],
})
export class ChartMonthPickerComponent {
  // TODO fix 2023 magic number - get it from some store
  public startYear: number = 2023;
  public monthModel!: MonthPickerModel;

  public monthSelected = output<Date>();

  constructor() {
    const now = new Date();

    this.monthModel = new MonthPickerModel(now.getFullYear(), now.getMonth() + 1);
  }

  public onCurrentMonthClicked(): void {
    this.monthSelected.emit(new Date());
  }

  public onLastMonthClicked(): void {
    const now = new Date();
    const lastMonthStart = new Date(now.getFullYear(), now.getMonth() - 1, 1);

    this.monthSelected.emit(lastMonthStart);
  }

  public onApplyClicked(): void {
    const date = new Date(this.monthModel.year!, this.monthModel.month! - 1, 1);    

    this.monthSelected.emit(date);
  }
}