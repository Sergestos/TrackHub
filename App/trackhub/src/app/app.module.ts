import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { SpaModule } from './spa/spa.module';
import { RouterModule } from '@angular/router';
import { CommitService } from './spa/commit/commit.service';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        SpaModule
    ],
    providers: [
        provideClientHydration(),
        CommitService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
