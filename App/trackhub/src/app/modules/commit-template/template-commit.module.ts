import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TemplateCommitComponent } from './components/template-commit.component';
import { TemplateCommitService } from './services/template-commit.service';
import { PreviewValidationComponent } from './components/preview-validation/preview-validation.component';
import { ButtonComponent } from '../../components/button/button.component';

@NgModule({
  declarations: [TemplateCommitComponent, PreviewValidationComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule.forChild([{ path: '', component: TemplateCommitComponent }]),
  ],
  providers: [TemplateCommitService],
  exports: [],
})
export class TemplateCommitModule {}
