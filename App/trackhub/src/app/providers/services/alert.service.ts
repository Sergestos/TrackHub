import { Injectable, signal } from '@angular/core';

export type AlertType = 'success' | 'error' | 'warning' | 'info';

export interface AlertMessage {
  type: AlertType;
  text: string;
}

@Injectable({ providedIn: 'root' })
export class AlertService {
  private _alert = signal<AlertMessage | null>(null);

  alert = this._alert.asReadonly();

  show(type: AlertType, text: string) {
    this._alert.set({ type, text });

    setTimeout(() => this.clear(), 5000);
  }

  clear() {
    this._alert.set(null);
  }
}
