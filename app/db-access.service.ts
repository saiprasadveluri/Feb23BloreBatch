import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { ProjectManager } from './projectmanager';
import { Task } from './task';
import { Comment } from './comment';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  private baseUrl = 'http://localhost:3004';

  constructor(private http: HttpClient) {}

  GetAllUsers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/UserInfo`);
  }

  AddNewUser(inpUsr: UserInfo): Observable<any> {
    return this.http.post(`${this.baseUrl}/UserInfo`, inpUsr);
  }

  DeleteUser(userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/UserInfo/${userId}`);
  }

  GetAllProjects(): Observable<any> {
    return this.http.get(`${this.baseUrl}/ProjectInfo`);
  }

  AddNewProject(inpProj: ProjectInfo): Observable<any> {
    return this.http.get<ProjectInfo[]>(`${this.baseUrl}/ProjectInfo`).pipe(
      map(projects => {
        const maxId = projects.reduce((max, p) => Math.max(max, parseInt(p.id, 10)), 0);
        inpProj.id = (maxId + 1).toString();
        return inpProj;
      }),
      switchMap(newProject => this.http.post(`${this.baseUrl}/ProjectInfo`, newProject))
    );
  }

  DeleteProject(projectId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ProjectInfo/${projectId}`);
  }

  GetAllProjectManagers(): Observable<any> {
    return this.http.get<UserInfo[]>(`${this.baseUrl}/UserInfo`).pipe(
      map(users => users.filter((user: UserInfo) => user.role === 'PM'))
    );
  }

  GetProjectMembers(projectId: string): Observable<any> {
    return this.http.get<ProjectManager[]>(`${this.baseUrl}/ProjectManager?projId=${projectId}`);
  }

  AddProjectMember(member: ProjectManager): Observable<any> {
    return this.http.get<ProjectManager[]>(`${this.baseUrl}/ProjectManager`).pipe(
      map(members => {
        const maxId = members.reduce((max, m) => Math.max(max, parseInt(m.id, 10)), 0);
        member.id = (maxId + 1).toString();
        return member;
      }),
      switchMap(newMember => this.http.post(`${this.baseUrl}/ProjectManager`, newMember))
    );
  }

  RemoveProjectMember(memberId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ProjectManager/${memberId}`);
  }

  GetTasks(projectId: string): Observable<any> {
    return this.http.get<Task[]>(`${this.baseUrl}/Task?projId=${projectId}`);
  }

  AddTask(task: Task): Observable<any> {
    return this.http.get<Task[]>(`${this.baseUrl}/Task`).pipe(
      map(tasks => {
        const maxId = tasks.reduce((max, t) => Math.max(max, parseInt(t.id, 10)), 0);
        task.id = (maxId + 1).toString();
        return task;
      }),
      switchMap(newTask => this.http.post(`${this.baseUrl}/Task`, newTask))
    );
  }

  getTasksByUser(userId: string): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.baseUrl}/Task?assignedto=${userId}`);
  }

  addComment(comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(`${this.baseUrl}/Comment`, comment);
  }

  updateTaskStatus(taskId: string, status: string): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/Task/${taskId}`, { status });
  }

  assignTask(taskId: string, role: string): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/Task/${taskId}`, { assignedto: role });
  }
}