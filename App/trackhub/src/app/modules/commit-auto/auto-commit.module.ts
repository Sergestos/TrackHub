import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AutoCommitComponent } from "./components/auto-commit.component";
import { AutoCommitService } from "./services/auto-commit.service";
import { PreviewValidationComponent } from "./components/preview-validation/preview-validation.component";
import { ButtonComponent } from "../../components/button/button.component";

@NgModule({
  declarations: [
    AutoCommitComponent,
    PreviewValidationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule.forChild([
      { path: '', component: AutoCommitComponent }
    ])
  ],
  providers: [
    AutoCommitService
  ],
  exports: []
})
export class AutoCommitModule { }
