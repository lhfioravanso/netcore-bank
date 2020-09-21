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
export class UserService {

  private api_url = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  authenthicate(credentials): Observable<any> {
    return this.http.post(this.api_url + "/authenticate", {
      username: credentials.username,
      password: credentials.password
    }, httpOptions);
  }

  getUser(id): Observable<any> {
    return this.http.get(`${this.api_url}/User/${id}`);
  }

}
