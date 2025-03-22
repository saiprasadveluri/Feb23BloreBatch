import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of } from 'rxjs';
import { Userinfo } from './userinfo';
import { ProjectInfo } from './project-info';
import { Task } from './task';
import { TaskComment } from './comment';
import { Projectmember } from './projectmember';

@Injectable({
  providedIn: 'root'
})
export class TmaccessService {
  private userUrl = 'http://localhost:3004/UserInfo'; // Base URL for users
  private projectUrl = 'http://localhost:3004/Project'; // Base URL for projects
  private projectMembersUrl = 'http://localhost:3004/ProjectMember'; // Base URL for project members
  private taskUrl = 'http://localhost:3004/Task'; // Base URL for tasks
  private commentUrl = 'http://localhost:3004/Comment'; // Base URL for comments

  constructor(private http: HttpClient) {}

  // Fetch all users
  getallusers(): Observable<Userinfo[]> {
    return this.http.get<Userinfo[]>(this.userUrl);
  }

  // Fetch all projects
  getallprojects(): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>(this.projectUrl);
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
  deleteUser(userId: string): Observable<any> {
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

  // Update an existing task
  updateTask(task: Task): Observable<Task> {
    return this.http.put<Task>(`${this.taskUrl}/${task.id}`, task);
  }

  // Fetch members of a specific project
  getProjectMembers(projectId: string): Observable<Projectmember[]> {
    return this.http.get<Projectmember[]>(`${this.projectMembersUrl}?projid=${projectId}`);
  }

  // Add a member to a project
  addProjectMember(member: Projectmember): Observable<Projectmember> {
    return this.http.post<Projectmember>(this.projectMembersUrl, member);
  }

  // Remove a project member by ID
  removeProjectMember(memberId: string): Observable<any> {
    return this.http.delete(`${this.projectMembersUrl}/${memberId}`);
  }

  getAllProjectMembers(): Observable<Projectmember[]> {
    return this.http.get<Projectmember[]>(this.projectMembersUrl); // Fetch all project members
  }

  getAllTasks(id: string): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.taskUrl}?assignedTo=${id}`);
  }

  addcomment(com: TaskComment): Observable<TaskComment> {
    return this.http.post<TaskComment>(this.commentUrl, com);
  }

  getCommentsByTaskId(taskId: string): Observable<TaskComment[]> {
    return this.http.get<TaskComment[]>(`${this.commentUrl}?TaskId=${taskId}`);
  }

   //get project member by user id
   getProjectMemberByUserId(userId: string): Observable<Projectmember | null> {
    return this.http.get<Projectmember[]>(`${this.projectMembersUrl}?memid=${userId}`).pipe(
      map(members => members.length > 0 ? members[0] : null),
      catchError(error => {
        console.error('Error fetching project member:', error);
        return of(null); // Return null in case of an error
      })
    );
  }

  //update project member
  updateProjectMember(member: Projectmember): Observable<Projectmember> {
    return this.http.put<Projectmember>(`${this.projectMembersUrl}/${member.id}`, member);
  }
}