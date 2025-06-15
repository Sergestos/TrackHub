import { Component, inject, input, OnInit, output } from "@angular/core";
import { ExerciseListService } from "../exercise-list.service";
import { FilterDateModel, FiltersModel as FilterModel } from "../exercise-list.models";

@Component({
    selector: 'trackhub-exercise-filter',
    templateUrl: './exercise-filter.component.html',
    standalone: false
})
export class FilterComponent implements OnInit {
    public userFirstYear!: number;

    public filter = input<FilterModel>();

    public onDateChangedEmmiter = output<FilterDateModel>();
    public onExpandAllEmitter = output<boolean>();
    public onShowPlayedOnlyEmitter = output<boolean>();

    private exerciseListService = inject(ExerciseListService);

    public ngOnInit(): void {
        this.exerciseListService.getUserExerciseProfile()
            .subscribe(item => {
                this.userFirstYear = item.getFullYear();
                this.filter()!.dateFilter = {
                    year: new Date().getFullYear(),
                    month: new Date().getMonth() + 1
                }
            })
    }

    public onExpandAllClicked(event: any): void {
        this.onExpandAllEmitter.emit(event.target.checked);
    }

    public onShowPlayedOnlyClicked(event: any): void {
        this.onShowPlayedOnlyEmitter.emit(event.target.checked);
    }

    public onFilterDateChanges(event: any) {
        this.onDateChangedEmmiter.emit(event);
    }
}