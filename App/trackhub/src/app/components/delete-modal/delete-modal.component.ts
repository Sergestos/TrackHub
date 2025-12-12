import { ChangeDetectionStrategy, Component, Inject, inject } from "@angular/core";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { ButtonComponent } from "../button/button.component";

export enum ModalResult {
  Confirmed,
  Rejected
}

@Component({
  selector: 'delete-modal',
  templateUrl: 'delete-modal.component.html',
  styleUrls: ['./delete-modal.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ButtonComponent]
})
export class DeleteModalComponent {
  readonly modalRef = inject(MatDialogRef<DeleteModalComponent>);

  public onConfirmClick($event: Event): void {
    this.modalRef.close(ModalResult.Confirmed);
  }

  public onRejectClick($event: Event): void {
    this.modalRef.close(ModalResult.Rejected);
  }
}

export function openDeleteModal(modal: MatDialog) {
  return modal.open(DeleteModalComponent, {
    width: '550px',
    panelClass: 'custom-dialog-container',
  });
}