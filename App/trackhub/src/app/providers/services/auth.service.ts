import { GoogleLoginProvider, SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map, of, tap } from "rxjs";

class AppUser {
    public email!: string;
    public firstName!: string;
    public lastName!: string;
    public photoUrl!: string;
    public idToken!: string;
}

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

          this.authExternalUser(this.user).subscribe(x => console.log(x));
        });
    }

    public logInViaGoogle(): void {
        this.googleAuthService
            .signIn(GoogleLoginProvider.PROVIDER_ID)
            .then((x: any) => console.log(x));
    }

    public logIn(): Observable<any> {
        return of(this.googleAuthService
            .signIn(GoogleLoginProvider.PROVIDER_ID)
            .then((response: any) => { 
                this.authExternalUser(response).subscribe(x => {

                }) }
            ));
    }

    public logOut(): Observable<boolean> {
        this.isAuthorized$.next(false);
        return this.isAuthorized$.asObservable();
    }

    public isAuthorized(): Observable<boolean> {
        return this.isAuthorized$;
    }

    private authExternalUser(user: any): Observable<any> {
        let appUser: AppUser = {
            email: user.email,
            firstName: user.firstName,
            lastName: user.lastName,
            photoUrl: user.photoUrl,
            idToken: user.idToken
        };

        return this.http.post<any>('https://localhost:7012/google-login', appUser);
    }
}