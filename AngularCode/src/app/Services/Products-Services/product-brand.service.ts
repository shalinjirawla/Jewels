import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
let AccessToken;
AccessToken=localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization','Bearer '+AccessToken)
};
@Injectable({
  providedIn: 'root'
})
export class ProductBrandService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Products/";

  public SaveBrand(input: any): Observable<any> {
    if (input.BrandId == 0) {
      let url = this.BaseURL + `SaveProductBrand`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    } else {
      let url = this.BaseURL + `UpdateProductBrand?BrandId=${input.BrandId}`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    }
  }
  public GetProductBrandList(): Observable<any> {
    let url = this.BaseURL + `GetProductBrandList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetProductBrand(BrandId:any):Observable<any>{
    let url=this.BaseURL+`GetProductBrand?BrandId=${BrandId}`;
    return this.HttpClient.get(url,httpOption).pipe((responce:any)=>{
      return this.Responce=responce;
    });
  }
  public DeleteProductBrand(BrandId: any): Observable<any> {
    let url = this.BaseURL + `DeleteProductBrand?BrandId=${BrandId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce
    });
  }
}
