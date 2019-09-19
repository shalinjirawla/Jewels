import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'

let AccessToken;
AccessToken = localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization', 'Bearer ' + AccessToken)
};


@Injectable({
  providedIn: 'root'
})
export class RawMaterailsService {
  Responce: any;
  constructor(private HttpClient: HttpClient) { }

  private BaseURLCommanConctroll: string = "https://localhost:44315/api/Commons/";
  private BaseURL: string = "https://localhost:44315/api/RawMaterails/";

  public GetLocationList(): Observable<any> {
    let url = this.BaseURLCommanConctroll + `GetActiveWarehouseList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetUOMList(): Observable<any> {
    let url = this.BaseURLCommanConctroll + `GetUOMList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetKgMetricUnitList(): Observable<any> {
    let url = this.BaseURLCommanConctroll + `GetKgMetricUnitList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetFtMetricUnitList(): Observable<any> {
    let url = this.BaseURLCommanConctroll + `GetFtMetricUnitList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public SaveRawMaterails(input): Observable<any> {
    debugger
    var data = JSON.stringify(input);
    let url = this.BaseURL + `AddRawMaterails`;
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

}


