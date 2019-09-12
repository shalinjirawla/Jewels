import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { TenantsModel } from '../../Models/Tenants/TenantsModel';
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class TenantsServicesService {
  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Tenants/";
  public SaveTenants(input: any): Observable<any> {
    if (input.TenantId == 0) {
      let url = this.BaseURL + `SaveTenant`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      })
    } else {

    }
  }
  public GetRegisterData(TenantId: any,UserId:any): Observable<any> {
    if (TenantId != 0) {
      let url = this.BaseURL+`GetRegisterData?TenantId=${TenantId}&UserId=${UserId}`;
      return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    }
  }
}
