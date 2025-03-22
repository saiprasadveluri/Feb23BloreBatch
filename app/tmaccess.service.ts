import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Userinfo } from './userinfo';
import { ProjectInfo } from './project-info';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TmaccessService {
  private userUrl = 'http://localhost:3004/UserInfo'; // Base URL for users
  private projectUrl = 'http://localhost:3004/Project'; // Base URL for projects
  private projectMembersUrl = 'http://localhost:3004/ProjectMember'; // Base URL for project members
  private taskUrl = 'http://localhost:3004/Task'; // Base URL for tasks

  constructor(private http: HttpClient) {}

  // Fetch all users
  getallusers(): Observable<any[]> {
    return this.http.get<any[]>(this.userUrl);
  }

  // Fetch all projects
  getallprojects(): Observable<any[]> {
    return this.http.get<any[]>(this.projectUrl);
  }

  // Add a new user
  AddNewUser(inpusr: Userinfo): Observable<Userinfo> {
    return this.http.post<Userinfo>(this.userUrl, inpusr);
  }

  // Add a new project
  AddNewProject(inppr: ProjectInfo): Observable<ProjectInfo> {
    return this.http.post<ProjectInfo>(this.projectUrl, inppr);
  }

  // Delete a user by ID
  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${this.userUrl}/${userId}`);
  }

  // Fetch tasks by project ID
  getTasksByProject(projectId: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.taskUrl).pipe(
      map((tasks: Task[]) => tasks.filter(task => task.projectId === projectId)) // Filter tasks by projectId
    );
  }

  // Create a new task
  createTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.taskUrl, task);
  }

  // Fetch members of a specific project
  getProjectMembers(projectId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.projectMembersUrl}?projid=${projectId}`);
  }

  // Add a member to a project
  addProjectMember(member: { projid: string; memid: string }): Observable<any> {
    return this.http.post<any>(this.projectMembersUrl, member);
  }

  // Remove a project member by ID
  removeProjectMember(memberId: string): Observable<any> {
    return this.http.delete(`${this.projectMembersUrl}/${memberId}`);
  }

  getAllProjectMembers(): Observable<any[]> {
    return this.http.get<any[]>(this.projectMembersUrl); // Fetch all project members
  }

  getAllTasks(id:string):Observable<Task[]> {
    return this.http.get<any[]>(`${this.taskUrl}?assignedTo=${id}`);
  }

  
  
  
}