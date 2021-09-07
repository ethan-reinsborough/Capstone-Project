import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { SearchEmployee } from '../models/searchemployee';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { LoginEmployee } from '../models/loginemployee';

@Component({
  selector: 'app-employeep-modify',
  templateUrl: './employeep-modify.component.html',
  styleUrls: ['./employeep-modify.component.css']
})

export class EmployeepModifyComponent implements OnInit {

  public personalEmployee: LoginEmployee;
  public fullName: String;
  public workPhone: String;
  public cellPhone: String;
  public streetAddress: String;
  public municipality: String;
  public province: String;
  public country: String;
  public feedback: String;
  public recordVersion: String;
  public positive: boolean;
  public negative: boolean;

  constructor(private employeeService: EmployeeService, private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
    this.loadFields()
  }

  loadFields() {
    this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
      this.personalEmployee = employee;
      //Sets fields to current record
      this.fullName = this.personalEmployee.fullName;
      this.workPhone = this.personalEmployee.workPhone;
      this.cellPhone = this.personalEmployee.cellPhone
      this.streetAddress = this.personalEmployee.streetAddress;
      this.municipality = this.personalEmployee.municipality;
      this.province = this.personalEmployee.province;
      this.country = this.personalEmployee.country;
      this.recordVersion = this.personalEmployee.recordVersion;

    }, err => {
      //Front end validation for now
      console.log(err);
    }).add(() => { })
  }

  update() {
    this.positive = false;
    this.feedback = "";
    if (this.workPhone === "") {
      this.feedback = "Work phone is required.";
    } else if (this.cellPhone === "") {
      this.feedback += "\nCell phone is required."
    } else if (this.streetAddress === "") {
      this.feedback += "\nStreet address is required."
    } 
    else if (this.municipality === "") {
      this.feedback += "\nMunicipality is required."
    }
    else if (this.province === "") {
      this.feedback += "\nProvince is required."
      this.negative = true;
    }
    else if (this.country === "") {
      this.feedback += "\nCountry is required."
    }
    else {
      this.negative = false;
      this.positive = true;
      //Sets object to fields (with updated values if there are any)
      this.personalEmployee.employeeId = parseInt(localStorage.getItem("id"));
      this.personalEmployee.workPhone = this.workPhone;
      this.personalEmployee.cellPhone = this.cellPhone;
      this.personalEmployee.streetAddress = this.streetAddress;
      this.personalEmployee.municipality = this.municipality;
      this.personalEmployee.province = this.province;
      this.personalEmployee.country = this.country;
      console.log(this.personalEmployee);
      this.employeeService.modifyPersonalEmployee(this.personalEmployee).subscribe(result => {
        if (result) {
          this.feedback = "Updated successfully.";
          this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
            this.personalEmployee = employee;           
            this.recordVersion = this.personalEmployee.recordVersion;
          }, err => {
            //Front end validation for now
            console.log(err);
          }).add(() => { })
        } else {
          this.feedback = "Update failed.";
        }
      }, err =>{
        this.feedback = err;
        this.positive = false;
        this.negative = true;
      })
    }
  }
}
