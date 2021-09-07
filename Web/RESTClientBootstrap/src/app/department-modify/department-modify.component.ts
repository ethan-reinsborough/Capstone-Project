import { Component, OnInit, Input } from '@angular/core';
import { Department } from '../models/department';
import { EmployeeService } from '../services/employee.service';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { LoginEmployee } from '../models/loginemployee';

@Component({
  selector: 'app-department-modify',
  templateUrl: './department-modify.component.html',
  styleUrls: ['./department-modify.component.css']
})
export class DepartmentModifyComponent implements OnInit {

  public departments: Department[];
  public department: Department;
  public employee: LoginEmployee;
  public selectedDepartment: Department;
  public feedback: String;
  public name: String;
  public description: String;
  public invocationDate: Date;
  public selectedIndex: number;
  public isNonHRSupervisor: boolean;
  public recordVersion: String;
  public readOnlyDate: Date;
  public readOnlyName: String;
  public positive : boolean;
  public negative: boolean;

  //@Input() department: Department;
  //@Input() departments: Department[];

  constructor(private employeeService: EmployeeService, private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.checkIsLoggedIn();
    this.checkIsSupervisor();
    this.loadFields();
  }

  checkIsLoggedIn() {
    if (localStorage.getItem("id") === null) {
      this.router.navigate(["/login"]);
    }
  }

  checkIsSupervisor() {
    if (localStorage.getItem("id") !== null) {
      this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
        this.employee = employee;
        if (employee.departmentId != 1) {
          if (employee.supervisorId != 10000000) {
            this.router.navigate(['']);
          } else {
            this.isNonHRSupervisor = true;
            console.log(this.isNonHRSupervisor);
          }
        }
      });
    }
  }

  loadFields() {
    this.employeeService.getDepartments().subscribe(departments => {

      this.departments = departments;
      console.log(this.employee);
      if (this.isNonHRSupervisor === true) {
        this.departments = this.departments.slice(Number(this.employee.departmentId) - 1, Number(this.employee.departmentId))
      }
    }, err => {
      //Front end validation for now
      console.log(err);
    }).add(() => { })
  }

  update() {
    this.positive = false;
    if (this.selectedDepartment === undefined) {
      this.feedback = "Please select a department.";
      this.negative = true;
    } if (!this.isNonHRSupervisor && (this.invocationDate < this.departments[Number(this.selectedDepartment) - 1].invocationDate)) {
      console.log(this.invocationDate);
      console.log(this.departments[Number(this.selectedDepartment)].invocationDate);
      this.feedback += "\nInvocation date cannot be before the original invocation date.";
      this.negative = true;
    } else {
      this.feedback = "";
      if (this.name === "") {
        this.feedback = "Department name cannot be empty.";
        this.negative = true;
      } else if (this.description === "") {
        this.feedback += "\nDepartment description cannot be empty."
        this.negative = true;
      } else if (this.invocationDate === undefined) {
        this.feedback += "\nDepartment invocation date cannot be empty.";
        this.negative = true;
      } else {
        this.feedback = "";
        this.negative = false;
        this.positive = true;
        this.department = new Department();
        this.selectedIndex = Number(this.selectedDepartment);
        if (this.isNonHRSupervisor === true) {
          this.department.departmentId = this.selectedIndex;
          this.department.name = this.departments[0].name
          this.department.description = this.description;
          this.department.invocationDate = this.departments[0].invocationDate;
          this.department.recordVersion = this.departments[0].recordVersion;
          console.log(this.recordVersion);
        } else {
          this.department.departmentId = this.selectedIndex;
          this.department.name = this.name;
          this.department.description = this.description;
          this.department.invocationDate = this.invocationDate;
          this.department.recordVersion = this.recordVersion;
          console.log("Previous record version: " + this.recordVersion);
          console.log(this.department);
        }
        this.employeeService.modifyDepartment(this.department).subscribe(result => {
          if (result) {
            this.feedback = "Updated successfully";
            this.employeeService.getDepartments().subscribe(departments => {
              this.departments = departments;
              if (this.isNonHRSupervisor === true) {
                this.departments = this.departments.slice(Number(this.employee.departmentId) - 1, Number(this.employee.departmentId))
              }
              this.selectedIndex = Number(this.selectedDepartment);
              if (this.isNonHRSupervisor === true) {
                this.recordVersion = this.departments[0].recordVersion;
              } else{
                this.recordVersion = this.departments[this.selectedIndex -1].recordVersion;
                console.log("Selected dept name: " + this.departments[this.selectedIndex -1].name);
              }
              
              console.log("New record version: " + this.recordVersion);
            }, err => {
              //Front end validation for now
              console.log(err);
            }).add(() => { })
          } else {
            this.feedback = "Update failed";
          }
        }, err =>{
          this.feedback = err;
          this.negative = true;
          this.positive = false;
        })
      }
    }
  }

  reloadDepartments() {
    this.selectedIndex = Number(this.selectedDepartment);
    console.log(this.departments);
    console.log(this.selectedIndex);
    if (this.selectedDepartment === undefined) {
      this.feedback = "Please select a department.";
    } else {
      //Duplicate code, add method later
      if (this.isNonHRSupervisor === true) {
        this.feedback = "";
        //If the supervisor is not from HR, they should only be able to select their department.
        this.name = this.departments[0].name
        this.description = this.departments[0].description;
        this.invocationDate = this.departments[0].invocationDate;
        this.recordVersion = this.departments[0].recordVersion;
        this.readOnlyName = this.departments[0].name
        this.readOnlyDate = this.departments[0].invocationDate;
      } else {
        this.feedback = "";
        this.name = this.departments[this.selectedIndex - 1].name;
        this.description = this.departments[this.selectedIndex - 1].description;
        this.invocationDate = this.departments[this.selectedIndex - 1].invocationDate;
        this.recordVersion = this.departments[this.selectedIndex - 1].recordVersion;
      }
    }
  }
}
