import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user.info';
import { ProjectInfo } from './project.info'; // Ensure you have this interface defined
import { TaskInfo } from './task.info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http: HttpClient) { }

  // Existing methods
  GetAllUsers(): Observable<any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }

  GetAllProjects():Observable<any>{
    return this.http.get("http://localhost:3004/ProjectInfo");
  }

  AddNewUser(inpUsr: UserInfo): Observable<UserInfo> {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo", inpUsr);
  }
DeleteUser(userId:number):Observable<any>{
  return this.http.delete('http://localhost:3004/UserInfo/${userId}');
}
  // New method for adding a project
  AddNewProject(inpProj: ProjectInfo): Observable<ProjectInfo>{
    return this.http.post<ProjectInfo>("http://localhost:3004/ProjectInfo", inpProj);
  }
  GetAllTasks(): Observable<TaskInfo[]>{
    return this.http.get<TaskInfo[]>("http://localhost:3004/Tasks");
  }

  UpdateTaskStatus(taskId: number, status: string): Observable<any> {
    return this.http.patch(`http://localhost:3004/Tasks/${taskId}`, { status });
  }

  AddCommentToTask(taskId: number, comment: string): Observable<any> {
    return this.http.post(`http://localhost:3004/Tasks/${taskId}/comments`, { comment });
  }
}
