import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: any = {};
  isLoginFailed = false;

  username: string;
  password: string;

  error: string;
  hide = true;

  constructor(private userService: UserService, private tokenStorage: TokenStorageService, private router: Router) { }

  formControl = new FormControl('', []);

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.router.navigateByUrl("/inicio");
    }
  }

  login(): void {
    let credentials = { username: this.username, password: this.password }
    this.userService.authenthicate(credentials).subscribe(
      data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUser(data);

        this.isLoginFailed = false;
        this.reloadPage();
      },
      err => {
        this.error = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

  reloadPage(): void {
    window.location.reload();
  }

}
