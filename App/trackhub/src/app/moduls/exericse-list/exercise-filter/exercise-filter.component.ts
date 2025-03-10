import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { ExerciseListService } from "../exercise-list.service";
import { FiltersModel, UserExerciseProfile } from "../exercise-list.models";
import { ThisReceiver } from "@angular/compiler";

@Component({
    selector: 'trackhub-exercise-filter',
    templateUrl: './exercise-filter.component.html'
})
export class DateFilterComponent implements OnInit {
    private userExerciseProfile?: UserExerciseProfile;

    public filter!: FiltersModel;
    public userProfileYears: number[] = [];

    @Output()
    public onDateChangedEmmiter: EventEmitter<FiltersModel> = new EventEmitter<FiltersModel>();

    @Output()
    public onExpandAllEmitter: EventEmitter<boolean> = new EventEmitter<boolean>();

    @Output()
    public onShowNonPlayedEmitter: EventEmitter<boolean> = new EventEmitter<boolean>();

    constructor(private exerciseListService: ExerciseListService) {
        this.filter = {
            year: 2024,
            month: new Date().getMonth() + 1,
            showNonPlayed: false,
            showExpanded: true
        };
    }

    public ngOnInit(): void {
        this.exerciseListService.getUserExerciseProfile()
            .subscribe(item => {
                let currentYear = new Date().getFullYear();
                for (let year = item.firstExerciseDate.getFullYear(); year <= currentYear; year++) {
                    this.userProfileYears.push(year);
                }
            })
    }

    public onExpandAllClicked(event: any): void {
        this.onExpandAllEmitter.emit(event.target.checked);
    }

    public onShowNonPlayedClicked(event: any): void {
        this.onShowNonPlayedEmitter.emit(event.target.checked);
    }

    public onMonthClick(monthIndex: number): void {
        this.filter.month = monthIndex;
        this.onDateChangedEmmiter.emit(this.filter);
    }

    public onYearSelected(year: number): void {
        this.onDateChangedEmmiter.emit(this.filter);
    }
}