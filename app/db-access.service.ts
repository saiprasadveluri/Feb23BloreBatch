import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, switchMap } from 'rxjs';
import { Userinfo } from './userinfo';
import { Project } from './project';
import { ProjectMembers } from './project-members';
import { Task } from './task';
import { Comments } from './comments';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  private apiUrl = 'http://localhost:3004'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  AddNewUser(user: Userinfo): Observable<Userinfo> {
    return this.http.post<Userinfo>(`${this.apiUrl}/UserInfo`, user);
  }
  AddNewProject(project: Project): Observable<Project> {
    return this.http.post<Project>(`${this.apiUrl}/Project`, project);
  }
  DeleteUser(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/UserInfo/${userId}`);
  }

  AssignMemberToProjectMembers(projectMember: ProjectMembers): Observable<ProjectMembers> {
    return this.http.post<ProjectMembers>(`${this.apiUrl}/ProjectMembers`, projectMember);
  }

  GetAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.apiUrl}/Project`);
  }

  GetAllUsers(): Observable<Userinfo[]> {
    return this.http.get<Userinfo[]>(`${this.apiUrl}/UserInfo`);
  }

  GetProjectMembers(projectId: string): Observable<ProjectMembers[]> {
    return this.http.get<ProjectMembers[]>(`${this.apiUrl}/ProjectMembers?projectId=${projectId}`);
  }

  GetTasksByProjectId(projectId: string): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}/Task?projectId=${projectId}`);
  }

  AddNewTask(task: Task): Observable<Task> {
    return this.http.post<Task>(`${this.apiUrl}/Task`, task);
  }
  getallprojectmembers(): Observable<ProjectMembers[]> {
    return this.http.get<ProjectMembers[]>(`${this.apiUrl}/ProjectMembers`);
  }
  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}/Task`);
  }

  addComment(comment: Comments): Observable<Comments> {
    return this.http.post<Comments>(`${this.apiUrl}/Comment`, comment);
  }
  updateTaskStatus(taskId: string, status: string, assignedTo: string): Observable<Task> {
    return this.http.get<Task[]>(`${this.apiUrl}/Task`).pipe(
      map(tasks => {
        const task = tasks.find(t => t.id === taskId);
        if (task) {
          task.status = status;
        }
        return task;
      }),
      switchMap(updatedTask => this.http.put<Task>(`${this.apiUrl}/Task/${taskId}`, updatedTask))
    );
  }


}