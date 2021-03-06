import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JsonPipe } from '@angular/common';
let AccessToken;
AccessToken=localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization','Bearer '+AccessToken)
};

@Injectable({
  providedIn: 'root'
})
export class CustomerTypeService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Customer";

  public GetCustomertypelist() : Observable<any> {
    let url = this.BaseURL + `/GetCustomerType`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public AddCustomerType(input:any):Observable<any>{
    let url = this.BaseURL + `/AddCustomerType`;
    var data = JSON.stringify(input);
    return this.HttpClient.post(url,data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetCustomerTypeById(Id:any):Observable<any>{
    let url = this.BaseURL + `/GetCustomerTypeById?Id=`+Id;
    return this.HttpClient.get(url,httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public DeleteCustomerTypeById(Id:any):Observable<any>{
    let url = this.BaseURL + `/DeleteCustomerType?Id=`+Id;
    return this.HttpClient.delete(url,httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public CustomerTypeIsExist(Name:any):Observable<any>{
    let url = this.BaseURL + `/CustomerIsExist?Name=`+Name;
    return this.HttpClient.get(url,httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

}
