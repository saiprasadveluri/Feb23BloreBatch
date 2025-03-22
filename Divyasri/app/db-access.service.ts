import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { UserInfo } from './user-info';
import { Newproject } from './newproject';
import { Task } from './task';
import { Projectmembers } from './projectmembers';
import { Comment } from './comment';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  // Removed duplicate getUsersByIds method to resolve the error.
  // Removed duplicate AddNewProject method to resolve the error.
  private baseUrl = 'http://localhost:3010';
  private userAddedSubject = new Subject<void>(); // Subject to notify when a new user is added

  userAdded$ = this.userAddedSubject.asObservable(); // Observable for user-added notifications

  constructor(private http:HttpClient) 
  {


   }
   GetAllUsers():Observable<any>
   {
    return this.http.get("http://localhost:3010/UserInfo");

   }
   AddNewUser(inpusr:UserInfo):Observable<UserInfo>
   {
    return this.http.post<UserInfo>("http://localhost:3010/UserInfo",inpusr)
   }
   AddNewComment(inpcmt: Comment):Observable<any>
   {
    return this.http.post<Comment>(`http://localhost:3010/Comment`, inpcmt);
   }
   AddNewProject(inpproj:Newproject):Observable<Newproject>
   {
    return this.http.post<Newproject>("http://localhost:3010/project",inpproj)
   }
   getprojectList():Observable<any>
   {
     return this.http.get("http://localhost:3010/project");
   
 
   }
   // Fetch the list of projects
getprojectLists(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/project`);
}
   assignto(projectId: string): Observable<any> {
   
  return this.http.get("http://localhost:3010/project/${projectId}");

  }
  AddNewTask(inptsk:Task):Observable<Task>
  {
   return this.http.post<Task>("http://localhost:3010/Task",inptsk)
  }
  // getProjectsForPM(managerId: string): Observable<any> {
  //   return this.http.get(`${this.baseUrl}/project?pm=${managerId}`);
  // }

  // Fetch projects assigned to the logged-in Project Manager
  getProjectsForPM(pmId: string): Observable<Newproject[]> {
    return this.http.get<Newproject[]>(`${this.baseUrl}/project?pm=${pmId}`);
  }
  getProjectForPM(pmId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/project?pm=${pmId}`);
  }
  addProjectMember(projid: string, memid: string): Observable<Projectmembers> {
    const body = { id: this.generateUniqueId(), projid, memid };
    return this.http.post<Projectmembers>(`${this.baseUrl}/ProjectMember`, body);
  }

  // Add a new project member
  // addProjectMember(projectId: string, userId: string): Observable<any> {
  //   const body = {
  //     id: this.generateUniqueId(), // Generate a unique ID for the project member
  //     projid: projectId,
  //     memid: userId
  //   };
  //   return this.http.post(`${this.baseUrl}/ProjectMember`, body);
  // }

  // Utility function to generate a unique ID
  private generateUniqueId(): string {
    return Math.random().toString(36).substring(2, 8);
  }
 
 // Create a new task
  addTask(task: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Task`, task);
  }
 
  // // Fetch all members of a specific project
  // getProjectMembers(projectId: string): Observable<any> {
  //   return this.http.get(`${this.baseUrl}/ProjectMember?projid=${projectId}`);
  // }
    // Fetch members assigned to a project
    getProjectMembers(projid: string): Observable<Projectmembers[]> {
      return this.http.get<Projectmembers[]>(`${this.baseUrl}/ProjectMember?projid=${projid}`);
    }
    getProjectMember(projid: string): Observable<any[]> {
      return this.http.get<any[]>(`${this.baseUrl}/ProjectMember?projid=${projid}`);
    }
    
  

  // // Fetch users with roles Developer and QA
  // getUsersByRole(): Observable<any> {
  //   return this.http.get(`${this.baseUrl}/UserInfo?role=DEV,QA`);
  // }
    // Fetch users with roles Developer and QA
    getUsersByRole(p0: string): Observable<UserInfo[]> {
      return this.http.get<UserInfo[]>(`${this.baseUrl}/UserInfo?role=DEV,QA`);
    }
  
  // Fetch user details for specific user IDs
  getUsersByIds(userIds: string[]): Observable<any> {
    const query = userIds.map((id) => `id=${id}`).join('&');
    return this.http.get(`${this.baseUrl}/UserInfo?${query}`);
  }
 
  createTask(task: Task): Observable<Task> {
    return this.http.post<Task>(`${this.baseUrl}/Task`, task);
  }

  getTasksForMember(memberId: string): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.baseUrl}/Task?assignedto=${memberId}`);
  }
  gettaskList():Observable<any>
  {
    return this.http.get("http://localhost:3010/Task");
  

  }
  notifyUserAdded(): void {
    this.userAddedSubject.next();
  }

  


}
 
