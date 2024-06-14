import { NgModule } from '@angular/core';
import { SpaContainerComponent } from './spa-container/spa-container.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { SpaRoutingModule } from './spa-routing.module';
import { CommitModule } from './commit/commit.module';

@NgModule({
    declarations: [
        SpaContainerComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        SpaRoutingModule,
        CommitModule
    ],
    providers: [

    ],
    exports: [
        SpaContainerComponent
    ]
})
export class SpaModule { }
