import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { IPagedList } from 'src/app/PagedList';
import { ITeamListViewModel } from '../models/team.models';

@Injectable({
  providedIn: 'root',
})
export class TeamsService {
  header = new HttpHeaders({ 'Content-Type': 'application/json' });

  private readonly baseUrl = '/api/teams';

  filterQuery$ = new BehaviorSubject<string>('');

  constructor(private readonly http: HttpClient) {}

  getTeams(parameters: HttpParams): Observable<IPagedList<ITeamListViewModel>> {
    return this.http.get<IPagedList<ITeamListViewModel>>(this.baseUrl, {
      headers: this.header,
      params: parameters,
    });
  }

  getTeamById<ITeamListViewModel>(id: number): Observable<ITeamListViewModel> {
    return this.http.get<ITeamListViewModel>(`${this.baseUrl}/${id}`, { headers: this.header });
  }

  postTeam<ITeamListViewModel>(team: ITeamListViewModel): Observable<ITeamListViewModel> {
    return this.http.post<ITeamListViewModel>(this.baseUrl, team, { headers: this.header });
  }

  putTeam<ITeamListViewModel>(
    id: number,
    team: ITeamListViewModel
  ): Observable<ITeamListViewModel> {
    return this.http.put<ITeamListViewModel>(`${this.baseUrl}/${id}`, team, {
      headers: this.header,
    });
  }
}
