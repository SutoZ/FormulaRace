import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPagedList } from 'src/app/PagedList';
import { IPilotsListViewModel } from 'src/app/race-administration/models/pilot.models';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class PilotsService {
  private readonly baseUrl = `${environment.BASE_URL}/api/pilots`;

  constructor(private readonly http: HttpClient) {}

  getPilots(params: HttpParams): Observable<IPagedList<IPilotsListViewModel>> {
    return this.http.get<IPagedList<IPilotsListViewModel>>(this.baseUrl, { params });
  }

  getPilotById(id: number): Observable<IPilotsListViewModel> {
    return this.http.get<IPilotsListViewModel>(`${this.baseUrl}/${id}`);
  }

  createPilot(pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> {
    return this.http.post<IPilotsListViewModel>(this.baseUrl, pilot);
  }

  updatePilot(id: number, pilot: IPilotsListViewModel): Observable<IPilotsListViewModel> {
    return this.http.put<IPilotsListViewModel>(`${this.baseUrl}/${id}`, pilot);
  }

  deletePilot(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
