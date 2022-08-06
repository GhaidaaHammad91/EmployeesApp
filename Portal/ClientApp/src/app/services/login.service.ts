import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiurl = 'https://localhost:44368/api/users/'


  constructor(private http: HttpClient, private router: Router) { }

  tokenresp: any;
  private _updatemenu = new Subject<void>();

  get updatemenu() {
    return this._updatemenu;
  }

  login(usercred: any) {
    return this.http.post(this.apiurl + 'authenticate', usercred);
  }

  generateRefreshToken() {
    let input = {
      "jwtToken": this.getToken(),
      "refreshToken": this.getRefreshToken()
    }
    return this.http.post(this.apiurl + 'refresh', input);
  }

  isLoggedIn() {
    return localStorage.getItem("token") != null;
  }

  getToken() {
    return localStorage.getItem("token") || '';
  }

  getRefreshToken() {
    return localStorage.getItem("refreshtoken") || '';
  }

  saveTokens(tokendata: any) {
    localStorage.setItem('token', tokendata.jwtToken);
    localStorage.setItem('refreshtoken', tokendata.refreshToken);
  }

  logout() {
    alert('Your session expired')
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }
}
