import { Component } from "@angular/core";
import { ExerciseItem } from "./exercise-list.models";

@Component({
	selector: 'trh-exercise-list',
	templateUrl: './exercise-list.component.html',
	styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent {	
	public exercises: ExerciseItem[] = [];
}