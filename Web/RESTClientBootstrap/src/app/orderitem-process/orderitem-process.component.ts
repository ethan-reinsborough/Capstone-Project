import { Component, OnInit } from '@angular/core';

import { FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';

import { Order } from '../models/order';
import { OrderItem } from '../models/orderitem';
import { LoginService } from '../services/login.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orderitem-process',
  templateUrl: './orderitem-process.component.html',
  styleUrls: ['./orderitem-process.component.css']
})
export class OrderitemProcessComponent implements OnInit {

  public orders: Order[];
  public order: Order;
  public newOrderItem: OrderItem;
  public orderItem: OrderItem;

  public messages: string[];
  public submitMessage: string;
  public isValidFormSubmitted: boolean;
  public loadingOrderItem: boolean = false;
  public orderCreating: boolean = false;
  public employee: LoginEmployee;
  public closeable: boolean;

  private poNumberURL: Number = null;
  public itemAdding: boolean = false;

  private orderItemIDURL: Number = null;
  public itemSaving: boolean = false;

  public formGroup: FormGroup

  constructor(
    public orderService: OrderService, 
    private loginService: LoginService,
    private route: ActivatedRoute) {
    this.isValidFormSubmitted = false;
    this.orderCreating = false;
    this.itemAdding = false;
    this.itemSaving = false;
  }

  ngOnInit(): void {
    this.loginService.checkIsLoggedIn();
    this.getEmployeeInfo(parseInt(localStorage.getItem("id")));
    this.route.params.subscribe(params => {
      this.poNumberURL = params['poNumber'];
      this.orderItemIDURL = params['orderItemID']
      this.checks();
    });
    
  }

  checks() {
    if(this.poNumberURL != null && this.orderItemIDURL !=  null){
      this.orderService.getOrders(parseInt(localStorage.getItem("id")), parseInt(localStorage.getItem("supId"))).subscribe(orders => {
        this.orders = orders;

        if(this.poNumberURL != null){
          this.order = this.orders.find(o => o.poNumber == this.poNumberURL)

          if(this.order.poNumber != null){
            this.orderItem = this.order.items.find(i => i.orderItemID == this.orderItemIDURL)
                
                let statuses = this.order.items.map(a => a.statusID)
        
                const isApprovedDenied = (statusID) => statusID == 2 || statusID == 3
                this.closeable = (statuses.every(isApprovedDenied))
          }
        }
      }, err =>{
        this.messages = err;
        this.itemSaving = false;
      })
    } 
  }


  /**
   * Form Submission event handler
   */
   public onFormSubmit(form: NgForm) {
    this.isValidFormSubmitted = false;
    
    if (form.invalid) {
      return;
    }

    this.orderItem.orderItemName = form.controls['orderItemName'].value
    this.orderItem.orderItemDescription = form.controls['orderItemDescription'].value
    this.orderItem.orderItemJustification = form.controls['orderItemJustification'].value
    
    this.messages = [];
    this.submitMessage = '';

    if (this.poNumberURL != null && this.orderItemIDURL !=  null){
      this.itemSaving = true;
      this.modifyOrderItem(form);
      
    } 

    
  }


  private modifyOrderItem(form: NgForm) {
    //get index of item to update
    //this.itemIndex = this.order.items.findIndex(i => i.orderItemID == this.orderItemIDURL)
    //this.order.items.splice(this.itemIndex, 1, this.orderItem)
    this.orderItem.poNumber = this.order.poNumberFormatted;
    this.orderService.processPOR(this.order).subscribe(
      result => {
        if (result) {
          
          this.submitMessage = `Item modified. Name: ${this.orderItem.orderItemName}`;
          this.isValidFormSubmitted = true;
          form.resetForm();
          window.location.reload();
          this.checks()
          
        } else {
          this.isValidFormSubmitted = false;
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    }).add(() => { this.itemSaving = false; });
  }

  getEmployeeInfo(empId: Number): void {
    this.loginService.getEmployeeInfo(empId).subscribe(employee => {
      this.employee = employee;
    })
  }

  ClosePOR(): void {
    this.orderService.closePOR(this.order).subscribe(
      result => {
        if (result) {
          window.location.reload();
          this.checks()
          
        } else {
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    })
  }

}
