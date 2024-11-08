import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, EMPTY, Observable, of } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { environment } from "../../environments/environment";
import { DOCUMENT } from "@angular/common";

class GoogleAuth {
    public idToken!: string;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private localStorage!: Storage;

    private isAuthorized$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor(
        private http: HttpClient,
        private googleAuthService: SocialAuthService,
        @Inject(DOCUMENT) document: Document
    ) { 
        this.localStorage = document.defaultView?.localStorage!;
    }

    public getGoogleAuthState(): Observable<SocialUser> {
        return this.googleAuthService.authState;
    }

    public logOut(): Observable<boolean> {
        this.localStorage.removeItem('access_token');

        this.googleAuthService.signOut();
        this.isAuthorized$.next(false);

        return this.isAuthorized$.asObservable();
    }

    public isAuthorized(): Observable<boolean> {
        return this.isAuthorized$
            .pipe(state$ => { 
                if (this.localStorage && this.localStorage.getItem('access_token') && !this.isAuthorized$.value) {
                    const url = environment.apiUrl + '/api/auth/validate-token';
                    return this.http.get(url)
                        .pipe(
                            catchError(_ => of(false)),
                            map(_ => true)                       
                        );
                }
                
                return state$
            });
    }

    public authExternalUser(user: any): Observable<string> {
        const headers: HttpHeaders = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        const payload: GoogleAuth = {
            idToken: user.idToken
        }

        const url = environment.apiUrl + '/api/auth/google-login';
        return this.http.post<any>(url, payload, { headers })
            .pipe(
                map(result => {
                    this.localStorage.setItem('profile_url', result.user.photoUrl);
                    this.localStorage.setItem('user_name', result.user.fullName);
                    this.localStorage.setItem('access_token', result.token);

                    this.isAuthorized$.next(true);
                    return result.token;
                }),
                catchError((err, caught) => {
                    console.error(err);
                    return EMPTY;
                })
            )
    }
}