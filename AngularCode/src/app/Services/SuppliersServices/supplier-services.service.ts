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
export class SupplierServicesService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Supplier/";

  public AddUpdate(input: any): Observable<any> {
    let url = this.BaseURL + `AddUpdateSuppliers`;
    debugger
    input.SuppliersDetail.addressList=input.AddressList;
    input.SuppliersDetail.contactList=input.ContactList;
    var data = JSON.stringify(input.SuppliersDetail);

    //input=JSON.stringify(data);
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetSuppliersList(): Observable<any> {
    let url = this.BaseURL + `GetsupplierList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetSuppliers(SuppilerId: any): Observable<any> {
    let url = this.BaseURL + `GetSupplier?SupplierId=${SuppilerId}`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteSuppliers(SuppliersId: any): Observable<any> {
    let url = this.BaseURL + `DeleteSuppliers?SuppliersId=${SuppliersId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
