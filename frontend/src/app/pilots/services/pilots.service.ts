import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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

  getPilots(event: PageEvent, params: HttpParams): Observable<any> | null {   

    var params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString());

    var url = this.baseUrl + "api/pilots";
    return this.http.get<any>(url, { params });
  }
}

