// import { Injectable } from '@angular/core';
 // import { HttpClient } from '@angular/common/http';
 // import { Observable } from 'rxjs';
 // import { UserInfo } from './user-info';
 // import { ProjectInfo } from './project-info';
  
 // @Injectable({
 //   providedIn: 'root'
 // })
 // export class DbAccessService {
 //   // [x: string]: any;
  
 //   constructor(private http:HttpClient)
 //    {
  
 //     }
 //     DeleteUser(userId: number): Observable<void> {
 //       return this.http.delete<void>(`http://localhost:3004/UserInfo/${userId}`);
 //     }
    
 //     GetAllUsers():Observable<any>
 //     {
 //       return this.http.get("http://localhost:3004/UserInfo")
 //     }
  
 //     AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
 //     {
 //       return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr)
 //     }
  
 //     GetAllProjects():Observable<any>
 //     {
 //       return this.http.get("http://localhost:3004/Project")
 //     }
  
 //     AddNewProject(inpPro:ProjectInfo):Observable<ProjectInfo>
 //     {
 //       return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpPro)
 //     }
 
 //     assignProjectToUser(userId: number, projectId: number): Observable<void> {
 //       return this.http.put<void>(`http://localhost:3004/UserInfo/${userId}`, { projectId });
 //     }
 // }

 import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { TaskInfo } from './task-info';
import { CommentInfo } from './comment-info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>("http://localhost:3004/UserInfo");
  }

  addUser(user: UserInfo): Observable<UserInfo> {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo", user);
  }

  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:3004/UserInfo/${userId}`);
  }

  getAllProjects(): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>("http://localhost:3004/Project");
  }

  addProject(project: ProjectInfo): Observable<ProjectInfo> {
    return this.http.post<ProjectInfo>("http://localhost:3004/Project", project);
  }

  getProjectsByPM(pmId: string): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>(`http://localhost:3004/Project?pmId=${pmId}`);
  }

  assignMemberToProject(projectId: string, memberId: string): Observable<any> {
    return this.http.put(`http://localhost:3004/Project/${projectId}/assign-member`, { memberId });
  }

  removeMemberFromProject(projectId: string, memberId: string): Observable<any> {
    return this.http.put(`http://localhost:3004/Project/${projectId}/remove-member`, { memberId });
  }

  createTask(task: TaskInfo): Observable<TaskInfo> {
    return this.http.post<TaskInfo>("http://localhost:3004/Task", task);
  }

  getTasksByProject(projectId: string): Observable<TaskInfo[]> {
    return this.http.get<TaskInfo[]>(`http://localhost:3004/Task?projectId=${projectId}`);
  }

  getTasksByUser(userId: string): Observable<TaskInfo[]> {
    return this.http.get<TaskInfo[]>(`http://localhost:3004/Task?assigneeId=${userId}`);
  }

  reassignTask(taskId: string, newAssigneeId: string): Observable<any> {
    return this.http.put(`http://localhost:3004/Task/${taskId}/reassign`, { newAssigneeId });
  }

  addComment(comment: CommentInfo): Observable<CommentInfo> {
    return this.http.post<CommentInfo>("http://localhost:3004/Comment", comment);
  }

  getCommentsByTask(taskId: string): Observable<CommentInfo[]> {
    return this.http.get<CommentInfo[]>(`http://localhost:3004/Comment?taskId=${taskId}`);
  }

  getAllPMs(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>("http://localhost:3004/UserInfo?role=PM");
  }

  assignProjectToPm(payload: { projectId: string, pmId: string }): Observable<any> {
    return this.http.put(`http://localhost:3004/Project/${payload.projectId}/assign-pm`, { pmId: payload.pmId });
  }
}