import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { LoginEmployee } from '../models/loginemployee';
import { unescapeIdentifier } from '@angular/compiler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginEmployee: LoginEmployee;
  public username: string;
  public password: string;
  public feedback: string;
  public messages: string[];

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem("id") != null){
      localStorage.clear()
    }
  }

  login() {
    this.feedback = '';
    if (this.username === undefined) {
      this.feedback += "Email cannot be empty.\n";
    }
    if (this.password === undefined) {
      this.feedback += "Password cannot be empty.\n";
    }
    if (this.username != undefined && this.password != undefined) {
      this.feedback = '';
      this.messages = null;
      this.loginService.login(this.username, this.password).subscribe(loginEmployee => {
        this.loginEmployee = loginEmployee;
        
        localStorage.setItem('id', loginEmployee.employeeId.toString());
        localStorage.setItem('supId', loginEmployee.supervisorId.toString());
        
        this.feedback = null;
        if(localStorage.getItem('poNumber') != null){                
          this.router.navigate(["orders/" + localStorage.getItem('poNumber')])
        } else {
          this.router.navigate([""]);
        }
        

      }, err => {
        this.feedback = "Login failed. Email or password incorrect.";
        this.password = undefined;
      }).add(() => {
      })
    }
  }
}
