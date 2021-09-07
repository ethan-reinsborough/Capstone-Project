import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderTableComponent } from './order-table/order-table.component';
import { OrderitemDetailsComponent } from './orderitem-details/orderitem-details.component';
import { OrderitemTableComponent } from './orderitem-table/orderitem-table.component';
//import { OrderitemsComponent } from './orderitems/orderitems.component';

//Import needed to convert to REST
import { HttpClientModule } from '@angular/common/http';

//Forms Module for Binding
import { FormsModule } from '@angular/forms';

//Service for Order
import { OrderService } from './services/order.service';

//Service for Login
import { LoginService } from './services/login.service';
//Service for Employee
import { EmployeeService } from './services/employee.service';
import { EmployeeSearchComponent } from './employee-search/employee-search.component';

import { EmployeesearchTableComponent } from './employeesearch-table/employeesearch-table.component';
import { EmployeeReviewComponent } from './employee-review/employee-review.component';
import { SearchfilterPipe } from './searchfilter.pipe';
import { ProcessComponent } from './process/process.component';
import { EmployeepModifyComponent } from './employeep-modify/employeep-modify.component';
import { DepartmentModifyComponent } from './department-modify/department-modify.component';
import { OrderitemProcessComponent } from './orderitem-process/orderitem-process.component';
import { EmployeeshowReviewComponent } from './employeeshow-review/employeeshow-review.component';
import { EmployeeshowreviewTableComponent } from './employeeshowreview-table/employeeshowreview-table.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    OrdersComponent,
    OrderTableComponent,
    EmployeeSearchComponent,
    OrderitemDetailsComponent,
    OrderitemTableComponent,
    EmployeeSearchComponent,
    EmployeesearchTableComponent,
    EmployeeReviewComponent,
    SearchfilterPipe,
    ProcessComponent,
    EmployeepModifyComponent,
    DepartmentModifyComponent,
    OrderitemProcessComponent,
    EmployeeshowReviewComponent,
    EmployeeshowreviewTableComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule    
  ],
  providers: [OrderService, EmployeeService, LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
