import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../providers/services/auth.service";
import { Router } from "@angular/router";
import { SocialUser } from "@abacritt/angularx-social-login";
import { LoadingService } from "../../providers/services/loading.service";
import { tap } from "rxjs";

@Component({
  selector: 'trackhub-auth',
  templateUrl: './auth.component.html',
  standalone: false
})
export class AuthComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private loadingService: LoadingService,
    private router: Router) { }

  public ngOnInit(): void {
    this.authService
      .getGoogleAuthState()
      .subscribe((user: SocialUser) => {
        if (user) {
          this.loadingService.show();
          this.authService.authExternalUser(user)
            .pipe(tap(_ => this.loadingService.complete()))
            .subscribe(token => {
              if (token) {
                this.router.navigateByUrl('/app/list');
              }
            });
        }
      });
  }
}