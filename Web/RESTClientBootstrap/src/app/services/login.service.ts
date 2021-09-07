import { Injectable } from '@angular/core';

//Observables fucntionality
import { Observable, of } from 'rxjs';

//Http Comms to REST
import { HttpClient } from '@angular/common/http';

//Constant and SharedService class
import { API_URI, SharedService } from './shared.service';

//Import for Error Handling for rxjs and observables
import { catchError } from 'rxjs/operators';
import { LoginEmployee } from '../models/loginemployee';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends SharedService {

  constructor(private http: HttpClient, private router: Router) {
    super();
  }

  public login(username: String, password: String): Observable<LoginEmployee> {
    const apiMethod = `${API_URI}/api/Login/${username}/${password}`;
    return this.http.get<LoginEmployee>(apiMethod).pipe(catchError(super.handleError));
  }

  public checkIsLoggedIn() {
    if (localStorage.getItem("id") === null) {
      this.router.navigate(["/login"]);
    }
  }

  public getEmployeeInfo(empId: Number): Observable<LoginEmployee> {
    const apiMethod = `${API_URI}/api/Login/${empId}`;
    return this.http.get<LoginEmployee>(apiMethod).pipe(catchError(super.handleError));
  }

  public getAllEmployeeInfo(empId: Number): Observable<LoginEmployee> {
    const apiMethod = `${API_URI}/api/Login/Info/${empId}`;
    return this.http.get<LoginEmployee>(apiMethod).pipe(catchError(super.handleError));
  }
}
