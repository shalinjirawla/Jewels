import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/";

  public SaveProductCategories(input: any): Observable<any> {
    if (input.CategoriesId == 0) {
      let url = this.BaseURL + `Products/SaveProductCategories`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce
      })
    } else {
      let url = this.BaseURL + `Products/UpdateProductCategorie?CategoriesId=${input.CategoriesId}`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce
      })
    }
  }
  public GetProductCategoriesList():Observable<any> {
    let url = this.BaseURL + `Products/GetProductCategoriesList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetProductCategories(CategoriesId:any):Observable<any> {
    let url = this.BaseURL + `Products/GetProductCategorie?CategoriesId=${CategoriesId}`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
 public DeleteCategorie(CategoriesId:any):Observable<any> {
  let url = this.BaseURL + `Products/DeleteProductCategorie?CategoriesId=${CategoriesId}`;
  return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
    return this.Responce = responce;
  });
}
}
