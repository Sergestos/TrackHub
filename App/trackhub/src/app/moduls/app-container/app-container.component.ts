import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../providers/services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'trh-app-container',
    templateUrl: './app-container.component.html',
    styleUrls: ['./app-container.component.css']
})
export class AppContainerComponent implements OnInit {
    public isAuthorized$ = new Observable<boolean>();

    constructor(
        private authService: AuthService,
        private router: Router) {

    }

    public ngOnInit(): void {
        this.isAuthorized$ = this.authService.isAuthorized(); 
    }

    public onLogout(): void {
        this.authService.logOut()
            .subscribe({
                next: _ => {
                    this.router.navigateByUrl('app/login');
                },
                error: err => {
                    
                }
            });
    }
}
