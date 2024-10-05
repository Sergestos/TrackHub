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
    public isAuthorized$!: Observable<boolean>;
    public isUserMenuAsked: boolean = false;

    constructor(
        private router: Router,
        private authService: AuthService
    ) { }

    public ngOnInit(): void {
        this.isAuthorized$ = this.authService.isAuthorized(); 
    }

    public onMenuDropDown(): void { 
        this.isUserMenuAsked = !this.isUserMenuAsked;
    }

    public onUserMenuPanelMouseOut(event: any): void {
        this.isUserMenuAsked = false;
    }

    public onNavigationClicked(url: string): void {
        this.router.navigate([url]);
    }
}
