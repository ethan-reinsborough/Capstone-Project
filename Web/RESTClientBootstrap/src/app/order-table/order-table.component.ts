import { Component, OnInit, Input } from '@angular/core';
import { NgControl, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';
import { Order } from '../models/order';
import { LoginService } from '../services/login.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-order-table',
  templateUrl: './order-table.component.html',
  styleUrls: ['./order-table.component.css']
})
export class OrderTableComponent implements OnInit {
  @Input() orders: Order[];
  public searchValue: string;
  public employee: LoginEmployee;
  public startDate: string;
  public endDate: string;
  public poNumber: Number;
  
  public isValidDates: boolean;
  public messages: string[] = [];

  constructor(private orderService: OrderService,
    private loginService: LoginService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getEmployeeInfo(parseInt(localStorage.getItem("id")));
    if(this.orders.length == 0){
      this.messages.push("No Orders Returned From API")
    }
    this.route.params.subscribe(p => {
      this.poNumber = p['poNumber'];
    });
    
  }
 
  public onFormSubmit(form: NgForm) {
    this.isValidDates = false;
    this.startDate = form.value.startDate.replace(/\//g, "");
    this.endDate = form.value.endDate.replace(/\//g,"")
    
    if(form.invalid){
      return;
    }

    this.getOrdersByDate(form)
  }

  private getOrdersByDate(form: NgForm) {
    this.orderService.getOrdersByDate(parseInt(localStorage.getItem("id")), this.startDate, this.endDate)
      .subscribe(
        orders => {
          this.orders = orders;
          
          if(this.orders.length == 0){
            this.messages.push("No records found in that date range")
          }
        }
      )
  }

  getEmployeeInfo(empId: Number): void {
    this.loginService.getEmployeeInfo(empId).subscribe(employee => {
      this.employee = employee;
    })
  }

  // deleteorder(id: String) {
  //   this.messages = [];
  //   this.deletingorder = true;

  //   this.orderService.deleteorder(id).subscribe(result => {
  //     if (result) {
  //       this.orders.splice(this.orders.indexOf(this.orders.find(p => p.id == id)), 1)
  //     } else {
  //       this.messages.push('Deletion failed');
  //     }
  //   }, err => {
  //     this.messages.push(...err);
  //   }).add(() => {
  //     this.deletingorder = false;
  //   })
  // }
}
