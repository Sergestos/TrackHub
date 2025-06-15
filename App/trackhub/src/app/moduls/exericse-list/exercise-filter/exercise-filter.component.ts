import { Component, inject, OnInit, output } from "@angular/core";
import { ExerciseListService } from "../exercise-list.service";
import { FilterDateModel } from "../exercise-list.models";

@Component({
    selector: 'trackhub-exercise-filter',
    templateUrl: './exercise-filter.component.html',
    standalone: false
})
export class FilterComponent implements OnInit {
    public userFirstYear!: number;
    public dateFilter: FilterDateModel | undefined;

    private exerciseListService = inject(ExerciseListService);

    public onDateChangedEmmiter = output<FilterDateModel>();
    public onExpandAllEmitter = output<boolean>();
    public onShowNonPlayedEmitter = output<boolean>();

    public ngOnInit(): void {
        this.exerciseListService.getUserExerciseProfile()
            .subscribe(item => {
                this.userFirstYear = item.getFullYear();
                this.dateFilter = {
                    year: new Date().getFullYear(),
                    month: new Date().getMonth()
                }
            })
    }

    public onExpandAllClicked(event: any): void {
        this.onExpandAllEmitter.emit(event.target.checked);
    }

    public onShowNonPlayedClicked(event: any): void {
        this.onShowNonPlayedEmitter.emit(event.target.checked);
    }

    public onFilterDateChanges(event: any) {
        this.onDateChangedEmmiter.emit(event);
    }
}