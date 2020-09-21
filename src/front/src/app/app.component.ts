import { Component } from '@angular/core';
import { TokenStorageService } from './services/token-storage.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Net Core Bank APP';

  constructor(private tokenStorageService: TokenStorageService) { }

  isLoggedIn$: Observable<boolean>;
  name: string;

  ngOnInit(): void {
    this.isLoggedIn$ = this.tokenStorageService.isLoggedIn;
    if (this.isLoggedIn$) {
      const user = this.tokenStorageService.getUser();
      this.name = user.name;
    }
  }

  logout(): void {
    this.tokenStorageService.signOut();
    window.location.reload();
  }
}
