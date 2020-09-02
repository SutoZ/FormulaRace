import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPilotsListViewModel } from '../models/pilot.models';
import { PagedList } from 'src/app/PagedList';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PilotsService {
  environment: string;
  constructor(private http: HttpClient) { }

  header = new HttpHeaders({ 'Content-Type': 'application/json' });

  getPilots<PagedList>(parameters: HttpParams): Observable<PagedList> | null {
    // var options: any = {
    //   headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    //   body: JSON.stringify(null)    //DTO to add
    // };
    return this.http.request<PagedList>("get", environment.BASE_URL + "api/pilots", { headers: this.header, params: parameters });
    //    return this.http.request<PagedList>("get", this.baseUrl + "api/pilots", { headers: this.header, params: parameters });
  }

  getPilotsById<IPilotsListViewModel>(id: number): Observable<IPilotsListViewModel> | null {
    return this.http.request<IPilotsListViewModel>("get", environment.BASE_URL + "api/pilots/" + id, { headers: this.header });
  }
  //any
  postPilot(pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> | null {
    return this.http.request<IPilotsListViewModel>('post', environment.BASE_URL + 'api/pilots/' + pilot, { headers: this.header });
  }
}

