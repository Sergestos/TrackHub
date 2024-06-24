import { Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, tap } from "rxjs";

@Injectable()
export class AuthGuard  {
    constructor(
        private authService: AuthService,
        private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return true;
        return this.authService.isAuthorized()
            .pipe(tap(isAuthorized => {
                if (!isAuthorized) {
                    this.router.navigateByUrl("/login")
                }
            }));
        ;
    }
}