import { Component, OnInit } from '@angular/core';
import { LoginEmployee } from '../models/loginemployee';
import { Rating } from '../models/rating';
import { EmployeeService } from '../services/employee.service';
import { LoginService } from '../services/login.service';
import { Review } from '../models/review';
import { Router } from '@angular/router';


@Component({
  selector: 'app-employee-review',
  templateUrl: './employee-review.component.html',
  styleUrls: ['./employee-review.component.css']
})
export class EmployeeReviewComponent implements OnInit {

  public employees: LoginEmployee[];
  public validateEmp: LoginEmployee;
  public review: Review;
  public reviewingEmployee: LoginEmployee;
  public ratings: Rating[];
  public belowExpectations: Rating;
  public meetsExpectations: Rating;
  public exceedsExpectations: Rating;
  public selectedRating: Rating;
  public feedback: String;
  public selectedEmployee: LoginEmployee;
  public comment: String;
  public positive: Boolean;
  public negative: Boolean;

  constructor(private employeeService: EmployeeService, private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.checkIsSupervisor();
    this.populateRatings();
    this.loadEmployees();
  }

  checkIsSupervisor() {
    if (localStorage.getItem("id") !== null) {
      this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
        this.validateEmp = employee;
        if (employee.supervisorId != 10000000) {
          this.router.navigate(['']);
        }
      });
    }
  }

  populateRatings() {
    this.ratings = [];
    this.belowExpectations = new Rating();
    this.meetsExpectations = new Rating();
    this.exceedsExpectations = new Rating();

    this.belowExpectations.score = 1;
    this.belowExpectations.desc = "Below expectations";

    this.meetsExpectations.score = 2;
    this.meetsExpectations.desc = "Meets expectations";

    this.exceedsExpectations.score = 3;
    this.exceedsExpectations.desc = "Exceeds expectations";

    this.ratings.push(this.belowExpectations);
    this.ratings.push(this.meetsExpectations);
    this.ratings.push(this.exceedsExpectations);
  }

  loadEmployees() {
    if (localStorage.getItem("id") !== null) {
      this.loginService.getAllEmployeeInfo(parseInt(localStorage.getItem("id"))).subscribe(employee => {
        this.reviewingEmployee = employee;
        console.log(this.reviewingEmployee)
        this.employeeService.getEmployeesForReview(this.reviewingEmployee.employeeId).subscribe(employees => {
          this.employees = employees;
          console.log(this.employees);
        })
      })
    }
  }

  create() {
    this.feedback = "";
    this.positive = false;
    if (this.selectedEmployee === undefined) {
      this.feedback = "Please select an employee to review.";
      this.negative = true;
    }
    if (this.selectedRating === undefined) {
      this.feedback += "\nPlease assign a rating."
      this.negative = true;
    }
    if (this.comment === undefined) {
      this.feedback += "\nPlease write a comment.";
      this.negative = true;
    }
    else {
      this.feedback = "";
      this.negative = false;
      this.positive = true;
      this.review = new Review();

      this.review.EmployeeID = Number(this.selectedEmployee);
      this.review.ReviewDate = new Date().toISOString().slice(0, 10);
      this.review.supervisorId = String(this.reviewingEmployee.employeeId);
      this.review.performanceRating = String(this.selectedRating);
      this.review.Comment = this.comment;
      console.log(this.review);
      this.employeeService.addReview(this.review).subscribe(result => {
        if (result) {
          this.feedback = "Review created successfully.";
        } else {
          this.feedback = "Review creation failed.";
        }
      })
    }
  }
}
