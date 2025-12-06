import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AutoCommitComponent } from "./components/auto-commit.component";
import { AutoCommitService } from "./services/auto-commit.service";

@NgModule({
  declarations: [
    AutoCommitComponent
  ],
  exports: [

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: AutoCommitComponent }
    ])
  ],
  providers: [
    AutoCommitService
  ]
})
export class AutoCommitModule { }
