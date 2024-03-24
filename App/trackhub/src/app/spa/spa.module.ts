import { NgModule } from '@angular/core';
import { SpaContainerComponent } from './spa-container/spa-container.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { SpaRoutingModule } from './spa-routing.module';
import { ExerciseComponent } from './commit/exercise/exercise.component';
import { CommitComponent } from './commit/commit.component';

@NgModule({
    declarations: [
        SpaContainerComponent,
        ExerciseComponent,
        CommitComponent
    ],
    imports: [
        BrowserModule,
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
