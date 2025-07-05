import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPilotsListViewModel } from '../models/pilot.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class PilotsService {
  constructor(private readonly http: HttpClient) { }

  private readonly baseUrl = `${environment.BASE_URL}/api/pilots`;

  getPilots<PagedList>(parameters: HttpParams): Observable<PagedList> {
    return this.http.get<PagedList>(this.baseUrl, { params: parameters });
  };

  getPilotById(id: number): Observable<IPilotsListViewModel> {
    return this.http.get<IPilotsListViewModel>(`${this.baseUrl}/${id}`);
  };

  postPilot(pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> {
    return this.http.post<IPilotsListViewModel>(this.baseUrl, pilot);
  };

  putPilot(id: number, pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> {
    return this.http.put<IPilotsListViewModel>(`${this.baseUrl}/${id}`, pilot);
  };

  checkNameExists(pilot: IPilotsListViewModel): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/IsNameExists`, pilot);
  }
}