import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { Router, RouterModule } from '@angular/router';
import { CommitService } from './providers/services/commit.service';
import { AppContainerComponent } from './moduls/app-container/app-container.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './providers/interceptors/auth.intercaptors';
import { AuthService } from './providers/services/auth.service';
import { AuthGuard } from './providers/guard/auth.guard';

@NgModule({
    declarations: [
        AppComponent,
        AppContainerComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        AppRoutingModule,
        HttpClientModule
    ],
    providers: [
        provideClientHydration(),
        AuthService,
        CommitService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            deps: [Router],
            multi: true
        },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
