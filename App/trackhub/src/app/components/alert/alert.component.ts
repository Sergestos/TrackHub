import { Component, computed, inject } from '@angular/core';
import { AlertService } from '../../providers/services/alert.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'trh-alert',
  imports: [CommonModule],
  templateUrl: './alert.component.html',
})
export class AlertComponent {
  private alertService = inject(AlertService);

  alert = this.alertService.alert;

  css = computed(() => {
    const type = this.alert()?.type;
    return {
      'border-green-500 text-white': type === 'success',
      'border-red-600 text-white': type === 'error',
      'border-yellow-500 text-black': type === 'warning',
      'border-blue-500 text-white': type === 'info',
    };
  });

  close() {
    this.alertService.clear();
  }
}
