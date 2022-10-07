import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDetail } from './login-detail';

@Injectable({
  providedIn: 'root'
})
export class LoginDetailService {

  constructor(private http:HttpClient) { }

  formData : LoginDetail = new LoginDetail();
  readonly baseUrl = 'https://localhost:44300/api/User/Login';

  postLogin(){
    return this.http.post(this.baseUrl,this.formData)
  }
}
