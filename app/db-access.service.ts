import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable , forkJoin} from 'rxjs';
import { UserInfo } from './user-info';
import { Project } from './project';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient) { }

  GetAllUsers():Observable<any>{
    return this.http.get("http://localhost:3004/UserInfo");
  }

  AddNewUser(inpUser:UserInfo):Observable<UserInfo>{
    console.log(inpUser);
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo", inpUser);
  }

  AddNewProject(inpProject:Project):Observable<Project>{
    console.log(inpProject);
    return this.http.post<UserInfo>("http://localhost:3004/Project", inpProject);
  }

  DeleteUser(id:string){
    return this.http.delete("http://localhost:3004/UserInfo/${id}");
  }

  GetAllProjects():Observable<any>{
    return this.http.get("http://localhost:3004/Project");
  }

  updateProject(id: string, updatedProject: Project): Observable<any> {
    return this.http.put("http://localhost:3004/Project/${id}", updatedProject);
  }

  GetProjectMembers(id:string):Observable<any>{
    return this.http.get("http://localhost:3004/ProjectMember");
  }

  AddNewTask(inpTask:Task):Observable<Task>{
    console.log(inpTask);
    return this.http.post<Task>("http://localhost:3004/Task", inpTask);
  }

  GetProjectTasks(projectId: string): Observable<any> {
    return this.http.get(`http://localhost:3004/Task?projectId=${projectId}`);
  }

  GetAllTasks():Observable<any>{
    return this.http.get("http://localhost:3004/Task");
  }

  GetAllComments():Observable<any>{
    return this.http.get("http://localhost:3004/Comment");
  }

  
}
