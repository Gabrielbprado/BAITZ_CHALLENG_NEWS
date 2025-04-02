import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})
export class DeleteModalComponent {

  constructor(private dialogRef: MatDialogRef<DeleteModalComponent>) {}
  close(): void {
    this.dialogRef.close(); 
  }

  confirmDeletion(): void {
    this.dialogRef.close(true); 
  }
}
