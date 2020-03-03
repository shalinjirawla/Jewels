import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JsonPipe } from '@angular/common';
let AccessToken;
AccessToken = localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization', 'Bearer ' + AccessToken)
};


@Injectable({
  providedIn: 'root'
})
export class SalesOrderService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/SalesOrder/";

  public AddEditSalesOrder(input: any): Observable<any> {
    let url = this.BaseURL + `SaveSalesOrder`;
    var data = JSON.stringify(input);
    //input=JSON.stringify(data);
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetSalesOrderList(): Observable<any> {
    let url = this.BaseURL + `GetSalesOrderList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetSalesOrder(SalesOrderId: number): Observable<any> {
    let url = this.BaseURL + `GetSalesOrderDetails?SalesOrderId=`+SalesOrderId;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteSalesOrder(SalesOrderId: number): Observable<any> {
    let url = this.BaseURL + `DeleteSalesOrder?SalesOrderId=`+SalesOrderId;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
