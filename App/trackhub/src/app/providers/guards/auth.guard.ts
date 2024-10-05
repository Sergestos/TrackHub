import { Injectable, inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, tap } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class PermissionsService {
    constructor(
        private router: Router,
        private authService: AuthService
    ) {

    }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.authService.isAuthorized()
            .pipe(tap(isAuthorized => {
                if (!isAuthorized) {
                    this.router.navigateByUrl("/app/login")
                }
            }));
    }
}

export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> => {
    return inject(PermissionsService).canActivate(next, state);
}