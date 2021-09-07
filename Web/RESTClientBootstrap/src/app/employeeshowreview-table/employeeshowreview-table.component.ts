import { Component, Input, OnInit } from '@angular/core';
import { Review } from '../models/review';

@Component({
  selector: 'app-employeeshowreview-table',
  templateUrl: './employeeshowreview-table.component.html',
  styleUrls: ['./employeeshowreview-table.component.css']
})
export class EmployeeshowreviewTableComponent implements OnInit {
  
  @Input() reviews: Review[];
  constructor() { }

  ngOnInit(): void {
  }

}
