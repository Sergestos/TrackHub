import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommitComponent } from './components/commit.component';
import { CommitService } from './services/commit.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CommitExerciseComponent } from './components/commit-exercise/commit-exercise.component';

@NgModule({
  declarations: [
    CommitExerciseComponent,
    CommitComponent
  ],
  exports: [

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: CommitComponent }
    ])
  ],
  providers: [
    CommitService,
    provideHttpClient(withInterceptorsFromDi())
  ]
})
export class CommitModule { }
