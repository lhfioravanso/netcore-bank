import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  private loggedIn = new BehaviorSubject<boolean>(false);
  
  constructor() { }

  signOut(): void {
    this.loggedIn.next(false);
    window.sessionStorage.clear();
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public getToken(): string {
    let token = sessionStorage.getItem(TOKEN_KEY);
    if (token)
      this.loggedIn.next(true);

    return token;
  }

  public saveUser(user): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
    this.loggedIn.next(true);
  }

  public getUser(): User {
    let user = JSON.parse(sessionStorage.getItem(USER_KEY));
    if (user)
      this.loggedIn.next(true);
      
    return user;
  }
  
  public get isLoggedIn() { 
    return this.loggedIn.asObservable(); 
  }
}
