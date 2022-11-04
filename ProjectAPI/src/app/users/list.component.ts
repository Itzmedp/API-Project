import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AccountService, AlertService } from '@app/_services';
import { Router } from '@angular/router';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    users = null;

    constructor(
        private accountService: AccountService,
        private alertService: AlertService,
        private router: Router,
        ) {}

    ngOnInit() {
        this.accountService.getAll()
            .pipe(first())
            .subscribe(users => this.users = users);            
    }

    deleteUser(userId: string) {
        const user = this.users.find(x => x.id === userId);
        this.accountService.delete(userId)
            .pipe(first())
            .subscribe(() => {
                this.alertService.success('Deleted successfully', { keepAfterRouteChange: true });
                this.users = this.users.filter(x => x.id !== userId)});
                this.router.navigate(['list']);
    }

    isDisabled(): boolean {
        var userRole = localStorage.getItem(`role`);
        if (userRole == 'Admin') {
        return true;
        } else {
        return false;
        }
    }
}