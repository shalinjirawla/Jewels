import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs'
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class GeneralsetpService {

}
@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Commons/";


  public SaveCurrency(input: any): Observable<any> {
    if (input.CurrencyId == 0) {
      let url = this.BaseURL + `SaveCurrency`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    } else {
      let url = this.BaseURL + `UpdateCurrency?CurrencyId=${input.CurrencyId}`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    }
  }
  public GetCurrencyList(): Observable<any> {
    let url = this.BaseURL + `GetCurrencys`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    })
  }
  public GetCurrency(CurrencyId: any): Observable<any> {
    let url = this.BaseURL + `GetCurrency?CurrencyId=${CurrencyId}`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteCurrency(CurrencyId: any): Observable<any> {
    let url = this.BaseURL + `DeleteCurrency?CurrencyId=${CurrencyId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public CureencyStatusChange(CurrencyId,Status):Observable<any>{
    let url=this.BaseURL+`Currencychange?CurrencyId=${CurrencyId}&Statuschange=${Status}`;
    return this.HttpClient.get(url,httpOption).pipe((responce:any)=>{
      return this.Responce=responce;
    });
  }
}
@Injectable({
  providedIn: 'root'
})
export class CreditTermsService {

}