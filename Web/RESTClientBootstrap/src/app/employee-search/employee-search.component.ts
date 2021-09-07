import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { SearchEmployee } from '../models/searchemployee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-search',
  templateUrl: './employee-search.component.html',
  styleUrls: ['./employee-search.component.css']
})
export class EmployeeSearchComponent implements OnInit {

  public loadingEmployees : boolean;
  public employee : SearchEmployee;
  public employees : SearchEmployee[];
  public messages : string[];
  public searchPhrase : string;
  public isMultiple : boolean;
  public feedback : string;
  public checkResult : boolean;

  constructor(private employeeService: EmployeeService, private router: Router) { }

  ngOnInit(): void {
    this.checkIsLoggedIn();
  }

  checkIsLoggedIn(){
    if(localStorage.getItem("id") === null){
      this.router.navigate(["/login"]);
    }
  }
  searchEmployee(){
    if(this.searchPhrase === undefined){
      //Clears form
      this.feedback = "Please input an EmployeeID or Last Name."
      this.employee = null;
      this.employees = null;
      return;
    } 
    this.feedback = null;
    this.loadingEmployees = true;
    //Is the result multiple employees?
    this.isMultiple = isNaN(parseInt(this.searchPhrase));
    this.messages = null;

    if(this.isMultiple){
      //If it is multiple employees, set the single employee to null
      this.employee = null;
      this.checkResult = false;
      this.employeeService.searchEmployees(this.searchPhrase).subscribe(employees => {
        this.employees = employees
      }, err =>{
        //Front end validation for now
        //console.log(err);
        //this.messages = [];
        //this.messages.push(...err);
      }).add(() =>{
        this.searchPhrase = undefined;
        this.loadingEmployees = false;
      })
    } else{
      this.checkResult = true;
      //If it is only one employee, set the list to null
      this.employees = null;
      this.employeeService.searchEmployee(this.searchPhrase).subscribe(employee => {
      this.employee = employee
      if(this.employee != null){
        this.checkResult = false;
      }
      }, err =>{
        //
      }).add(() =>{
        this.loadingEmployees = false;
      })
    }   
  }
}
