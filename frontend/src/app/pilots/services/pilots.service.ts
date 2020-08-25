import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PageEvent } from '@angular/material/paginator';

@Injectable({
  providedIn: 'root'
})
export class PilotsService {
  baseUrl: string = "https://localhost:44372/";
  constructor(private http: HttpClient
    //@Optional() @Inject('BASE_URL') private baseUrl: string) { }
  ) { }


  getPilots(event: PageEvent, parameters: HttpParams): Observable<any> | null {
    var header = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any>(this.baseUrl + "api/pilots", { headers: header, params: parameters });
  }
}

