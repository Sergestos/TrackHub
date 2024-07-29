import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { Router, RouterModule } from '@angular/router';
import { CommitService } from './providers/services/commit.service';
import { AppContainerComponent } from './moduls/app-container/app-container.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './providers/interceptors/auth.intercaptors';
import { AuthService } from './providers/services/auth.service';
import { PermissionsService } from './providers/guards/auth.guard';
import { UserDropdownComponent } from './moduls/app-container/user-dropdown/user-dropdown.component';
import { ExerciseListService } from './providers/services/exercise-list.service';
import { GoogleLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';

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
    ]})
export class AppModule { }
