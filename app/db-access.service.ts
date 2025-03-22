import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from './user-info';
import { Observable } from 'rxjs';
import { ProjectInfo } from './project-info';
import { TaskInfo } from './task-info';
import { ProjectmemberInfo } from './projectmember-info';
import { CommentInfo } from './comment-info';



@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  constructor(private http:HttpClient) {
   }
   GetAllUsers():Observable<UserInfo[]>{
    return this.http.get<UserInfo[]>("http://localhost:3005/UserInfo");
   }
   AddNewUser(inpUsr:UserInfo):Observable<any>{
    return this.http.post<UserInfo>("http://localhost:3005/UserInfo",inpUsr);
   }
   DeleteUser(userId: string): Observable<any> {
    return this.http.delete<UserInfo>(`http://localhost:3005/UserInfo/${userId}`);
   }

   AddNewProject(project: ProjectInfo): Observable<any> {
    return this.http.post<ProjectInfo>("http://localhost:3005/Project", project);
   }
   GetProjects(): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>('http://localhost:3005/Project');
   }

   AddProjectMember(projectmember: ProjectmemberInfo): Observable<any> {
    return this.http.post<ProjectmemberInfo>('http://localhost:3005/ProjectMember', projectmember);
   }

   GetProjectMembers(projid: string): Observable<ProjectmemberInfo[]> {
    return this.http.get<ProjectmemberInfo[]>('http://localhost:3005/ProjectMember');
   }

   GetPProjectMembers(projid: string): Observable<ProjectmemberInfo[]> {
    return this.http.get<ProjectmemberInfo[]>(`http://localhost:3005/ProjectMember?projid=${projid}`);
   }
  
   GetTasks(): Observable<TaskInfo[]> {
    return this.http.get<TaskInfo[]>('http://localhost:3005/Task');
   }
   AddNewTask(task:TaskInfo): Observable<any> {
    return this.http.post<TaskInfo>('http://localhost:3005/Task',task);
   }

   UpdateTask(task: TaskInfo): Observable<any> {
    return this.http.put<TaskInfo>(`http://localhost:3005/Task/${task.id}`, task);
   }

   GetComments(taskId: string): Observable<CommentInfo[]> {
    return this.http.get<CommentInfo[]>(`http://localhost:3005/Comment?taskid=${taskId}`);
   }
   AddComment(comment: CommentInfo): Observable<any> {
    return this.http.post<CommentInfo>('http://localhost:3005/Comment', comment);
   }
}

