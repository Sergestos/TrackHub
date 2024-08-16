import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, EMPTY, Observable } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';

class GoogleAuth {
    public idToken!: string;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private isAuthorized$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor(
        private http: HttpClient,
        private googleAuthService: SocialAuthService
    ) { }

    public getGoogleAuthState(): Observable<SocialUser> {
        return this.googleAuthService.authState;
    }

    public logOut(): Observable<boolean> {
        this.isAuthorized$.next(false);
        return this.isAuthorized$.asObservable();
    }

    public isAuthorized(): Observable<boolean> {
        return this.isAuthorized$;
    }

    public authExternalUser(user: any): Observable<string> {
        const headers: HttpHeaders = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        const payload: GoogleAuth = {
            idToken: user.idToken
        }

        return this.http.post<string>('http://localhost:5044/api/auth/google-login', payload, { headers, responseType: 'text' as 'json' })
            .pipe(
                map(result => {
                    this.isAuthorized$.next(true);
                    localStorage.setItem('access_token', result);
                    return result;
                }),
                catchError((err, caught) => {
                    console.error(err);
                    return EMPTY;
                })
            )
    }
}