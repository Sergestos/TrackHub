import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../providers/services/auth.service';
import { Router } from '@angular/router';
import { LoadingService } from '../../providers/services/loading.service';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'trh-app-container',
  templateUrl: './app-container.component.html',
  styleUrls: ['./app-container.component.scss'],
  standalone: false
})
export class AppContainerComponent implements OnInit {
  private localStorage!: Storage;

  public isLoading$!: Observable<boolean>;
  public isAuthorized$!: Observable<boolean>;
  public isUserMenuAsked: boolean = false;

  public userName: string = ''
  public userPictureUrl: string = ''

  constructor(
    private router: Router,
    private authService: AuthService,
    private loadingService: LoadingService,
    @Inject(DOCUMENT) document: Document
  ) {
    this.localStorage = document.defaultView?.localStorage!;
  }

  public ngOnInit(): void {
    this.isAuthorized$ = this.authService.isAuthorized();
    this.isLoading$ = this.loadingService.loading$;

    if (this.localStorage) {
      this.userName = this.localStorage.getItem('user_name') ?? 'user';
      this.userPictureUrl = this.localStorage.getItem('profile_url') ?? '';
    }
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

  public onLogoutClick(): void {
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
