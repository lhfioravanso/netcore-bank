import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TokenStorageService } from '../../services/token-storage.service';
import { User } from '../../models/user';
import { Account } from '../../models/account';
import { Transaction } from '../../models/transaction';
import { first } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { AddTransactionDialogComponent } from 'src/app/dialogs/add-transaction/add-transaction-dialog.component';
import { NotificationService } from '../../services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { MatTableDataSource } from '@angular/material/table';
import { AccountService } from 'src/app/services/account.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {

  displayedColumns = ['createdAt', 'transactionOperation', 'value'];
  dataSource = new MatTableDataSource<any>();
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  user: User;
  account: Account;
  transactions: Transaction[];
  operacoes = [
    { id: 1, descricao: "Depósito" },
    { id: 2, descricao: "Resgate" },
    { id: 3, descricao: "Pagamento" },
  ]

  constructor(private tokenStorage: TokenStorageService, 
    public dialog: MatDialog, 
    private notifyService: NotificationService,
    private userService: UserService,
    private accountService: AccountService) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  filter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  ngOnInit(): void {
    this.user = this.tokenStorage.getUser();

    this.userService.getUser(this.user.id)
      .pipe(first())
      .subscribe(data => {
        if (data) {
          this.accountService.getAccount(data.listAccountId[0])
            .pipe(first())
            .subscribe(acc => {
              if (acc) {
                this.account = (acc);
                this.accountService.getAccountTransactions(this.account.id)
                  .pipe(first())
                  .subscribe(transactions => {
                    if (transactions) {
                      this.transactions = (transactions);
                      this.dataSource.data = (this.transactions);
                    }
                  });
              }
            });
        }
      });
    
  }

  deposit() {
    this.add(this.operacoes[0], this.account.id)
  }

  withdraw() {
    this.add(this.operacoes[1], this.account.id)
  }

  payment() {
    this.add(this.operacoes[2], this.account.id)
  }

  add(operacao, accountId) {
    const dialogRef = this.dialog.open(AddTransactionDialogComponent, {
      data: { 
        accountId: accountId,
        operacao: operacao 
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.account.balance = result;
        this.notifyService.showSuccess("Operação realizada com sucesso.", "Notificação");
        this.refreshTable();
      }
    });
  }

  public loadTransactions(id: any) {
    return this.accountService.getAccountTransactions(id)
            .pipe(first())
            .subscribe(transactions => {
              if (transactions) {
                this.transactions = (transactions);
                this.dataSource.data = (this.transactions);
              }
            });
  }

  private refreshTable() {
    this.loadTransactions(this.account.id);
    this.paginator._changePageSize(this.paginator.pageSize);
  }

  

}
