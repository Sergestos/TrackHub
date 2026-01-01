import { Component, input, output } from "@angular/core";

export type ButtonType = 'default' | 'danger';

@Component({
  selector: 'trh-button',
  templateUrl: 'button.component.html',
  styleUrls: ['./button.component.scss'],
  standalone: true,
})
export class ButtonComponent {
  public value = input<string>();
  public disabled = input<boolean>(false);
  public type = input<ButtonType>('default');

  public pressed = output<MouseEvent>();

  public onClick(event: MouseEvent): void {
    if (this.disabled())
      return;

    this.pressed.emit(event);
  }
}
