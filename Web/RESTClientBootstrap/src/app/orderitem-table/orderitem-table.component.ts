import { Component, OnInit, Input } from '@angular/core';
import { EmployeeReviewComponent } from '../employee-review/employee-review.component';
import { LoginEmployee } from '../models/loginemployee';
import { Order } from '../models/order';


@Component({
  selector: 'app-orderitem-table',
  templateUrl: './orderitem-table.component.html',
  styleUrls: ['./orderitem-table.component.css']
})
export class OrderitemTableComponent implements OnInit {
  @Input() order: Order;
  @Input() employee: LoginEmployee;

  messages: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
