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
export class ProductService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Products/";

  public SaveProduct(input: any): Observable<any> {
      let url = this.BaseURL + `SaveProduct`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    
  }
  public GetProductList(): Observable<any> {
    let url = this.BaseURL + `GetProductList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetProduct(ProductId:any):Observable<any>{
    let url=this.BaseURL+`GetProduct?ProductId=${ProductId}`;
    return this.HttpClient.get(url,httpOption).pipe((responce:any)=>{
      return this.Responce=responce;  
    });
  }
  public DeleteProduct(ProductId: any): Observable<any> {
    let url = this.BaseURL + `DeleteProduct?ProductId=${ProductId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce
    });
  }
}
