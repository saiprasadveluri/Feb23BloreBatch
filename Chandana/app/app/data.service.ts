import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'http://localhost:3004/data'; // Adjust the URL as needed

  constructor(private http: HttpClient) {}

  getData(): Observable<{ Project: any[]; UserInfo: any[]; }> {
    return this.http.get<{ Project: any[]; UserInfo: any[]; }>(this.apiUrl);
  }

  assignTask(task: any): Observable<any> {
    return this.http.post('http://localhost:3004/assign-task', task);
  }
  createTask(task: any): Observable<any> {
    return this.http.post('http://localhost:3004/Task', task);
  }
}
