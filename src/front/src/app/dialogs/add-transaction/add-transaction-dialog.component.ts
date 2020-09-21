import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-add-transaction-dialog',
  templateUrl: './add-transaction-dialog.html',
  styleUrls: ['./add-transaction-dialog.css']
})

export class AddTransactionDialogComponent {

  error: string = null;
  operacao: any;

  constructor(public dialogRef: MatDialogRef<AddTransactionDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private accountService: AccountService) { 
                  this.operacao = data.operacao
              }

  formControl = new FormControl('', [
    Validators.required
  ]);

  getErrorMessage() {
    return this.formControl.hasError('required') ? 'Campo obrigatório' :
        '';
  }

  submit() {
    
  }

  cancel(): void {
    this.dialogRef.close();
  }

  public confirm(): void {   
    this.accountService.makeTransaction(this.operacao.id, this.data.accountId, { value: this.data.operationValue }).subscribe((res: any) => {
      if (res.success) {
        this.dialogRef.close(res.balance);
      } else
      {
        this.error = res.message;
      }  
    }, (err: any) => {
      if (err.error) {
        this.error = err.error.message;
      }
      else if (err.message) {
        this.error = err.message;
      } else {
        this.error = "Erro não tratado";
      }
    });

    
  }
}
