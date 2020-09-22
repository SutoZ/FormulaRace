import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  header = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getTeams<PagedList>(parameters: HttpParams): Observable<PagedList> {
    return this.http.request<PagedList>('get', environment.BASE_URL + 'api/teams', { headers: this.header, params: parameters });
  }

  getTeamById<ITeamListViewModel>(id: number): Observable<ITeamListViewModel> {
    return this.http.request<ITeamListViewModel>('get', environment.BASE_URL + 'api/teams/' + id, { headers: this.header });
  }

  postTeam<ITeamListViewModel>(team: ITeamListViewModel): Observable<ITeamListViewModel> {
    return this.http.post<ITeamListViewModel>(environment.BASE_URL + 'api/teams/', team, { headers: this.header });
  };

  putTeam<ITeamListViewModel>(id: number, team: ITeamListViewModel): Observable<ITeamListViewModel> {
    return this.http.put<ITeamListViewModel>(environment.BASE_URL + 'api/teams/' + id, team, { headers: this.header });
  };

}
