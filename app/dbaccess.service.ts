import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';

@Injectable({
  providedIn: 'root'
})
export class DBAccessService {
  private baseUrl = "http://localhost:3004"; // Base URL for json-server

  constructor(private http: HttpClient) {}

  // Fetch all users
  GetAllUsers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/UserInfo`);
  }

  // Add a new user
  AddNewUser(inpUsr: UserInfo): Observable<UserInfo> {
    return this.http.post<UserInfo>(`${this.baseUrl}/UserInfo`, inpUsr);
  }

  // Delete a user by ID
  deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/UserInfo/${userId}`);
  }

  // Fetch all projects
  GetAllProjects(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Project`);
  }

  // Add a new project
  AddNewProject(inpProj: ProjectInfo): Observable<ProjectInfo> {
    return this.http.post<ProjectInfo>(`${this.baseUrl}/Project`, inpProj);
  }

  // Delete a project by ID
  deleteProject(projectId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Project/${projectId}`);
  }

  // Fetch members of a specific project
  GetProjectMembers(projectId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ProjectMember?projId=${projectId}`);
  }

  // Assign a member to a project
  AssignMemberToProject(member: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/ProjectMember`, member);
  }
  RemoveProjectMember(memberId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ProjectMember/${memberId}`);
  }
  // Create a new task
  CreateTask(task: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Task`, task);
  }
  // Fetch tasks of a specific project
  GetTasksByProjectId(projectId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Task?projid=${projectId}`);
  }
}
