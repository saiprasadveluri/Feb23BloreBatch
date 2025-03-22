import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './userinfo';

@Injectable({
  providedIn: 'root'
})
export class DbAccessServiceService {

  constructor(private http:HttpClient) { }

  //Add a User
  adduser(user:UserInfo):Observable<UserInfo>{
    console.log(user);
    return this.http.post<UserInfo>('http://localhost:3001/UserInfo', user);
  }
  //userlist
  getUsers(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>('http://localhost:3001/UserInfo');
  }
  getProjects(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:3001/Project'); // Fetch projects from JSON Server
  }
  
}
