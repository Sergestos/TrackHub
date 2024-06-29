import { Component, OnInit } from "@angular/core";
import { ExerciseListService } from "../../../providers/services/exercise-list.service";
import { UserExerciseProfile } from "../exercise-list.models";

@Component({
	selector: 'trh-date-filter',
	templateUrl: './date-filter.component.html',
	styleUrls: ['./date-filter.component.css']
})
export class DateFilterComponent implements OnInit {
    private userExerciseProfile?: UserExerciseProfile;

    public years: number[] = [];

    constructor(
        private exerciseListService: ExerciseListService) {
        
    }

    public ngOnInit(): void {
        this.exerciseListService.getUserExerciseProfile()
            .subscribe(item => {
                let currentYear = new Date().getFullYear();
                for (let year = item.firstExerciseDate.getFullYear(); year <= currentYear; year++) {
                    this.years.push(year);  
                }
            })
    }

    public onMonthClick(monyhIndex: number): void {

    }
}