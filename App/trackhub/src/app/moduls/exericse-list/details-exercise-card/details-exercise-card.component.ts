import { Component, Input, OnInit } from "@angular/core";
import { ExerciseListService } from "../../../providers/services/exercise-list.service";
import { Observable, of } from "rxjs";
import { RecordDetailsItem } from "../exercise-list.models";

@Component({
	selector: 'trh-details-exercise-card',
	templateUrl: './details-exercise-card.component.html',
	styleUrls: ['./details-exercise-card.component.css']
})
export class DetailsExerciseItemComponent implements OnInit {	
	@Input() 
	public exerciseId!: string;

	@Input()
	public exerciseDetailsModels?: RecordDetailsItem[] | null;

	public exerciseDetails$: Observable<RecordDetailsItem[]> = new Observable<RecordDetailsItem[]>();

	constructor(private exerciseListService: ExerciseListService) { }

	public ngOnInit(): void {
		if (this.exerciseDetailsModels) {
			this.exerciseDetails$ = of(this.exerciseDetailsModels);
		}
	}
}