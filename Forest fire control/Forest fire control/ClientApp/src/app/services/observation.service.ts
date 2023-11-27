import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Region } from '../models/regoin.model'
import { ObservationSite } from '../models/observation.model';

@Injectable({
  providedIn: 'root'
})
export class ObservationService {
  private apiUrl = '/api/observation';

  constructor(private http: HttpClient) { }

  getRegions(): Observable<Region[]> {
    return this.http.get<Region[]>(`${this.apiUrl}/regions`);
  }

  addObservation(observation: ObservationSite): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-observation`, observation)
  }

  getObservations(): Observable<ObservationSite[]> {
    return this.http.get<ObservationSite[]>(`${this.apiUrl}/get-all-observation`);
  }
}