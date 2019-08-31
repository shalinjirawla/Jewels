import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ProductBrandService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/";

  public SaveBrand(input:any):Observable<any>{
    if(input.BrandId==0){
      let url=this.BaseURL+'/Product/';
      return this.HttpClient.post(url,input,httpOption).pipe((responce:any)=>{
        return this.Responce=responce;
      });
    }else{
      let url=this.BaseURL+`/Product/?BrandId=${input.BrandId}`;
      return this.HttpClient.post(url,input,httpOption).pipe((responce:any)=>{
        return this.Responce=responce;
      });
    }
  }
}
