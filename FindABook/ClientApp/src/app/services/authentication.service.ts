import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { LoginVM } from '../models/login-vm';
import { RegisterVM } from '../models/register-vm';
import { User } from '../models/user-vm';
import { JwtTokenVM } from '../models/viewmodels/contact-vm';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private _loginurl = environment.baseAPIUrl + 'UserAuth/Login';
  private _registerUrl = environment.baseAPIUrl + 'UserAuth/Register';
  redirectUrl: string;
  constructor(private http: HttpClient) { }


  public getToken(): string {
    return localStorage.getItem('token');
  }

  public getUserDetail(): JwtTokenVM {
    const helper = new JwtHelperService();
    const token = this.getToken();
    const tokenDetails = helper.decodeToken(token);
    console.log(tokenDetails);
    return tokenDetails;
  }

  public getUserDetails(): string {
    const helper = new JwtHelperService();
    const token = this.getToken();
    const tokenDetails = helper.decodeToken(token);
    console.log(tokenDetails);
    return localStorage.getItem('token');
  }

  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();

    if (token == null) {
      return false;
    }
    // return a boolean reflecting 
    // whether or not the token is expired
    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(token);

    return !isExpired;
  }

  public login(userDetails:LoginVM): Observable<any> {
    return this.http.post<any>(this._loginurl, userDetails)
      .pipe(map(user => {

        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('token', user.token);
        }
        return user;
      }));
  }

  public register(userDetails:RegisterVM):Observable<HttpResponse<any>> {
    return this.http.post<any>(this._registerUrl,userDetails)
    .pipe(map(user => {
      //login successful if there's a jwt token in the response
      if (user && user.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('token', user.token);
      }
      return user;
    }));
  }
}
