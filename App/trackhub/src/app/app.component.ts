import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: '<trh-app-container></trh-app-container>',
  styles: `* {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100vh;
    background: #070707;
  }`,
  standalone: false
})
export class AppComponent {
  public isLoading: boolean = true;
}
