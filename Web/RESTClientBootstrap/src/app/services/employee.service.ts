import { Injectable } from '@angular/core';

//Observables fucntionality
import { Observable, of } from 'rxjs';

//Http Comms to REST
import { HttpClient } from '@angular/common/http';

//Constant and SharedService class
import { API_URI, SharedService } from './shared.service';

//Import for Error Handling for rxjs and observables
import { catchError } from 'rxjs/operators';
import { SearchEmployee } from '../models/searchemployee';
import { LoginEmployee } from '../models/loginemployee';
import { Department } from '../models/department';
import { Review } from '../models/review';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends SharedService {

  constructor(private http: HttpClient) {
    super();
  }

  public searchEmployee(searchPhrase: String): Observable<SearchEmployee> {
    //const apiMethod = `${API_URI}/api/Employees/${searchPhrase}`;
    const apiMethod = `${API_URI}/api/Employees/${searchPhrase}`;
    return this.http.get<SearchEmployee>(apiMethod).pipe(catchError(super.handleError));
  }

  public searchEmployees(searchPhrase: String): Observable<SearchEmployee[]> {
    const apiMethod = `${API_URI}/api/Employees/byName/${searchPhrase}`;
    return this.http.get<SearchEmployee[]>(apiMethod).pipe(catchError(super.handleError));
  }

  public modifyPersonalEmployee(employee: LoginEmployee): Observable<boolean> {
    const apiMethod = `${API_URI}/api/Employees/Update/Personal/${employee.employeeId}`;
    return this.http.put<any>(apiMethod, employee, super.httpOptions()).pipe(catchError(super.handleError));
  }

  public modifyDepartment(department: Department): Observable<boolean> {
    const apiMethod = `${API_URI}/api/Employees/Update/Department/${department.departmentId}`;
    return this.http.put<any>(apiMethod, department, super.httpOptions()).pipe(catchError(super.handleError));
  }

  public getDepartments(): Observable<Department[]> {
    const apiMethod = `${API_URI}/api/Employees/Info/Departments`;
    return this.http.get<Department[]>(apiMethod).pipe(catchError(super.handleError));
  }

  public getDepartmentById(deptId: Number): Observable<Department> {
    const apiMethod = `${API_URI}/api/Employees/Departments/byId/${deptId}`;
    return this.http.get<Department>(apiMethod).pipe(catchError(super.handleError));
  }

  public addReview(review: Review): Observable<boolean> {
    const apiMethod = `${API_URI}/api/Employees/AddReview`;
    return this.http.post<any>(apiMethod, review, super.httpOptions())
            .pipe(catchError(super.handleError));
  }

  public getEmployeesForReview(supervisorId: Number): Observable<LoginEmployee[]> {
    const apiMethod = `${API_URI}/api/Employees/EmployeesForReview/${supervisorId}`;
    return this.http.get<LoginEmployee[]>(apiMethod).pipe(catchError(super.handleError));
  }

  public getReviews(employeeId: Number): Observable<Review[]> {
    //const apiMethod = `${API_URI}/api/Employees/${searchPhrase}`;
    const apiMethod = `${API_URI}/api/Employees/Info/Reviews/${employeeId}`;
    return this.http.get<Review[]>(apiMethod).pipe(catchError(super.handleError));
  }
}
