import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { JsonPipe } from '@angular/common';
import { RegisterModel } from '../../Models/ApplicationUser/RegisterModel';
import { LoginModel } from '../../Models/ApplicationUser/LoginModel';
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ApplicationUserService {

  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/Account/";
  public Responce: any;
  public SaveRegister(input: RegisterModel): Observable<any> {
    let url = this.BaseURL + `Register`;
    return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    })
  }
  public LogInProcess(input: LoginModel): Observable<any> {
    let url = this.BaseURL + `LoginProcess`;
    return this.HttpClient.post(url, input, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
