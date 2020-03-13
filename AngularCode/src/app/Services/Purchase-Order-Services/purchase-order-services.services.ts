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
export class PurchaseOrderService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/PurchaseOrder/";

  public AddEditPurchaseOrder(input: any): Observable<any> {
    let url = this.BaseURL + `SavePurchaseOrder`;
    var data = JSON.stringify(input);
    //input=JSON.stringify(data);
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetPurchaseOrderList(): Observable<any> {
    let url = this.BaseURL + `GetPurchaseOrderList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetPurchaseOrder(PurchaseOrderId: number): Observable<any> {
    let url = this.BaseURL + `GetPurchaseOrderDetails?PurchaseOrderId=` + PurchaseOrderId;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeletePurchaseOrder(PurchaseOrderId: number): Observable<any> {
    let url = this.BaseURL + `DeletePurchaseOrder?PurchaseOrderId=` + PurchaseOrderId;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetPurchaseOrderlistIdBySupplier(SupplierId: number) {
    let url = this.BaseURL + `GetPurchaseOrderDetailsIdBySupplier?SupplierId=` + SupplierId;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetProductIdByPurchaseOrder(PurchaseOrderId: number) {
    let url = this.BaseURL + `GetProductIdByPurchaseOrder?PurchaseOrderId=` + PurchaseOrderId;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
