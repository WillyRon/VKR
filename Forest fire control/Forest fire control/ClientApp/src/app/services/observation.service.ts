import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Region } from '../models/regoin.model';
import { ObservationSite } from '../models/observation.model';
import { Incedent } from '../models/incident.model';
import { VideoArchive } from '../models/video-archiv.model';
import { IncedentStatusEnum } from '../models/emums/incident-status.emum';

@Injectable({
  providedIn: 'root'
})
export class ObservationService {
  private apiUrl = '/api/observation';

  constructor(private http: HttpClient) { }

  getRegions(): Observable<Region[]> {
    return this.http.get<Region[]>(`${this.apiUrl}/regions`);
  }

  getIncedents(): Observable<Incedent[]> {
    return this.http.get<Incedent[]>(`${this.apiUrl}/incedents`);
  }

  addObservation(observation: ObservationSite): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-observation`, observation);
  }

  updateObservation(observation: ObservationSite): Observable<any> {
    return this.http.post(`${this.apiUrl}/update-observation`, observation);
  }

  deleteObservation(observation: ObservationSite): Observable<any> {
    return this.http.post(`${this.apiUrl}/delete-observation`, observation);
  }

  getObservations(): Observable<ObservationSite[]> {
    return this.http.get<ObservationSite[]>(`${this.apiUrl}/get-all-observation`);
  }

  getIncidentObservations(longitude: number, latitude: number): Observable<Incedent[]> {
    return this.http.get<Incedent[]>(`${this.apiUrl}/get-incident-observation?longitude=${longitude}&latitude=${latitude}`);
  }

  getVideoArchiveObservations(longitude: number, latitude: number): Observable<VideoArchive[]> {
    return this.http.get<VideoArchive[]>(`${this.apiUrl}/get-video-archive-observation?longitude=${longitude}&latitude=${latitude}`);
  }

  getStatusString(status: IncedentStatusEnum): string {
    switch (status) {
      case IncedentStatusEnum.New:
        return 'Новый';
      case IncedentStatusEnum.Done:
        return 'Закрыт';
      default:
        return 'Неизвестный статус';
    }
  }
}