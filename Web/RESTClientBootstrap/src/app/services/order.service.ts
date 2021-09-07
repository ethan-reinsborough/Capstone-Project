import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { HttpClient, HttpParams } from '@angular/common/http';

import { API_URI, SharedService } from './shared.service';

import { catchError } from 'rxjs/operators';
import { Order } from '../models/order';
import { SearchParam } from '../models/searchparam';

@Injectable({
    providedIn: 'root'
})
export class OrderService extends SharedService {

    constructor(private http: HttpClient) {
        super();
    }

    public createOrder(order: Order): Observable<Order> {
        const apiMethod = `${API_URI}/api/Orders/Create`;
        return this.http.post<Order>(apiMethod, order, super.httpOptions())
            .pipe(catchError(super.handleError));
    }

    public modifyOrderItem(order: Order): Observable<boolean> {
        const apiMethod = `${API_URI}/api/Orders/Update/${order.poNumber}`;
        return this.http.put<any>(apiMethod, order, super.httpOptions()).pipe(catchError(super.handleError));
    }

    public addOrderItem(order: Order): Observable<boolean> {
        const apiMethod = `${API_URI}/api/Orders/AddItem/${order.poNumber}`;
        return this.http.post<any>(apiMethod, order, super.httpOptions())
            .pipe(catchError(super.handleError));
    }

    public getOrdersByDate(empId: Number, start: string, end: string): Observable<Order[]> {
        const apiMethod = `${API_URI}/api/Orders/SearchDate/${empId}/${start}/${end}`;
        return this.http.get<Order[]>(apiMethod).pipe(catchError(super.handleError))
    }

    public getOrders(empId: Number, supId: Number): Observable<Order[]> {
        const apiMethod = `${API_URI}/api/Orders/${empId}/${supId}`;             
        return this.http.get<Order[]>(apiMethod).pipe(catchError(super.handleError));
    }

    public getProcessPORs(searchParam: SearchParam): Observable<Order[]> {   
        const apiMethod = `${API_URI}/api/Orders/Details/${searchParam.empId}`;
        return this.http.post<Order[]>(apiMethod, searchParam, super.httpOptions())
            .pipe(catchError(super.handleError));
    }

    public processPOR(order: Order): Observable<boolean> {
        const apiMethod = `${API_URI}/api/Orders/Details/Process/${order.poNumber}`;
        return this.http.put<any>(apiMethod, order, super.httpOptions()).pipe(catchError(super.handleError));
    }

    public closePOR(order: Order): Observable<boolean> {
        const apiMethod = `${API_URI}/api/Orders/Details/Close/${order.poNumber}`;
        return this.http.put<any>(apiMethod, order, super.httpOptions()).pipe(catchError(super.handleError));
    }

    public noLongerNeeded(order: Order): Observable<boolean> {
        const apiMethod = `${API_URI}/api/Orders/Need/${order.poNumber}`;     
        return this.http.put<any>(apiMethod, order, super.httpOptions()).pipe(catchError(super.handleError));
    }
 
}