import { Component, Input, OnInit } from "@angular/core";
import { ExerciseListService } from "../../../providers/services/exercise-list.service";
import { Observable } from "rxjs";
import { ExerciseDetails } from "../exercise-list.models";

@Component({
	selector: 'trh-details-exercise-card',
	templateUrl: './details-exercise-card.component.html',
	styleUrls: ['./details-exercise-card.component.css']
})
export class DetailsExerciseItemComponent implements OnInit {	
	@Input() exerciseId!: string;

	public exerciseDetails$: Observable<ExerciseDetails[]> = new Observable<ExerciseDetails[]>();

	constructor(private exerciseListService: ExerciseListService) { }

	public ngOnInit(): void {
		this.exerciseDetails$ = this.exerciseListService.getExerciseDetails(this.exerciseId);
	}
}