import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { formatCurrency } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;
    baseApiUrl: string = environment.apiUrl;
    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }

    login(username, password) {
        return this.http.post<User>(`${environment.apiUrl}/User/Login`, { username, password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                this.userSubject.next(user);
                return user;
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }

    // register(user: User) {
    //     return this.http.post(`${environment.apiUrl}/User/Register`, user);
    // }

    register(addUserRequest : User) : Observable<User> {

        // addEmployeeRequest.userId = '00000000-0000-0000-0000-000000000000'
    
        return this.http.post<User>(this.baseApiUrl + '/User/Register', addUserRequest);
    
      }

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/User/GetUser`);
    }

    getById(userId: string) {
        return this.http.get<User>(`${environment.apiUrl}/User/GetByIdUser+${userId}`);
    }

    update(userId, params) {
        return this.http.put(`${environment.apiUrl}/User/UpdateUser/+${userId}`, params)
            .pipe(map(x => {

                if (userId == this.userValue.userId) {

                    const user = { ...this.userValue, ...params };
                    localStorage.setItem('user', JSON.stringify(user));

                    this.userSubject.next(user);
                }
                return x;
            }));
    }

    delete(userId: string) {
        return this.http.delete(`${environment.apiUrl}/User/DeleteUser?+${userId}`)
            .pipe(map(x => {

                if (userId == this.userValue.userId) {
                    this.logout();
                }
                return x;
            }));
    }
}