import { ChangeDetectionStrategy, Component, Inject, inject } from "@angular/core";
import { MatDialog, MatDialogRef } from "@angular/material/dialog";

export enum ModalResult {
  Confirmed,
  Rejected
}

@Component({
  selector: 'delete-modal',
  templateUrl: 'delete-modal.component.html',
  styleUrls: ['./delete-modal.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DeleteModal {
  readonly modalRef = inject(MatDialogRef<DeleteModal>);

  public onConfirmClick(): void {
    this.modalRef.close(ModalResult.Confirmed);
  }

  public onRejectClick(): void {
    this.modalRef.close(ModalResult.Rejected);
  }
}

export function openDeleteModal(modal: MatDialog) {
  return modal.open(DeleteModal, {
    width: '550px',
    panelClass: 'custom-dialog-container',
  });
}