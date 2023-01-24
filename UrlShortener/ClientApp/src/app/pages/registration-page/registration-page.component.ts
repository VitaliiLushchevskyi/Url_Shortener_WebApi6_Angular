import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UrlShortener } from 'src/app/services/url.service';
import { User } from 'src/app/shared/User';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css'],
})
export class RegistrationPageComponent {
  constructor(public urlService: UrlShortener, public router: Router) {}
  errorMessage = '';

  public user: User = {
    firstName: '',
    lastName: '',
    userName: '',
    password: '',
    email: '',
  };

  onRegistration() {
    this.urlService.registration(this.user).subscribe({
      next: () => {
        this.router.navigate(['/login']);
        alert('Registration success');
      },
      error: (err: any) => {
        this.errorMessage = `Failed to registration new account: ${err}`;
      },
    });
  }
}
