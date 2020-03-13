import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JsonPipe } from '@angular/common';
let AccessToken;
AccessToken = localStorage.getItem('AccessToken');
const httpOption = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' }).set('Authorization', 'Bearer ' + AccessToken)
};


@Injectable({
  providedIn: 'root'
})
export class ReceiveNotesServicesService {

  Responce: any;
  constructor(private HttpClient: HttpClient) { }
  private BaseURL: string = "https://localhost:44315/api/PurchaseOrder/";
  public AddReceiveNotes(input: any): Observable<any> {
    let url = this.BaseURL + `SaveReceiveNotes`;
    var data = JSON.stringify(input);
    return this.HttpClient.post(url, data, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    })
  }
  public GetReceiveNotesList(): Observable<any> {
    let url = this.BaseURL + `GetReceiveNotesList`;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public GetReceiveNotes(ReceiveNoteId: number): Observable<any> {
    let url = this.BaseURL + `GetReceiveNotesDetails?ReceiveNotesId=` + ReceiveNoteId;
    return this.HttpClient.get(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
  public DeleteReceiveNotes(ReceiveNoteId: number): Observable<any> {
    let url = this.BaseURL + `DeleteReceiveNotes?ReceiveNotesId=` + ReceiveNoteId;
    return this.HttpClient.delete(url, httpOption).pipe((responce: any) => {
      return this.Responce = responce;
    });
  }
}
