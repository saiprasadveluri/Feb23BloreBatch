import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjInfo } from './proj-info';

 
@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  private apiUrl = 'http://localhost:3004';
 
  constructor(private http:HttpClient)
  {
   
   }
   GetAllUsers():Observable<any>
   {
     return this.http.get("http://localhost:3004/UserInfo");
   }
   AddNewUser(inpUsr:UserInfo):Observable<any>
   {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr);
   }
   AddNewProject(project:ProjInfo): Observable<ProjInfo> {
    return this.http.post<ProjInfo>('http://localhost:3004/Project', project);
  }
  getAllProjects():Observable<any>
   {
     return this.http.get("http://localhost:3004/Project");
   }
   getProjectMembers(projectId: number): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>("http://localhost:3004/ProjectMember");
    
}
GetProjectsForPm(pmId: string): Observable<ProjInfo[]> {
  return this.http.get<ProjInfo[]>(`${this.apiUrl}/Project?pm=${pmId}`);
}
 
}