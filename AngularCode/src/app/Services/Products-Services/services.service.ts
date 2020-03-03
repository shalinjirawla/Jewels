import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs'
let AccessToken;
AccessToken=localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization','Bearer '+AccessToken)
};
@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Products/";

  public SaveService(input: any): Observable<any> {
      let url = this.BaseURL + `SaveProductService`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    
  }
  public GetServiceList(): Observable<any> {
    let url = this.BaseURL + `GetServiceList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetService(ServiceId:any):Observable<any>{
    let url=this.BaseURL+`GetService?ServiceId=${ServiceId}`;
    return this.HttpClient.get(url,httpOption).pipe((responce:any)=>{
      return this.Responce=responce;  
    });
  }
  public DeleteService(ServiceId: any): Observable<any> {
    let url = this.BaseURL + `DeleteService?ServiceId=${ServiceId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce
    });
  }
}
