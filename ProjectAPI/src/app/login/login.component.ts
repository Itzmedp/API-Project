import { Component, OnInit } from '@angular/core';
import { LoginDetailService } from '../shared/login-detail.service';
import { NgForm } from '@angular/forms'
 
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public service : LoginDetailService) { }

  ngOnInit(): void {
  }

  onSubmit(form : NgForm){
    this.service.postLogin().subscribe(
      (res)=>{
        console.log("Submited Successfully");
      },
      (err) =>{
        console.log(err);
        
      }
    )

  }

}
