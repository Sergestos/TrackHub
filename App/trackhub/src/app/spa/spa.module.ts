import { NgModule } from '@angular/core';
import { SpaContainerComponent } from './spa-container/spa-container.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { SpaRoutingModule } from './spa-routing.module';
import { ExerciseComponent } from './commit/exercise/exercise.component';
import { CommitComponent } from './commit/commit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommitService } from './commit/commit.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    declarations: [
        SpaContainerComponent,
        ExerciseComponent,
        CommitComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterModule,
        SpaRoutingModule
    ],
    providers: [

    ],
    exports: [
        SpaContainerComponent
    ]
})
export class SpaModule { }
