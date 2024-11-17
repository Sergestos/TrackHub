import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { Router, RouterModule, withHashLocation } from '@angular/router';
import { CommitService } from './moduls/commit/commit.service';
import { AppContainerComponent } from './moduls/app-container/app-container.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './providers/interceptors/auth.interceptors';
import { AuthService } from './providers/services/auth.service';
import { PermissionsService } from './providers/guards/auth.guard';
import { UserDropdownComponent } from './moduls/app-container/user-dropdown/user-dropdown.component';
import { ExerciseListService } from './moduls/exericse-list/exercise-list.service';
import { GoogleLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { JwtInterceptor } from './providers/interceptors/jwt.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
    declarations: [
        AppComponent,
        AppContainerComponent,
        UserDropdownComponent
    ],
    bootstrap: [
        AppComponent
    ], 
    imports: [
        BrowserModule,
        RouterModule,
        AppRoutingModule,
        MatProgressSpinnerModule
    ],
    providers: [       
        provideClientHydration(),
        provideHttpClient(withInterceptorsFromDi()),
        AuthService,
        CommitService,
        ExerciseListService,
        PermissionsService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            deps: [Router],
            multi: true
        },        
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        },
        {
            provide: 'SocialAuthServiceConfig',
            useValue: {
                autoLogin: false,
                providers: [
                    {
                        id: GoogleLoginProvider.PROVIDER_ID,
                        provider: new GoogleLoginProvider('940834046939-gj9u1uicginda53uv10m66eg8k5hicnc.apps.googleusercontent.com')
                    }
                ],
                onError: (err) => {
                    console.error(err);
                }
            } as SocialAuthServiceConfig
        },
        provideAnimationsAsync(),
    ]})
export class AppModule { }
