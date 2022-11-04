import { Component } from '@angular/core';

import { AccountService, AlertService } from './_services';
import { User } from './_models';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    user: User;

    constructor(private accountService: AccountService,private alertService: AlertService) {
        this.accountService.user.subscribe(x => this.user = x);
    }

    logout() {
        this.accountService.logout();
        this.alertService.info("Logout Successfully")
    }
}