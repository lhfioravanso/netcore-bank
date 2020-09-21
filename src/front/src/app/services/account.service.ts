import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private api_url = `${environment.apiUrl}/Account`;

  constructor(private http: HttpClient) { }

  makeTransaction(operacaoId: any, userId: any, value: any ) : Observable<any> {
    switch (operacaoId) {
      case 1:
        return this.deposit(userId, value);
      case 2:
        return this.withdraw(userId, value);
      case 3:
        return this.payment(userId, value);
      default:
        break;
    }
  }
  deposit(userId: any, value: any): Observable<any> {
    return this.http.post(`${this.api_url}/${userId}/deposit`, value, httpOptions);
  }

  withdraw(userId: any, value: any): Observable<any> {
    return this.http.post(`${this.api_url}/${userId}/withdraw`, value, httpOptions);
  }

  payment(userId: any, value: any): Observable<any> {
    return this.http.post(`${this.api_url}/${userId}/payment`, value, httpOptions);
  }

  getAccount(id): Observable<any> {
    return this.http.get(`${this.api_url}/${id}`);
  }

  getAccountTransactions(id): Observable<any> {
    return this.http.get(`${this.api_url}/${id}/transactions`);
  }
}
