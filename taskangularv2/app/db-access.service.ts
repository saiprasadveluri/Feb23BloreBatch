import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Project } from './project';
import { Task } from './task';
import { ProjectMembers } from './project-members';
import { TaskComment } from './Taskcomment';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  
  constructor(private http:HttpClient) { 

  }
  GetAllUsers():Observable<any>
  {
    return this.http.get("http://localhost:3000/UserInfo");

  }
  AddNewUser(InpUser:UserInfo):Observable<any>
  {
    return this.http.post("http://localhost:3000/UserInfo",InpUser);
  }

  AddNewProject(InpProject:Project):Observable<any>{
    return this.http.post("http://localhost:3000/Project",InpProject);
  }
  GetAllProjects():Observable<any>{
    return this.http.get("http://localhost:3000/Project");    
  }
  SubmitTask(InpTask: Task): Observable<any> {
    return this.http.post("http://localhost:3000/Task", InpTask);
  }
  private projectData: { id: string; name: string } | null = null;

  setProjectData(data: { id: string; name: string }) {
    this.projectData = data;
  }
  getProjectData() {
    return this.projectData;
  } 
  GetprojectMembers():Observable<any>{
    return this.http.get("http://localhost:3000/ProjectMembers");
  }
  GetTasks():Observable<any>{
    return this.http.get("http://localhost:3000/Task");
  }
  AddProjectMember(meminp:ProjectMembers):Observable<any>
  {
    return this.http.post("http://localhost:3000/ProjectMembers",meminp);
  }
  // AssignUserToProject(userId: string, projectId: string) {
  //   return this.http.post('/api/assignUserToProject', { userId, projectId });
  // }
  GetAllMembers():Observable<any>{
    return this.http.get("http://localhost:3000/ProjectMembers")
  }
  AddComment(comment: TaskComment):Observable<any>{
    return this.http.post('http://localhost:3000/Comment',comment); 
  }
  UpdateTaskStatus(taskId: string, newStatus: string): Observable<any> {
    const url = `http://localhost:3000/Task/${taskId}`; // Endpoint for updating task status
    return this.http.patch(url, { status: newStatus }); // Send PATCH request with updated status
  }
  GetCommentsByTaskId(taskId: string): Observable<TaskComment[]> {
    const url = `http://localhost:3000/Comment?taskid=${taskId}`; // Endpoint to fetch comments by task ID
    return this.http.get<TaskComment[]>(url); // Send GET request to fetch comments
  }
  GetUserInfoByMemId(memid: string): Observable<{ memid: string; role: string; name: string }> {
    return this.http.get<{ memid: string; role: string; name: string }>(`/api/userinfo/${memid}`);
  }
  AssignTask(taskId: string, assigneeId: string): Observable<any> {
    return this.http.post<any>('/api/assign-task', { taskId, assigneeId });
  }
  RemoveUserFromProject(userId: string): Observable<any> {
    return this.http.delete<any>(`/api/remove-user/${userId}`);
  }
  GetUserinfoRoles(memIds: string[]): Observable<{ id: string; role: string }[]> {
    const url = `http://localhost:3000/UserInfo`; // Endpoint for fetching user info
    return this.http.get<{ id: string; role: string }[]>(url).pipe(
      map((users) => users.filter((user) => memIds.includes(user.id))) // Filter users by memIds
    );
  }
}

