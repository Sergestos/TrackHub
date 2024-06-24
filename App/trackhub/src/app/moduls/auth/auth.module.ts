import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AuthComponent } from "./auth.component";
import { AuthService } from "../../providers/services/auth.service";

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