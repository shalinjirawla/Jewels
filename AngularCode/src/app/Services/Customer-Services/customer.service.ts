import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
let AccessToken;
AccessToken = localStorage.getItem('AccessToken');

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315";

  public AddCustomer(input: any): Observable<any> {
    debugger
    let Result;
    let url_ = this.BaseURL + `/api/Customer/AddCustomer`;
    url_ = url_.replace(/[?&]$/, "");
    input.CustomerDetail.addressList = input.AddressList;
    input.CustomerDetail.contactList = input.ContactList;
    var j = input.CustomerDetail;
    var content_ = JSON.stringify(j);

    let options_: any = {
      observe: "response",
      body: content_,
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };
    return this.HttpClient.request("post", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });

  }

  public GetCurrency(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Commons/GetActiveCurrencys`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });

  }

  public GetCustomerType(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Customer/GetCustomerType`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });

  }

  public GetCountry(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Commons/GetCountryList`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });

  }

  public GetDiscountType(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Commons/GetDiscountTypeLists`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });
  }

  public GetCustomerList(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Customer/GetCustomerList`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });

  }

  public GetCustomerById(i: any): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Customer/GetCustomerById?Id=` + i;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });
  }

  public DeleteCustomer(i: any): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Customer/DeleteCustomer?Id=` + i;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("delete", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });
  }

  public GetCreditTermsList(): Observable<any> {
    let Result;
    let url_ = this.BaseURL + `/api/Commons/GetCreditTermsList`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
      observe: "response",
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Accept": "application/json"
      }).set('Authorization', 'Bearer ' + AccessToken)
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
      return Result = response_;
    });
  }

}
