import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { OrderService } from '../services/order.service';
import {LoginEmployee} from '../models/loginemployee';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public employee: LoginEmployee;

  constructor(private router: Router, private orderService: OrderService) { }

  ngOnInit(): void {
    this.checkIsLoggedIn();
  }
  
  checkIsLoggedIn(){
    if(localStorage.getItem("id") === null){
      this.router.navigate(["/login"]);
    }
  }
}
