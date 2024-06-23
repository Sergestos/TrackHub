import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { CommitService } from './moduls/commit/commit.service';
import { AppContainerComponent } from './moduls/app-container/app-container.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [
        AppComponent,
        AppContainerComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        AppRoutingModule
    ],
    providers: [
        provideClientHydration(),
        CommitService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
