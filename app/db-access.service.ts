import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Project } from './project';
import { Projmember } from './projmember';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  GetUserById(pmId: string) {
    throw new Error('Method not implemented.');
  }

  constructor(private http:HttpClient) { 


  }
  AddNewUser(Input:UserInfo): Observable<UserInfo>{
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo",Input);
  }

  AddNewProject(Input:Project): Observable<Project>{
    return this.http.post<Project>("http://localhost:3004/Project",Input);
  }

  AddNewProjectMember(Input:Projmember): Observable<Projmember>{
    return this.http.post<Projmember>("http://localhost:3004/ProjectMember",Input);
  }

  GetAllUsers(): Observable<any>{
    return this.http.get("http://localhost:3004/UserInfo");
  }
  DeleteUser(id:string): Observable<any>{
    return this.http.delete(`http://localhost:3004/UserInfo/${id}`);
  }

  GetAllProjects(): Observable<any>{
    return this.http.get("http://localhost:3004/Project");
  }

  GetProjectById(userId: string): Observable<Project[]> {
    return this.http.get<Project[]>(`http://localhost:3004/Project?pm=${userId}`);
  }
  
  GetAllProjectMembers(): Observable<Projmember[]> {
    return this.http.get<Projmember[]>("http://localhost:3004/ProjectMember");
  }

  GetProjectMembersByProjectId(projectId: string): Observable<Projmember[]> {
    return this.http.get<Projmember[]>(`http://localhost:3004/ProjectMember?projId=${projectId}`);
  }
}
