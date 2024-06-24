import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExerciseListComponent } from './exercise-list.component';
import { ExerciseListItemComponent } from './exercise-list-item/exercise-list-item.component';

@NgModule({
    declarations: [
        ExerciseListComponent,
        ExerciseListItemComponent
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
