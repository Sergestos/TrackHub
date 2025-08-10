import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { tap } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  private router = inject(Router);

  intercept(req: HttpRequest<any>, next: HttpHandler): any {
    return next.handle(req).pipe(
      tap({
        error: (error) => {
          if (error instanceof HttpErrorResponse) {
            if (error.status === 401 || error.status === 403) {
              this.router.navigateByUrl('/app/login')
            }
          }
        },
      }));
  }
}