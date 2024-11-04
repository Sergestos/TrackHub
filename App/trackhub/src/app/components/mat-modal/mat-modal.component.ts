import { ChangeDetectionStrategy, Component, inject } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { 
    MatDialog, 
    MatDialogActions, 
    MatDialogClose, 
    MatDialogContent, 
    MatDialogRef, 
    MatDialogTitle } 
from "@angular/material/dialog";

export enum ModalResult {
    Confirmed,
    Rejected
}

@Component({
    selector: 'mat-modal',
    templateUrl: 'mat-modal.component.html',
    styleUrls: ['./mat-modal.component.css'],
    standalone: true,
    imports: [MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MatModal {
    readonly modalRef = inject(MatDialogRef<MatModal>);
    
    public onConfirmClick(): void {
        this.modalRef.close(ModalResult.Confirmed);
    }

    public onRejectClick(): void {        
        this.modalRef.close(ModalResult.Rejected);
    }
}

export function openDeleteConfirmationModal(modal: MatDialog) {
    return modal.open(MatModal, { 
        width: '450px',
        panelClass: 'custom-dialog-container'
    });
}