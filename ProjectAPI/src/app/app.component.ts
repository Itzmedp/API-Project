import { Component } from '@angular/core';
import { AccountService } from './shared/account.service';
import { User } from './shared/register-details';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  user: User = new User;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe((x: User) => this.user = x);
}

logout() {
    this.accountService.logout();
}
}
