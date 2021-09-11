import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { UserData } from '../../providers/user-data';

import { UserOptions } from '../../interfaces/user-options';
import { AuthenticationService } from '../../services/authentication.service';
import { LoginVM } from '../../models/login-vm';
import { LoginResponse } from '../../models/viewmodels/general_response';
import { HttpResponse } from '@angular/common/http';



@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
  styleUrls: ['./login.scss'],
})
export class LoginPage {
  login: UserOptions = { username: '', password: '',FirstName:'', LastName:'',ConfirmPassword:'' };
  submitted = false;
  errorMessage ="";
  result:any;
  constructor(
    public userData: UserData,
    public authService:AuthenticationService,
    public router: Router
  ) { }

  onLogin(form: NgForm) {
    this.submitted = true;

    if (form.valid) {
      this.userData.login(this.login.username);
      this.authService.login(new LoginVM(this.login.username,this.login.password)).subscribe((data:LoginResponse)=>{
        this.result=data.success;
        if(data.success==true){
        console.log(data);
        this.router.navigateByUrl('/app/tabs/books');
        }
        else{
          this.errorMessage=data.message;          
        }
      },(error)=>{

      },()=>{

      })
      
    }
  }

  onSignup() {
    this.router.navigateByUrl('/signup');
  }
}
