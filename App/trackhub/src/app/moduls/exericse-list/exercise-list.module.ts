import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExercisePageComponent } from './exercise-page/exercise-page.component';
import { DetailsExerciseItemComponent } from './details-exercise-card/details-exercise-card.component';
import { DateFilterComponent } from './exercise-filter/exercise-filter.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
    declarations: [
        ExercisePageComponent,
        DetailsExerciseItemComponent,
        DateFilterComponent
    ],
    exports: [

    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        RouterModule.forChild([
            { path: '', component: ExercisePageComponent }
        ])
    ],
    providers: [
        provideHttpClient(withInterceptorsFromDi())
    ]
})
export class ExerciseListModule { }
