import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExerciseListComponent } from './exercise-list.component';
import { DetailsExerciseItemComponent } from './details-exercise-card/details-exercise-card.component';

@NgModule({
    declarations: [
        ExerciseListComponent,
        DetailsExerciseItemComponent 
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: '', component: ExerciseListComponent }
        ])
    ],
    providers: [
        
    ],
    exports: [
        
    ]
})
export class ExerciseListModule { }
