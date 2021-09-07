import { Component, OnInit } from '@angular/core';
import { Review } from '../models/review';
import { EmployeeService } from '../services/employee.service';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { LoginEmployee } from '../models/loginemployee';
import { ITS_JUST_ANGULAR } from '@angular/core/src/r3_symbols';

@Component({
  selector: 'app-employeeshow-review',
  templateUrl: './employeeshow-review.component.html',
  styleUrls: ['./employeeshow-review.component.css']
})
export class EmployeeshowReviewComponent implements OnInit {

  public reviews : Review[];
  public feedback : string;
  public employee : LoginEmployee;

  constructor(private employeeService: EmployeeService, private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
    this.checkIsLoggedIn();
    this.searchReview();
  }

  checkIsLoggedIn(){
    if(localStorage.getItem("id") === null){
      this.router.navigate(["/login"]);
    }
  }

  searchReview(){
    this.loginService.getEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
      this.employee = employee; 
      
      console.log(this.employee); 
      this.employeeService.getReviews(parseInt(localStorage.getItem("id"))).subscribe(reviews => {
         
        this.reviews = reviews;
        for(let i = 0; i < reviews.length; i++){
          this.reviews[i].supervisorId = this.employee.supervisor; 
          if(this.reviews[i].performanceRating === "1") {
            this.reviews[i].performanceRating = "Below expectations";
          } 
          if(this.reviews[i].performanceRating === "2") {
            this.reviews[i].performanceRating = "Meets expectations";
          } 
          if(this.reviews[i].performanceRating === "3") {
            this.reviews[i].performanceRating = "Exceeds expectations";
          } 
        }     
      }, err =>{
        this.feedback = err;
      }).add(() =>{});
    })
  }
}
