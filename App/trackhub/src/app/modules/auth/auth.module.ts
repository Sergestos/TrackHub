import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AuthComponent } from "./components/auth.component";
import {
  GoogleSigninButtonModule,
  GoogleSigninButtonDirective,
  SocialLoginModule
}
from "@abacritt/angularx-social-login";

@NgModule({
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  declarations: [
    AuthComponent
  ],
  imports: [
    SocialLoginModule,
    GoogleSigninButtonModule,
    RouterModule.forChild([
      { path: '', component: AuthComponent }
    ]),
  ],
  providers: [
    GoogleSigninButtonDirective
  ]
})
export class AuthModule { }