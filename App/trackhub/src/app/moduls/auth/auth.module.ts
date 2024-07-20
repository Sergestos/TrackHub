import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AuthComponent } from "./auth.component";

@NgModule({
    declarations: [
        
    ],
    imports: [      
        RouterModule.forChild([
            { path: '', component: AuthComponent }
        ])
    ],
    providers: [
      
    ],
    exports: [
        
    ]
})
export class AuthModule { }