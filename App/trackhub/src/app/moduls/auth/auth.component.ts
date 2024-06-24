import { Component } from "@angular/core";
import { AuthService } from "../../providers/services/auth.service";
import { Router } from "@angular/router";

@Component({
	selector: 'trh-auth',
	templateUrl: './auth.component.html',
	styleUrls: ['./auth.component.css']
})
export class AuthComponent {	
    constructor(
        private authService: AuthService,
        private router: Router) {
        
    }

    public onLogInClick(): void {
        this.authService.logIn()
            .subscribe({
                next: _ => {
                    this.router.navigateByUrl('app/list');
                },
                error: err => {
                    
                }
            });
    }
}