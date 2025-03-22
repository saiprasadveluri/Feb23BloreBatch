import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class DBAccessService {
 
  constructor(private http: HttpClient) {}

  GetAllUsers(): Observable<any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }

  GetAllProjects(): Observable<any> {
    return this.http.get("http://localhost:3004/Project");
  }

  AddNewUser(inpUsr: UserInfo): Observable<any> {
    return this.http.post("http://localhost:3004/UserInfo", inpUsr);
  }

  AddNewProject(inpPrj: ProjectInfo): Observable<any> {
    return this.http.post("http://localhost:3004/Project", inpPrj);
  }

  GetProjectMembers(projectId: ProjectInfo): Observable<any> {
    return this.http.get("http://localhost:3004/projectManger");
  }

  RemoveMemberFromProject(projectId: string, memberId: string): Observable<any> {
    return this.http.delete(`http://localhost:3004/projectManger/${projectId}/members/${memberId}`);
  }

  AssignMemberToProject(projectId: string, member: { id: string; name: string; role: string }): Observable<any> {
    return this.http.post(`http://localhost:3004/projectManger/${projectId}/members`, member);
  }

  GetProjectTasks(projectId: string): Observable<any> {
    return this.http.get(`http://localhost:3004/Task`);
  }

  
  AddNewTask(task: Task): Observable<any>{
    throw new Error('http://localhost:3004/Task');
  } 
 
  RemoveTaskFromProject(projectId: string, taskId: string): Observable<any> {
    return this.http.delete(`http://localhost:3004/projects/${projectId}/tasks/${taskId}`);
  }
}