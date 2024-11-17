import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ExerciseComponent } from './exercise/exercise.component';
import { CommitComponent } from './commit.component';
import { CommitService } from './commit.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        ExerciseComponent,
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
