import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../providers/services/auth.service";
import { Router } from "@angular/router";
import { SocialUser } from "@abacritt/angularx-social-login";

@Component({
    selector: 'trackhub-auth',
    templateUrl: './auth.component.html'
})
export class AuthComponent implements OnInit {
    constructor(
        private authService: AuthService,
        private router: Router) { }

    public ngOnInit(): void {
        this.authService.getGoogleAuthState().subscribe((user: SocialUser) => {
            this.authService.authExternalUser(user).subscribe(_ => {
                this.router.navigateByUrl('/app/list');
            });
        });
    }
}