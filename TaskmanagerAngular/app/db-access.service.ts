import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Project } from './project';
import { Task } from './task';
import { ProjectMembers } from './project-members';

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
}
