import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { AddNewProject } from './add-new-project';
import { DisplayProjectList } from './display-project-list';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  deleteProject(projectId: string) {
    throw new Error('Method not implemented.');
  }
  removeMemberFromProject(payload: { projId: string; memId: string; }) {
    throw new Error('Method not implemented.');
  }
  constructor(private http: HttpClient) {}

  GetAllUsers(): Observable<any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }

  AddNewUser(inpUsr: UserInfo): Observable<any> {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo", inpUsr);
  }

  AddNewProject(project: AddNewProject): Observable<AddNewProject> {
    return this.http.post<AddNewProject>('http://localhost:3004/Project', project);
  }

  DisplayAllProjects(): Observable<any> {
    return this.http.get('http://localhost:3004/Project');
  }

  addMemberToProject(payload: { projId: string; memId: string }): Observable<any> {
    return this.http.post('http://localhost:3004/ProjectMember', payload);
  }

  getMembersForProject(projectId: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:3004/ProjectMember?projId=${projectId}`);
  }

  DisplayProject(): Observable<DisplayProjectList[]> {
    return this.http.get<DisplayProjectList[]>('http://localhost:3004/Project');
  }

  getProjectsForMember(memberId: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:3004/ProjectMember?memId=${memberId}`);
  }

  getProjectsForPM(pmId: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:3004/Project?pmId=${pmId}`);
  }

  assignProjectToPm(payload: { projId: string; pmId: string }): Observable<any> {
    return this.http.put(`http://localhost:3004/Project/${payload.projId}`, { pmId: payload.pmId });
  }

  getTasksByPmId(pmId: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:3004/Tasks?pmId=${pmId}`);
  }

 
}