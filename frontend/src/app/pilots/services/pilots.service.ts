import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pilot } from '../pilot';

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

  getPilots(): Observable<Pilot[]> | null {
    return this.http.get<Pilot[]>(this.baseUrl + "/api/pilots", this.httpOptions);
  }
}

