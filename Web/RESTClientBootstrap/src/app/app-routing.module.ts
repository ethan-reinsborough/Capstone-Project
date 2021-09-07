import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OrdersComponent } from './orders/orders.component';

import { EmployeeSearchComponent } from './employee-search/employee-search.component'; 
import { LoginComponent } from './login/login.component';
import { EmployeeReviewComponent } from './employee-review/employee-review.component';
import { OrderitemDetailsComponent } from './orderitem-details/orderitem-details.component';
import { HomeComponent } from './home/home.component';
import { ProcessComponent } from './process/process.component';
import { EmployeeshowReviewComponent } from './employeeshow-review/employeeshow-review.component';
import { EmployeepModifyComponent } from './employeep-modify/employeep-modify.component';
import { DepartmentModifyComponent } from './department-modify/department-modify.component';
import { OrderitemProcessComponent } from './orderitem-process/orderitem-process.component';


const routes: Routes = [    
    { path: '', component: HomeComponent },  

    //process por item
    { path: 'process/:poNumber/edititem/:orderItemID', component: OrderitemProcessComponent },
       
    //Modify POR Item
    { path: 'orders/:poNumber/edititem/:orderItemID', component: OrderitemDetailsComponent },

    //Modify POR -> Add new item to PO
    { path: 'orders/:poNumber/additem', component: OrderitemDetailsComponent },

    //Create POR
    { path: 'orders/create', component: OrderitemDetailsComponent },

    //Process POR
    { path: 'process/:poNumber', component: ProcessComponent },
    { path: 'process', component: ProcessComponent },
    

    //View PORs, POR item
    { path: 'orders/:poNumber', component: OrdersComponent },  
    { path: 'orders', component: OrdersComponent },

    { path: 'employees/search', component: EmployeeSearchComponent},
    { path: 'employees/review', component: EmployeeReviewComponent},
    { path: 'employees/pmodify', component: EmployeepModifyComponent},
    { path: 'employees/dmodify', component: DepartmentModifyComponent},
    { path: 'employees/sreview', component: EmployeeshowReviewComponent},
    { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
