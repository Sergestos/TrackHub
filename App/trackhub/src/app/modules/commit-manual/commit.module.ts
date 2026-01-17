import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CommitExerciseComponent } from './components/commit-exercise/commit-exercise.component';
import { CommitComponent } from './components/commit.component';
import { CommitService } from './services/commit.service';
import { ButtonComponent } from '../../components/button/button.component';
import { SuggestionService } from './services/suggestion.service';

@NgModule({
  declarations: [CommitExerciseComponent, CommitComponent],
  exports: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule.forChild([{ path: '', component: CommitComponent }]),
  ],
  providers: [
    CommitService,
    SuggestionService,
    provideHttpClient(withInterceptorsFromDi()),
  ],
})
export class CommitModule {}
