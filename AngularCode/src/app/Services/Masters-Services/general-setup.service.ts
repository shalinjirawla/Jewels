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
    debugger
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
  public CureencyStatusChange(CurrencyId, Status): Observable<any> {
    let url = this.BaseURL + `Currencychange?CurrencyId=${CurrencyId}&Statuschange=${Status}`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
@Injectable({
  providedIn: 'root'
})
export class CreditTermsService {
  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Commons/";


  public SaveCreditTerms(input: any): Observable<any> {
    if (input.CreditTermId == 0) {
      let url = this.BaseURL + `AddCreditTerm`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    } else {
      let url = this.BaseURL + `UpdateCreditTerm?CreditTermId=${input.CreditTermId}`;
      return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
        return this.Responce = responce;
      });
    }
  }
  public GetCreditTermsist(): Observable<any> {
    let url = this.BaseURL + `GetCreditTermsList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    })
  }
  public GetCreditTerms(CreditTermsId: any): Observable<any> {
    let url = this.BaseURL + `GetCreditTermsById?CreditTermId=${CreditTermsId}`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteCreditTerms(CreditTermsId: any): Observable<any> {
    let url = this.BaseURL + `DeleteCreditTerm?CreditTermId=${CreditTermsId}`;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public AddCountry(input): Observable<any> {
    let url = this.BaseURL + `AddUpdateCountry`;
    return this.HttpClient.post(url, JSON.stringify(input), httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetCountryList(): Observable<any> {
    let url = this.BaseURL + `GetCountryList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetCountry(i): Observable<any> {
    let url = this.BaseURL + `GetCountry?Id=` + i;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public DeleteCountry(i): Observable<any> {
    let url = this.BaseURL + `DeleteCountry?Id=` + i;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public GetLocationList(): Observable<any> {
    let url = this.BaseURL + `GetWarehouseList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }

  public AddLocation(input: any): Observable<any> {
    debugger
    let url = this.BaseURL + `SaveWarehouseList`;
    return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    })
  }
}