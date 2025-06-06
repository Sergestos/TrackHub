import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent } from "./app.component";
import { Route } from '@angular/router';
import { AppContainerComponent } from "./modules/app-container/app-container.component";

export const appRoutes: Route[] = [];

@NgModule({
    declarations: [
        AppComponent,
        AppContainerComponent,
    ],
    imports: [
        BrowserModule,
        CommonModule,
        RouterModule, 
        RouterModule.forRoot(appRoutes)
    ],
    bootstrap: [
        AppComponent
    ],
})
export class AppModule { }