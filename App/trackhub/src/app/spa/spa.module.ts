import { NgModule } from '@angular/core';
import { SpaContainerComponent } from './spa-container/spa-container.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        SpaContainerComponent
    ],
    imports: [
        BrowserModule,
        RouterModule
    ],
    providers: [

    ],
    exports: [
        SpaContainerComponent
    ]
})
export class SpaModule { }
