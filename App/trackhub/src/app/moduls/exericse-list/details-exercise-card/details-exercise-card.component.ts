import { Component, Input, OnInit } from "@angular/core";
import { Observable, of } from "rxjs";
import { RecordDetailsItem } from "../exercise-list.models";

@Component({
	selector: 'trackhub-details-exercise-card',
	templateUrl: './details-exercise-card.component.html',
	styleUrls: ['./details-exercise-card.component.scss']
})
export class DetailsExerciseItemComponent implements OnInit {
	@Input()
	public exerciseId!: string;

	@Input()
	public exerciseDetailsModels?: RecordDetailsItem[] | null;

	public exerciseDetails$: Observable<RecordDetailsItem[]> = new Observable<RecordDetailsItem[]>();

	public ngOnInit(): void {
		if (this.exerciseDetailsModels) {
			this.exerciseDetails$ = of(this.exerciseDetailsModels);
		}
	}
}