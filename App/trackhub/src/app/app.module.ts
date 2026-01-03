import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AppContainerComponent } from './modules/app-container/app-container.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './providers/interceptors/auth.interceptors';
import { AuthService } from './providers/services/auth.service';
import { ExerciseListService } from './modules/exercise-list/services/exercise-list.service';
import { GoogleLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { JwtInterceptor } from './providers/interceptors/jwt.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ApiService } from './providers/services/api.service';
import { AlertComponent } from './components/alert/alert.component';
import { AlertService } from './providers/services/alert.service';

@NgModule({
  declarations: [
    AppComponent,
    AppContainerComponent,
  ],
  bootstrap: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    MatProgressSpinnerModule,
    AlertComponent
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withInterceptorsFromDi()),
    ApiService,
    AuthService,
    AlertService,
    ExerciseListService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      deps: [Router, ActivatedRoute,],
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
  ]
})
export class AppModule { }
