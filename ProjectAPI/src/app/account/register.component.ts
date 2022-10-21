import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/_services';
import { User } from '@app/_models';

@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {

    form: FormGroup;
    loading = false;
    submitted = false;
    users :  User[] = [];
    addUserRequest: User = {
        userId : '',
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        role: '',
        status : true,
        password: '',
      };
    
      errorMessage : string = "" ;
    
      Message : string ="";
    
      constructor(
        private accountService : AccountService,
         private router: Router,
         private formBuilder: FormBuilder,
         private alertService: AlertService,
         ) { }
    
    
    
      ngOnInit(): void {
        this.form = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email:['',[Validators.required,Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
            address:['',Validators.required],
            role:['',Validators.required],
            status: ['', Validators.required],
            password: ['', Validators.required]
        });
    
       
    
      }

      get f() { return this.form.controls; }
    
      onSubmit(){
    
        this.accountService.register(this.addUserRequest)
    
        .subscribe({
    
         next: (User) => {
          this.alertService.success('Registered successfully', { keepAfterRouteChange: true });
          this.router.navigate(['User']);
    
         }
    
        });
    
        (error: any) => {
          this.alertService.error(error);    
          this.errorMessage = error.message;    
    
        }
    
       
    
      }
    
}