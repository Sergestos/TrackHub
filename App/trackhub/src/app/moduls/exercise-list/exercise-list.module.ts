import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExercisePageComponent } from './exercise-page/exercise-page.component';
import { DetailsExerciseItemComponent } from './exercise-details-card/exercise-details-card.component';
import { FilterComponent } from './exercise-filter/exercise-filter.component';
import { MatButtonModule } from '@angular/material/button';
import { MonthNamePipe } from "../../providers/pipes/month.pipe";
import { ExerciseDateFilterComponent } from './exercise-filter/exercise-date-filter/exercise-date-filter.component';

@NgModule({
  declarations: [
    ExercisePageComponent,
    ExerciseDateFilterComponent,
    DetailsExerciseItemComponent,
    FilterComponent
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
    ]),
    MonthNamePipe
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi())
  ]
})
export class ExerciseListModule { }
