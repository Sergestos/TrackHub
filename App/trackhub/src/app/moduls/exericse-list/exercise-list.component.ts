import { Component, OnInit } from "@angular/core";
import { ExerciseItem } from "./exercise-list.models";

@Component({
	selector: 'trh-exercise-list',
	templateUrl: './exercise-list.component.html',
	styleUrls: ['./exercise-list.component.css']
})
export class ExerciseListComponent implements OnInit {
	public exercises: ExerciseItem[] = [];

	public ngOnInit(): void {
		this.exercises = [
			{
				totalPlayed: 10,
				playDate: new Date()
			},
			{
				totalPlayed: 60,
				playDate: new Date()
			},		
			{
				totalPlayed: 60,
				playDate: new Date()
			},
			{
				totalPlayed: 60,
				playDate: new Date()
			},
			{
				totalPlayed: 60,
				playDate: new Date()
			},
			{
				totalPlayed: 60,
				playDate: new Date()
			},
			{
				totalPlayed: 60,
				playDate: new Date()
			}
		]
	}	
}