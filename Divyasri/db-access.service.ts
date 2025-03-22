import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Newproject } from './newproject';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  // Removed duplicate getUsersByIds method to resolve the error.
  // Removed duplicate AddNewProject method to resolve the error.
  private baseUrl = 'http://localhost:3010';
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
   AddNewProject(inpproj:Newproject):Observable<Newproject>
   {
    return this.http.post<Newproject>("http://localhost:3010/project",inpproj)
   }
   getprojectList():Observable<any>
   {
     return this.http.get("http://localhost:3010/project");
   
 
   }
   assignto(projectId: string): Observable<any> {
   
  return this.http.get("http://localhost:3010/project/${projectId}");

  }
  AddNewTask(inptsk:Task):Observable<Task>
  {
   return this.http.post<Task>("http://localhost:3010/Task",inptsk)
  }
  getProjectsForPM(managerId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/project?pm=${managerId}`);
  }

  addProjectMember(projectid:any,usrid:string): Observable<any> {
 
    const body = { projectid, usrid };
    return this.http.post(`${this.baseUrl}/ProjectMember`, body);
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
 
  // Fetch all members of a specific project
  getProjectMembers(projectId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/ProjectMember?projid=${projectId}`);
  }

  // Fetch users with roles Developer and QA
  getUsersByRole(): Observable<any> {
    return this.http.get(`${this.baseUrl}/UserInfo?role=DEV,QA`);
  }
  
  // Fetch user details for specific user IDs
  getUsersByIds(userIds: string[]): Observable<any> {
    const query = userIds.map((id) => `id=${id}`).join('&');
    return this.http.get(`${this.baseUrl}/UserInfo?${query}`);
  }
 





}
 
