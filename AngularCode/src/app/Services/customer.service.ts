import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import {Observable} from 'rxjs'

const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private HttpClient:HttpClient) { }
  private BaseURL: string = "https://localhost:44315";

  public AddCustomer(input: any):Observable<any>{
    debugger
    let Result;
    if(input.CustomerId==0){
    // const url = `${this.BaseURL}/api/Customer/AddCustomer`;
    //   return this.HttpClient.post(url ,input ,httpOption).pipe((responce: any) => {
    //     return Result = responce;
    //   })
    // }
    let url_ = this.BaseURL + `/api/Customer/AddCustomer`;
    url_ = url_.replace(/[?&]$/, "");

    var content_ = JSON.stringify(input);

    let options_: any = {
        observe: "response",
        body:content_,
        // responseType: "blob",
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        })
    };

    return this.HttpClient.request("post", url_, options_).pipe((response_: any) => {
        return Result = response_;
    });
  }
    // else{
    //   const url = `${this.BaseURL}/UpdateEmpolyee`;
    //   return this.HttpClient.post(url, input ,httpOption).pipe((responce: any) => {
    //     return Result = responce;
    //   })
    // }
  }

  public GetCurrency():Observable<any>{
    let Result;
    let url_ = this.BaseURL + `/api/Customer/GetCurrency`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
        observe: "response",
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        })
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
        return Result = response_;
    });

  }

  public GetCustomerType():Observable<any>{
    let Result;
    let url_ = this.BaseURL + `/api/Customer/GetCustomerType`;
    url_ = url_.replace(/[?&]$/, "");

    let options_: any = {
        observe: "response",
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        })
    };

    return this.HttpClient.request("get", url_, options_).pipe((response_: any) => {
        return Result = response_;
    });

  }
}
