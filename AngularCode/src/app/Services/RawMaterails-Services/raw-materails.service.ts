import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
import { parse } from 'path';

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
    return this.HttpClient.get(url, httpOption)
    .pipe((responce: any) => {
      return this.Responce = responce;
    })
    
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
    input.outer_Weight_metric_Units=parseFloat(input.outer_Weight_metric_Units);
    input.inner_Weight_metric_Units=parseFloat(input.inner_Weight_metric_Units);
    input.uomId=parseFloat(input.uomId);
    input.oD_metric_Units=parseFloat(input.oD_metric_Units);
    input.iD_metric_Units=parseFloat(input.iD_metric_Units);
    input.productCategorieId=parseFloat(input.productCategorieId);
    input.brandId=parseFloat(input.brandId);
    input.supplierId=parseFloat(input.supplierId);
    input.taxCodeId=parseFloat(input.taxCodeId);
    input.warehouseId=parseFloat(input.warehouseId);
    
    var data = JSON.stringify(input);
    let url = this.BaseURL + `AddRawMaterails`;
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public UpdateRawMaterails(Id,input): Observable<any> {
    input.outer_Weight_metric_Units=parseFloat(input.outer_Weight_metric_Units);
    input.inner_Weight_metric_Units=parseFloat(input.inner_Weight_metric_Units);
    input.uomId=parseFloat(input.uomId);
    input.oD_metric_Units=parseFloat(input.oD_metric_Units);
    input.iD_metric_Units=parseFloat(input.iD_metric_Units);
    input.productCategorieId=parseFloat(input.productCategorieId);
    input.brandId=parseFloat(input.brandId);
    input.supplierId=parseFloat(input.supplierId);
    input.taxCodeId=parseFloat(input.taxCodeId);
    input.warehouseId=parseFloat(input.warehouseId);
    
    var data = JSON.stringify(input);
    let url = this.BaseURL + `UpdateRawMaterails?RMId=`+Id;
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetRawMaterailsList(): Observable<any> {
    let url = this.BaseURL + `GetRawMaterailsList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetRawMaterails(Id): Observable<any> {
    let url = this.BaseURL + `GetRawMaterails?RMId=`+Id;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteRawMaterails(Id): Observable<any> {
    let url = this.BaseURL + `DeleteRawMaterails?RMId=`+Id;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

}


