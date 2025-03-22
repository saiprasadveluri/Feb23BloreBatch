import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Project } from './project';
import { ProjectMember } from './project-member';
import { Task } from './task';
import { Comment } from './comment';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  private apiUrl = 'http://localhost:3004';

  constructor(private http: HttpClient) { }

  GetAllUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/UserInfo`);
  }

  AddNewUser(obj: UserInfo): Observable<any> {
    return this.http.post(`${this.apiUrl}/UserInfo`, obj);
  }

  GetAllProjects(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Project`);
  }

  AddNewProject(obj: Project): Observable<any> {
    return this.http.post(`${this.apiUrl}/Project`, obj);
  }

  DeleteUser(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/UserInfo/${id}`);
  }

  DeleteProject(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Project/${id}`);
  }
  DeleteProjectMem(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/ProjectMember/${id}`);
  }

  GetAllProjMem(): Observable<any> {
    return this.http.get(`${this.apiUrl}/ProjectMember`);
  }
  
  AddNewProjectMem(obj: ProjectMember): Observable<any> {
    return this.http.post(`${this.apiUrl}/ProjectMember`, obj);
  }

  GetAllTasks(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Task`);
  }

  AddTask(obj: Task): Observable<any> {
    return this.http.post(`${this.apiUrl}/Task`, obj);
  }

  GetAllComment(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Comment`);
  }

  AddComment(obj: Comment): Observable<any> {
    return this.http.post(`${this.apiUrl}/Comment`, obj);
  }
  
  UpdateTaskAssignedTo(id: string, assignedTo: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/Task/${id}`, { assignedto: assignedTo });
  }
  UpdateTaskStatus(id:string,Status:string):Observable<any>{
    return this.http.patch(`${this.apiUrl}/Task/${id}`, { status: Status });
  }
}