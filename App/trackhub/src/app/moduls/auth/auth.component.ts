import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../providers/services/auth.service";
import { Router } from "@angular/router";
import { SocialUser } from "@abacritt/angularx-social-login";
import { LoadingService } from "../../providers/services/loading.service";

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
        this.authService.getGoogleAuthState().subscribe((user: SocialUser) => {
            if (user) {
		        this.loadingService.show();
                this.authService.authExternalUser(user).subscribe(_ => {
                    this.loadingService.hide();
                    this.router.navigateByUrl('/app/list');
                });
            }            
        });
    }
}