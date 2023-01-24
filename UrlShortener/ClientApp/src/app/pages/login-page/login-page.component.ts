import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UrlShortener } from 'src/app/services/url.service';
import { LoginRequest } from 'src/app/shared/loginResult';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor(private urlService: UrlShortener, private router: Router) {}

  public creds: LoginRequest = {
    username: "",
    password: "",
  };

  public ErrorMessage = "";

  onLogin() {
    this.urlService.login(this.creds).subscribe({
      next: () => {
        //successfully logged in       
          this.router.navigate([""]);       
      },
      error: (err: any) => {
        console.log(err);
        this.ErrorMessage = "Failed to login!";
      },
    });
  }
}
