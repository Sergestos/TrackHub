import { Component } from "@angular/core";
import { AuthService } from "../../providers/services/auth.service";
import { Router } from "@angular/router";
import { SocialUser } from "@abacritt/angularx-social-login";

@Component({
	selector: 'trh-auth',
	templateUrl: './auth.component.html',
	styleUrls: ['./auth.component.css']
})
export class AuthComponent {	
    constructor(
        private authService: AuthService,
        private router: Router) {
        this.authService.getGoogleAuthState().subscribe((user: SocialUser) => {
            this.authService.authExternalUser(user).subscribe((response: string) => {
                this.router.navigateByUrl('/app/list');
            });
        });
    }
}