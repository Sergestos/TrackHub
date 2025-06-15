import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { ExerciseListService } from "../exercise-list.service";
import { FilterDateModel, FiltersModel } from "../exercise-list.models";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'trackhub-exercise-filter',
    templateUrl: './exercise-filter.component.html',
    standalone: false
})
export class DateFilterComponent implements OnInit {
    public filter!: FiltersModel;
    public userFirstYear!: number;
    public dateFilter: FilterDateModel | undefined;

    @Output()
    public onDateChangedEmmiter: EventEmitter<FiltersModel> = new EventEmitter<FiltersModel>();

    @Output()
    public onExpandAllEmitter: EventEmitter<boolean> = new EventEmitter<boolean>();

    @Output()
    public onShowNonPlayedEmitter: EventEmitter<boolean> = new EventEmitter<boolean>();

    constructor(
        private exerciseListService: ExerciseListService,
        private activeRoute: ActivatedRoute,
        private router: Router) {

        /*     this.filter = {
                 year: new Date().getFullYear(),
                 month: new Date().getMonth() + 1,
                 showNonPlayed: true,
                 showExpanded: true
             };*/
    }

    public ngOnInit(): void {
        this.exerciseListService.getUserExerciseProfile()
            .subscribe(item => {
                this.userFirstYear = item.getFullYear();
                this.dateFilter = {
                    year: 2025,
                    month: 4
                }

            })
    }

    public onExpandAllClicked(event: any): void {
        this.onExpandAllEmitter.emit(event.target.checked);
    }

    public onShowNonPlayedClicked(event: any): void {
        this.onShowNonPlayedEmitter.emit(event.target.checked);
    }

    public onFilterDateChanges($event: any) {
        console.log($event);
    }

    public onMonthClick(monthIndex: number): void {
        //   this.filter.month = monthIndex;
        this.onDateChangedEmmiter.emit(this.filter);
    }

    public onYearSelected($event: any): void {
        this.onDateChangedEmmiter.emit(this.filter);
    }
}