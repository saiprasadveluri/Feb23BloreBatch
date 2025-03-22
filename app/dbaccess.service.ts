import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
@Injectable({
  providedIn: 'root'
})
export class DBAccessService {
 
  constructor(private http: HttpClient)
  {
 
   }
   GetAllUsers():Observable<any>
    {
      return this.http.get("http://localhost:3004/UserInfo");
    }
    AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
    {
      return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr);
    }
    GetAllProjects():Observable<any>
    {
      return this.http.get("http://localhost:3004/Project");
    }
    AddNewProject(inpProj:ProjectInfo):Observable<ProjectInfo>
  {
    return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpProj);
  }
  GetAllProjectsById(name:string):Observable<ProjectInfo>
  {
    return this.http.get<ProjectInfo>("http://localhost:3004/Project?="+name);
  }
}

 