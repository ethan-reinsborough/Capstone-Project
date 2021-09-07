import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';
import { Order } from '../models/order';
import { SearchParam } from '../models/searchparam';
import { LoginService } from '../services/login.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['./process.component.css']
})
export class ProcessComponent implements OnInit {
  public loadingItems: boolean;
  public orders: Order[];
  public selectedOrder: Order;
  public poNumber: Number = null;
  public employee: LoginEmployee;
  public messages: string[];
  public searchParam: SearchParam;
  public closeable: boolean = false;


  public poNumberSearch: string;
  public searchEmp: string;
  public startDate: string;
  public endDate: string;
  public statusId: Number;

  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService) { }

  ngOnInit(): void {
    this.checkIsSupervisor();
    this.loginService.checkIsLoggedIn();
    this.getEmployeeInfo(parseInt(localStorage.getItem("id")));
    this.route.params.subscribe(p => {
      this.poNumber = p['poNumber'];
      this.getItems();
    });
    
    

  }

  checkIsSupervisor() {
    if (localStorage.getItem("id") !== null) {
      this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
        this.employee = employee;
        if (employee.supervisorId != 10000000) {
          this.router.navigate(['']);
        }
      });
    }
  }

  onFormSubmit(form: NgForm) {
    this.startDate = form.value.startDate.replace(/\//g, "");
    this.endDate = form.value.endDate.replace(/\//g, "")
    this.statusId = form.value.statusId

    this.searchParam = new SearchParam();
    this.searchParam.empId = parseInt(localStorage.getItem("id"))
    this.searchParam.poNumber = form.value.poNumberSearch
    this.searchParam.searchEmp = form.value.searchEmp
    this.searchParam.start = this.startDate
    this.searchParam.end = this.endDate
    this.searchParam.statusId = form.value.statusId

    if (form.value.statusId == 0) {
      return;
    }

    this.getOrders()
  }

  getEmployeeInfo(empId: Number): void {
    this.loginService.getEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
      this.employee = employee;
    })
  }

  getOrders(): void {
    this.loadingItems = true;
    this.messages = null;

    this.orderService.getProcessPORs(this.searchParam).subscribe(orders => {
      this.orders = orders;

    }, err => {
      this.messages = err;
      this.loadingItems = false;
    })
  }

  getItems(): void {
    if (this.poNumber != null) {
      this.searchParam = new SearchParam()
      this.searchParam.empId = parseInt(localStorage.getItem("id"))
      this.searchParam.poNumber = this.poNumber
      this.searchParam.statusId = 10
      this.searchParam.searchEmp = ''

      this.orderService.getProcessPORs(this.searchParam).subscribe(o => {
        this.orders = o;
        this.selectedOrder = this.orders[0]

        let statuses = this.selectedOrder.items.map(a => a.statusID)

        const isApprovedDenied = (statusID) => statusID == 2 || statusID == 3
        this.closeable = (statuses.every(isApprovedDenied))

      }, err => {
        this.messages = err;
        this.loadingItems = false;
      })

    }
  }

  ClosePOR(): void {
    this.orderService.closePOR(this.selectedOrder).subscribe(
      result => {
        if (result) {
          
          
        } else {
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    })
  }
}

