import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../providers/services/auth.service';

@Component({
    selector: 'trh-app-container',
    templateUrl: './app-container.component.html',
    styleUrls: ['./app-container.component.css']
})
export class AppContainerComponent implements OnInit {
    public isAuthorized$: Observable<boolean> = new Observable<boolean>();
    public isUserMenuAsked: boolean = false;

    constructor(private authService: AuthService) { }

    public ngOnInit(): void {
        this.isAuthorized$ = this.authService.isAuthorized(); 
    }

    public onMenuDropDown(): void { 
        this.isUserMenuAsked = !this.isUserMenuAsked;
    }

    public onUserMenuPanelMouseOut(event: any): void {
        this.isUserMenuAsked = false;
    }
}
