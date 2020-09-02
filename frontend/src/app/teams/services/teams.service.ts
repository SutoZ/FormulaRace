import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITeamListViewModel } from '../models/team.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  constructor(private http: HttpClient) { }

  header = new HttpHeaders({ 'Content-Type': 'application/json' });
  getTeams<PagedList>(parameters: HttpParams): Observable<PagedList> | null {
  console.log(environment.BASE_URL);
    return this.http.request<PagedList>('get', environment.BASE_URL + 'api/teams', { headers: this.header, params: parameters });
  }
}
