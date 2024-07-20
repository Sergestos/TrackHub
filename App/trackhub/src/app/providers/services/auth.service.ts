import { GoogleLoginProvider, SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map, of } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private isAuthorized$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private user: SocialUser | null; 
    
    constructor(
        private http: HttpClient,
        private googleAuthService: SocialAuthService
    ) {
        this.user = null;
        this.googleAuthService.authState.subscribe((user: SocialUser) => {
          console.log(user);
          this.user = user;
        });
    }

    public logInViaGoogle(): void {
        this.googleAuthService
            .signIn(GoogleLoginProvider.PROVIDER_ID)
            .then((x: any) => console.log(x));
    }

    public logIn(): Observable<any> {
        // return this.http.get('https://localhost:7012/google-login');

        /*this.isAuthorized$.next(true);
        return this.isAuthorized$.asObservable();*/

        return this.http.get<any>('https://localhost:7012/google-login',
            {
                params: new HttpParams().set('provider', 'Google'),
                headers: new HttpHeaders()
                    .set('Access-Control-Allow-Headers', 'Content-Type')
                    .set('Access-Control-Allow-Methods', 'GET')
                    .set('Access-Control-Allow-Origin', '*')
            })
            .pipe(map(data => {
                return data;
            }));
    }

    public logOut(): Observable<boolean> {
        this.isAuthorized$.next(false);
        return this.isAuthorized$.asObservable();
    }

    public isAuthorized(): Observable<boolean> {
        return this.isAuthorized$;
    }
}