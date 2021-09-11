import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from "@auth0/angular-jwt";
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _loginurl = 'https://localhost:44382/api/UserAuth/Login';
  private _registerurl = 'https://localhost:44382/api/UserAuth/Register';
  configService: any;
  headers: any;
  config: any;
  workshopresult : any;

  constructor(private http: HttpClient) {
   }

   public getToken(): string {
    return localStorage.getItem('token');
    
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(token);

    return isExpired;
  }

 public login(EmailAddress: string, password: string):Observable<HttpResponse<any>> {
    console.log("about to post");

    return this.http.post<any>(this._loginurl,{EmailAddress,password})
    .pipe(map(user => {
      
    if (user && user.token) {
       localStorage.setItem('token', user.token);
    }

    return user;
    }));
 }

 logout() {
  localStorage.removeItem('token'); 
}

 public register(EmailAddress:string,FirstNames:string,LastName:string,Password:string,ConfirmPassword:string)
 {
   return this.http.post<any>(this._registerurl,{EmailAddress,FirstNames,LastName,Password,ConfirmPassword});
 }
}
