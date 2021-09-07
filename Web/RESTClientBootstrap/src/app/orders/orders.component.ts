import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';

import { Order } from '../models/order';
import { LoginService } from '../services/login.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  public loadingOrders: boolean;
  public orders: Order[];
  public selectedOrder: Order;
  public messages: string[]
  public poNumber: Number = 0;
  public empId: Number = null
  public employee: LoginEmployee;
  public url: string;

  constructor(
    private orderService: OrderService, 
    private route: ActivatedRoute,
    private loginService: LoginService ) { }

  ngOnInit(): void {  
    this.getEmployeeInfo();
    this.route.params.subscribe(p => {     
      this.poNumber = p['poNumber'];    
      //localStorage.setItem('poNumber', this.poNumber.toString())
      this.getOrders(this.poNumber);

    });
    this.loginService.checkIsLoggedIn();
  }

  getEmployeeInfo(): void {
    this.loginService.getEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
      this.employee = employee;   
      
    })
  }

  getOrders(poNumber: Number): void {
    this.loadingOrders = true;
    this.messages = null;
    this.orderService.getOrders(parseInt(localStorage.getItem("id")), parseInt(localStorage.getItem("supId"))).subscribe(orders => {       
      this.orders = orders;
      if(poNumber != null){    
        this.selectedOrder = this.orders.find(o => o.poNumber == this.poNumber)       
      }         
    }, err => {
      this.messages = [];
      this.messages.push(...err);
    }).add(() => {
      this.loadingOrders = false;
    }) 
  }

}
