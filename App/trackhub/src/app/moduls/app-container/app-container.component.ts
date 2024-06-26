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
    public isUserSettingsAsked: boolean = false;

    constructor(private authService: AuthService) { }

    public ngOnInit(): void {
        this.isAuthorized$ = this.authService.isAuthorized(); 
    }

    public onSettingsDropDown(): void { 
        this.isUserSettingsAsked = !this.isUserSettingsAsked;
    }

    public onUserSettingsPanelMouseOut(event: any): void {
        this.isUserSettingsAsked = false;
    }
}
