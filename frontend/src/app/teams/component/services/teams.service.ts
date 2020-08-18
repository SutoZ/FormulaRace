import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../team';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
    baseUrl: string = "https://localhost:44372";
    constructor(private http: HttpClient
      //@Optional() @Inject('BASE_URL') private baseUrl: string) { }
    ) { }
  
    httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
  
    getPilots(): Observable<Team[]> | null {
      return this.http.get<Team[]>(this.baseUrl + "/api/teams", this.httpOptions);
    }
}
