import { Component, OnInit } from '@angular/core';

import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';

import { Order } from '../models/order';
import { OrderItem } from '../models/orderitem';
import { LoginService } from '../services/login.service';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orderitem-details',
  templateUrl: './orderitem-details.component.html',
  styleUrls: ['./orderitem-details.component.css']
})
export class OrderitemDetailsComponent implements OnInit {
  public orders: Order[];
  public order: Order;
  public newOrderItem: OrderItem;
  public orderItem: OrderItem;

  public messages: string[];
  public submitMessage: string;
  public isValidFormSubmitted: boolean;
  public headerMessage: string;
  public loadingOrderItem: boolean = false;
  public orderCreating: boolean = false;
  public employee: LoginEmployee;
  public button: string;
  public closeable: boolean;

  public poNumberURL: Number = null;
  public itemAdding: boolean = false;

  public orderItemIDURL: Number = null;
  public itemSaving: boolean = false;

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
      //MODIFY ITEM
      this.headerMessage = 'Edit Order Item'

      this.orderService.getOrders(parseInt(localStorage.getItem("id")), parseInt(localStorage.getItem("supId"))).subscribe(orders => {
        this.orders = orders;

        if(this.poNumberURL != null){
          this.order = this.orders.find(o => o.poNumber == this.poNumberURL)

          if(this.order.poNumber != null){
            this.orderItem = this.order.items.find(i => i.orderItemID == this.orderItemIDURL)
            
            this.button = "Save Item";
            let statuses = this.order.items.map(a => a.statusID)
        
            const isApprovedDenied = (statusID) => statusID == 2 || statusID == 3
            this.closeable = (statuses.every(isApprovedDenied))
            if(this.closeable == true){
              this.ClosePOR()
            }
          }
        }
      }, err =>{
        this.messages = err;
        this.itemSaving = false;
      })
    } else if (this.poNumberURL != null && this.orderItemIDURL == null){
      //ADD ITEM
      this.headerMessage = 'Add Order Item';

      this.orderService.getOrders(parseInt(localStorage.getItem("id")), parseInt(localStorage.getItem("supId"))).subscribe(orders => {
        this.orders = orders;

        if(this.poNumberURL != null){
          this.order = this.orders.find(o => o.poNumber == this.poNumberURL)
          this.orderItem = new OrderItem()

          this.button = "Add Item";
        }
      }, err =>{
        this.messages = err;
        this.itemSaving = false;
      })
    
    } else {
      //CREATE ORDER
      this.headerMessage = 'Create Purchase Order Requisition';
      this.order = new Order();
      this.orderItem = new OrderItem();
      this.button = "Create POR";
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

    this.orderItem = form.value;
    
    this.messages = [];
    this.submitMessage = '';

    if (this.poNumberURL != null && this.orderItemIDURL !=  null){
      this.itemSaving = true;
      this.modifyOrderItem(form);

    } else if (this.poNumberURL != null && this.orderItemIDURL == null) {

      this.itemAdding = true;
      this.addOrderItem(form);

    } else {
      this.order = new Order();
      this.order.items = [];
      this.order.employeeid = parseInt(localStorage.getItem("id"));
      this.order.items.push(this.orderItem)
      this.orderCreating = true;
      this.createPurchaseOrder(form);
    }
  }


  private modifyOrderItem(form: NgForm) {
    //get index of item to update
    //this.itemIndex = this.order.items.findIndex(i => i.orderItemID == this.orderItemIDURL)
    //this.order.items.splice(this.itemIndex, 1, this.orderItem)
    this.orderItem.poNumber = this.order.poNumberFormatted;
    this.order.items.push(this.orderItem)
    this.orderService.modifyOrderItem(this.order).subscribe(
      result => {
        if (result) {
          this.submitMessage = `Item modified. Name: ${this.orderItem.orderItemName}`;
          this.isValidFormSubmitted = true;
          //form.resetForm();
          this.checks()
        } else {
          this.isValidFormSubmitted = false;
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    }).add(() => { this.itemSaving = false; });
  }

  private addOrderItem(form: NgForm) {
    //already have order
    this.orderItem.poNumber = this.order.poNumberFormatted;
    this.order.items.push(this.orderItem);
    this.orderService.addOrderItem(this.order).subscribe(
      result => {
        if (result) {
          this.submitMessage = `Item added. Name: ${this.orderItem.orderItemName}`
          this.isValidFormSubmitted = true;
          //form.resetForm();
          //this.checks()
        } else {
          this.isValidFormSubmitted = false;
          this.messages.push('Duplicate item added. Instead the quantity is changed.')
          //form.resetForm();
          //this.checks()
        }
      }, err => {
        this.messages.push(...err);
      }).add(() => { this.itemAdding = false; });
  }


  private createPurchaseOrder(form: NgForm) {
    this.orderService.createOrder(this.order).subscribe(
      order => {
        console.warn(order.poNumber);
        if (order.poNumber != 0 || order.poNumber != null) {
          this.submitMessage = `order created. poNumber: ${order.poNumber}`;
          //reroute to home page
          this.isValidFormSubmitted = true;
          //form.resetForm();
          //this.checks()
        } else {
          this.isValidFormSubmitted = false;
          this.messages.push('An error occured creating the order via REST API');
        }
      }, err => {
        this.messages.push(...err);
      }).add(() => { this.orderCreating = false; });
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
          //window.location.reload();
          //this.checks()
          
        } else {
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    })
  }

  NoLongerNeeded(): void { 
    this.order.items.push(this.orderItem)
    this.orderService.noLongerNeeded(this.order).subscribe(
      result => {
        if (result) {
          this.submitMessage = `Item modified. Name: ${this.orderItem.orderItemName}`;
          this.isValidFormSubmitted = true;
          //window.location.reload();
          this.checks()
        } else {
          this.isValidFormSubmitted = false;
          this.messages.push('An error occured updating the item via REST API');
        }
      }, err => {
        this.messages.push(...err);
    }).add(() => { this.itemSaving = false; });
  }
  
}
