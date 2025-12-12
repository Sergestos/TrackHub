import { Component, inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { SocialUser } from "@abacritt/angularx-social-login";
import { tap } from "rxjs";
import { AuthService } from "../../../providers/services/auth.service";
import { LoadingService } from "../../../providers/services/loading.service";

const ROUTE_LIST = '/app/list';

@Component({
  selector: 'thr-auth',
  templateUrl: './auth.component.html',
  standalone: false
})
export class AuthComponent implements OnInit {
  private authService = inject(AuthService);
  private loadingService = inject(LoadingService);
  private router = inject(Router);

  public ngOnInit(): void {
    this.authService
      .getGoogleAuthState()
      .subscribe((user: SocialUser) => {
        if (!user)
          return;
        
        this.loadingService.show();
        this.authService.authExternalUser(user)
          .pipe(tap(_ => this.loadingService.complete()))
          .subscribe(token => {
            if (token) {
              this.router.navigateByUrl(ROUTE_LIST);
            }
          });
      });
  }
}