import { Component, input } from '@angular/core';

@Component({
  selector: 'trh-preview-validation',
  templateUrl: './preview-validation.component.html',
  standalone: false,
})
export class PreviewValidationComponent {
  public issue = input.required<string>();
  public lineNumber = input<number>();
}
