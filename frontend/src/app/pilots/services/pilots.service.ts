import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPilotsListViewModel } from '../models/pilot.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PilotsService {
  environment: string;
  constructor(private http: HttpClient) { }

  header = new HttpHeaders({ 'Content-Type': 'application/json' });

  getPilots<PagedList>(parameters: HttpParams): Observable<PagedList> | null {
    return this.http.request<PagedList>('get', environment.BASE_URL + 'api/pilots', { headers: this.header, params: parameters });
  };

  getPilotsById(id: number): Observable<IPilotsListViewModel> | null {
    return this.http.request<IPilotsListViewModel>('get', environment.BASE_URL + 'api/pilots/' + id, { headers: this.header });
  };

  postPilot<IPilotsListViewModel>(pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> | null {
    return this.http.post<IPilotsListViewModel>(environment.BASE_URL + 'api/pilots/', pilot, { headers: this.header });
  };

  putPilot<IPilotsListViewModel>(id:number, pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> | null {
    return this.http.put<IPilotsListViewModel>(environment.BASE_URL + 'api/pilots/' + id, pilot, { headers: this.header });
  };
}

