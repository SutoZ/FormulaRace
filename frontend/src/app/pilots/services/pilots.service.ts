import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPilotViweModel } from '../pilot';

@Injectable({
  providedIn: 'root'
})
export class PilotsService {
  baseUrl: string = "https://localhost:44372";
  constructor(private http: HttpClient
    //@Optional() @Inject('BASE_URL') private baseUrl: string) { }
  ) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getPilots(): Observable<IPilotViweModel[]> | null {
    return this.http.get<IPilotViweModel[]>(this.baseUrl + "/api/pilots", this.httpOptions);
  }
}

