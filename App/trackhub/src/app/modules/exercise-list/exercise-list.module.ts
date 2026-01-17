import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExercisePageComponent } from './components/exercise-page/exercise-page.component';
import { DetailsExerciseItemComponent } from './components/exercise-details-card/exercise-details-card.component';
import { FilterComponent } from './components/exercise-filter/exercise-filter.component';
import { MatButtonModule } from '@angular/material/button';
import { MonthPickerComponent } from '../../components/month-picker/month-picker.component';

@NgModule({
  declarations: [
    ExercisePageComponent,
    DetailsExerciseItemComponent,
    FilterComponent,
  ],
  exports: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    RouterModule.forChild([{ path: '', component: ExercisePageComponent }]),
    MonthPickerComponent,
  ],
  providers: [provideHttpClient(withInterceptorsFromDi())],
})
export class ExerciseListModule {}
