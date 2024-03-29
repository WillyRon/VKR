import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { Application } from '../models/application.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = '/api/user';

  constructor(private http: HttpClient) { }

  addUser(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-user`, user)
  }

  getUserInfo(email: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/user/${email}`);
  }

  getApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/applications`);
  }

  addApplication(application: Application): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-application`, application);
  }

  changeStatus(application: Application): Observable<any> {
    return this.http.post(`${this.apiUrl}/change-application-status`, application);
  }
}