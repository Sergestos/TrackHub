import { Component, ElementRef, EventEmitter, HostListener, OnInit, Output } from "@angular/core";
import { AuthService } from "../../../providers/services/auth.service";
import { Router } from "@angular/router";

@Component({
    selector: 'trh-user-dropdown',
    templateUrl: './user-dropdown.component.html',
    styleUrls: ['./user-dropdown.component.css']
})
export class UserDropdownComponent implements OnInit {
    private isComponentReady: boolean = false;

    @Output()
    public isUserMenuAskedEmmiter: EventEmitter<boolean> = new EventEmitter<boolean>();

    @HostListener('document:click', ['$event'])
    public clickout(event: any) {
        if (this.isComponentReady) {
            if (!this.eRef.nativeElement.contains(event.target)) {
                this.isUserMenuAskedEmmiter.emit(false);
            }
        }
    }

    constructor(
        private authService: AuthService,
        private router: Router,
        private eRef: ElementRef) {

    }

    public ngOnInit(): void {
        setTimeout(() => {
            this.isComponentReady = true;            
        }, 0);
    }

    public onLogout(): void {
        this.authService.logOut()
            .subscribe({
                next: _ => {
                    this.isUserMenuAskedEmmiter.emit(false);
                    this.router.navigateByUrl('app/login');
                },
                error: err => {
                    
                }
            });
    }
}