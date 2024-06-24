import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ExerciseComponent } from './exercise/exercise.component';
import { CommitComponent } from './commit.component';
import { CommitService } from '../../providers/services/commit.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        ExerciseComponent,
        CommitComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: '', component: CommitComponent }
        ])
    ],
    providers: [
        CommitService
    ],
    exports: [
        
    ]
})
export class CommitModule { }
