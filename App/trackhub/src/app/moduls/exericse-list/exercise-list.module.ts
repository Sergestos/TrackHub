import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExerciseListComponent } from './exercise-list.component';
import { DetailsExerciseItemComponent } from './details-exercise-card/details-exercise-card.component';
import { DateFilterComponent } from './exercise-filter/exercise-filter.component';

@NgModule({
    declarations: [
        ExerciseListComponent,
        DetailsExerciseItemComponent,
        DateFilterComponent
    ],
    exports: [

    ], 
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: '', component: ExerciseListComponent }
        ])
    ], 
    providers: [
        provideHttpClient(withInterceptorsFromDi())
    ]
})
export class ExerciseListModule { }
