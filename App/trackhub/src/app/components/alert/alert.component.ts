
import { Component, computed, inject } from '@angular/core';
import { AlertService } from '../../providers/services/alert.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert.component.html'
})
export class AlertComponent {
  private alertService = inject(AlertService);

  alert = this.alertService.alert;

  css = computed(() => {
    const type = this.alert()?.type;
    return {
      'bg-green-700 text-white': type === 'success',
      'bg-red-600 text-white': type === 'error',
      'bg-yellow-500 text-black': type === 'warning',
      'bg-blue-600 text-white': type === 'info',
    };
  });

  close() {
    this.alertService.clear();
  }
}
