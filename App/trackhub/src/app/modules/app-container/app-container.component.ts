import {
  Component,
  computed,
  effect,
  inject,
  Inject,
  OnInit,
  DOCUMENT,
} from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../providers/services/auth.service';
import { Router } from '@angular/router';
import { LoadingService } from '../../providers/services/loading.service';

const ROUTE_LOGIN = '/app/login';

@Component({
  selector: 'trh-app-container',
  templateUrl: './app-container.component.html',
  styleUrls: ['./app-container.component.scss'],
  standalone: false,
})
export class AppContainerComponent implements OnInit {
  private router = inject(Router);
  private authService = inject(AuthService);
  private loadingService = inject(LoadingService);

  private localStorage!: Storage;

  public isLoading$!: Observable<boolean>;
  public isAuthorizedState = computed(() => this.authService.isAuthorized());

  public userName: string = '';
  public userPictureUrl: string = '';

  constructor(@Inject(DOCUMENT) document: Document) {
    this.localStorage = document.defaultView?.localStorage!;

    effect(() => {
      if (this.localStorage) {
        setTimeout(() => {
          this.userName = this.localStorage.getItem('user_name') ?? 'user';
          this.userPictureUrl = this.localStorage.getItem('profile_url') ?? '';
        }, 0);
      }
    });
  }

  public ngOnInit(): void {
    this.isLoading$ = this.loadingService.loading$;
  }

  public onNavigationClicked(url: string): void {
    this.router.navigate([url]);
  }

  public onLogoutClick(): void {
    this.authService
      .logout()
      .subscribe((_) => this.router.navigateByUrl(ROUTE_LOGIN));
  }
}
