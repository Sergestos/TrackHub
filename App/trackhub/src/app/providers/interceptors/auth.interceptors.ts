import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable, Injector } from "@angular/core";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { catchError, filter, finalize, switchMap, take } from "rxjs/operators";
import { AuthService } from "../services/auth.service";

@Injectable({ providedIn: 'root' })
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshedToken$ = new BehaviorSubject<string | null>(null);

  private injector = inject(Injector);
  private authService?: AuthService;

  private auth(): AuthService {
    if (!this.authService) this.authService = this.injector.get(AuthService);
    return this.authService;
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: unknown) => {
        if (!(err instanceof HttpErrorResponse)) return throwError(() => err);

        if (err.status !== 401) return throwError(() => err);

        if (req.url.includes('api/auth/refresh')) return throwError(() => err);

        if (this.isRefreshing) {
          return this.refreshedToken$.pipe(
            filter(token => token !== null),
            take(1),
            switchMap(token => {
              const retryReq = req.clone({
                setHeaders: { Authorization: `Bearer ${token!}` }
              });

              return next.handle(retryReq);
            })
          );
        }

        this.isRefreshing = true;
        this.refreshedToken$.next(null);

        return this.auth().refreshToken().pipe(
          switchMap(() => {
            const token = this.auth().getAccessToken();
            if (!token) 
              return throwError(() => err);

            this.refreshedToken$.next(token);
            const retryReq = req.clone({
              setHeaders: { Authorization: `Bearer ${token}` }
            });

            return next.handle(retryReq);
          }),
          catchError(refreshErr => {
            this.refreshedToken$.next(null);

            return throwError(() => refreshErr);
          }),
          finalize(() => {
            this.isRefreshing = false;
          })
        );
      })
    );
  }
}
