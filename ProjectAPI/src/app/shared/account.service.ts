import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { User } from './register-details';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  constructor(
    private http:HttpClient,
    private router: Router,) {

      this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user') || '{}'));
        this.user = this.userSubject.asObservable();
     }

     
    public get userValue(): User {
      return this.userSubject.value;
  }

  register(user: User) {
    return this.http.post(`${environment.apiUrl}/users/register`, user);
}

  login(username: any, password: any) {
    return this.http.post<User>(`${environment.apiUrl}/users/authenticate`, { username, password })
        .pipe(map(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user));
            this.userSubject.next(user);
            return user;
        }));
}

logout() {
  localStorage.removeItem('user');
  this.userSubject.next(null);
  this.router.navigate(['/account/login']);
}

getAll() {
  return this.http.get<User[]>(`${environment.apiUrl}/users`);
}


update(id: any, params: any) {
  return this.http.put(`${environment.apiUrl}/users/${id}`, params)
      .pipe(map(x => {
          if (id == this.userValue.id) {
              const user = { ...this.userValue, ...params };
              localStorage.setItem('user', JSON.stringify(user));

              this.userSubject.next(user);
          }
          return x;
      }));
}

delete(id: string) {
  return this.http.delete(`${environment.apiUrl}/users/${id}`)
      .pipe(map(x => {
          if (id == this.userValue.id) {
              this.logout();
          }
          return x;
      }));
}
}







