import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserData } from '../../providers/user-data';
import { UserOptions } from '../../interfaces/user-options';
import { AuthenticationService } from '../../services/authentication.service';
import { RegisterVM } from '../../models/register-vm';

@Component({
  selector: 'page-signup',
  templateUrl: 'signup.html',
  styleUrls: ['./signup.scss'],
})
export class SignupPage {
  signup: UserOptions = { username: '', password: '', FirstName:'', LastName:'',ConfirmPassword:'' };
  submitted = false;

  constructor(
    public router: Router,
    public authService:AuthenticationService,
    public userData: UserData
  ) {}

  onSignup(form: NgForm) {
    this.submitted = true;

    if (form.valid) {
      this.authService.register(new RegisterVM(this.signup.username,this.signup.FirstName, this.signup.LastName,this.signup.password, this.signup.ConfirmPassword,this.signup.username,"0000000000"))
      .subscribe((data=>{
        this.router.navigateByUrl('/app/tabs/books');
      }),
      (error)=>{

      },
      ()=>{

      });

    }
  }
}
