import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  baseUrl: string = "https://localhost:44372/";
  constructor(private http: HttpClient
    //@Optional() @Inject('BASE_URL') private baseUrl: string) { }
  ) { }

  header = new HttpHeaders({ 'Content-Type': 'application/json' });

  getTeams(parameters: HttpParams): Observable<any> | null {
    return this.http.get<any>(this.baseUrl + "api/teams", { headers: this.header, params: parameters });
  }
}
