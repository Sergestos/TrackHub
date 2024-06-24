import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";

@Injectable()
export class AuthService {
    private isAuthorized$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    constructor(private http: HttpClient) { 

    }

    public logIn(): Observable<boolean> {
        this.isAuthorized$.next(true);
        return this.isAuthorized$.asObservable();
    }

    public logOut(): Observable<boolean> {
        this.isAuthorized$.next(false);       
        return this.isAuthorized$.asObservable();
    }

    public isAuthorized(): Observable<boolean> {
        return this.isAuthorized$;
    }
}