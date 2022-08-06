import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  responsedata: any;
  constructor(private service: LoginService, private route: Router) {
    localStorage.clear();
  }

  ngOnInit(): void {
  }

  loginform = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  login() {
    if (this.loginform.valid) {
      this.service.login(this.loginform.value).subscribe(result => {
        this.responsedata = result;
        if (this.responsedata != null) {
          localStorage.setItem('token', this.responsedata.jwtToken);
          localStorage.setItem('refreshtoken', this.responsedata.refreshToken);
          this.service.updatemenu.next();
          this.route.navigate(['']);
        } else {
          alert("login Failed");
        }
      });
    }
  }

}
