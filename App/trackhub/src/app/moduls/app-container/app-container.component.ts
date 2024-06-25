import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../providers/services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'trh-app-container',
    templateUrl: './app-container.component.html',
    styleUrls: ['./app-container.component.css']
})
export class AppContainerComponent implements OnInit {
    public isAuthorized$: Observable<boolean> = new Observable<boolean>();
    public userSettingsAsked: boolean = false;

    @HostListener('document:click', ['$event'])
    public clickout(event: any) {
        if(!this.eRef.nativeElement.contains(event.target)) {
            this.userSettingsAsked = false;
        }
    }

    constructor(
        private authService: AuthService,
        private router: Router,
        private eRef: ElementRef) {

    }

    public ngOnInit(): void {
        this.isAuthorized$ = this.authService.isAuthorized(); 
    }

    public onSettingsDropDown(): void {
        this.userSettingsAsked = !this.userSettingsAsked;
    }

    public onLogout(): void {
        this.authService.logOut()
            .subscribe({
                next: _ => {
                    this.userSettingsAsked = false;
                    this.router.navigateByUrl('app/login');
                },
                error: err => {
                    
                }
            });
    }
}
