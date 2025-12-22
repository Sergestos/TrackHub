import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Inject, Injectable, signal, DOCUMENT } from "@angular/core";
import { EMPTY, Observable, of } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { environment } from "../../environments/environment";

class GoogleAuth {
  public idToken!: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpClient = inject(HttpClient);
  private googleAuthService = inject(SocialAuthService);

  readonly isAuthorized = signal<boolean>(false);

  private localStorage!: Storage;

  constructor(@Inject(DOCUMENT) document: Document) {
    this.localStorage = document.defaultView?.localStorage!;

    if (this.localStorage && this.localStorage.getItem('access_token')) {
      const url = environment.apiUrl + '/api/auth/validate-token';
      this.httpClient.get(url)
        .subscribe({
          next: () => {
            this.isAuthorized.set(true);
          },
          error: () => {
            this.isAuthorized.set(false);
          }
        });
    }
  }

  public logOut(): Observable<boolean> {
    this.localStorage.removeItem('access_token');
    this.localStorage.removeItem('user_name');
    this.localStorage.removeItem('profile_url');

    this.isAuthorized.set(false);

    this.googleAuthService.authState
      .subscribe(_ => this.googleAuthService.signOut(true));

    return of(true);
  }

  public getGoogleAuthState(): Observable<SocialUser> {
    return this.googleAuthService.authState;
  }

  public authExternalUser(user: any): Observable<string> {
    const headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const payload: GoogleAuth = {
      idToken: user.idToken
    }

    const url = environment.apiUrl + '/api/auth/google-login';
    return this.httpClient.post<any>(url, payload, { headers })
      .pipe(
        map(result => {
          this.localStorage.setItem('profile_url', result.user.photoUrl);
          this.localStorage.setItem('user_name', result.user.fullName);
          this.localStorage.setItem('access_token', result.token);

          this.isAuthorized.set(true);

          return result.token;
        }),
        catchError((err, caught) => {
          this.isAuthorized.set(false);

          console.error(err);
          return EMPTY;
        })
      )
  }
}