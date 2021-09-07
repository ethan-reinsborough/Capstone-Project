import { Component, OnInit, Input } from '@angular/core';
import { SearchEmployee } from '../models/searchemployee';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employeesearch-table',
  templateUrl: './employeesearch-table.component.html',
  styleUrls: ['./employeesearch-table.component.css']
})
export class EmployeesearchTableComponent implements OnInit {

  @Input() employee: SearchEmployee;
  @Input() employees: SearchEmployee[];

  messages: string[] = [];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
  }

}
