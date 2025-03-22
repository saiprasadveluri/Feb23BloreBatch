import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Userinfo } from './userinfo';
import { Project } from './project';
import { Projectmember } from './projectmember';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient) { }

  GetAllUsers():Observable <any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }
  AddNewUser(inpUsr:Userinfo):Observable<Userinfo>{
    return this.http.post<Userinfo>("http://localhost:3004/UserInfo",inpUsr);
  }
  GetAllProjects():Observable <any> {
    return this.http.get("http://localhost:3004/Project");
  }
  AddNewProject(inpPrj:Project):Observable<Project>{
    return this.http.post<Project>("http://localhost:3004/Project",inpPrj);
  }
  deleteUser(inpusr:string):Observable<any>{
    return this.http.delete(`http://localhost:3004/UserInfo/${inpusr}`);
  }
  GetAllProjectMember():Observable<any>{
    return this.http.get(`http://localhost:3004/projectMember`);
  }
  AddNewProjectMember(inpPjmb:Projectmember):Observable<Projectmember>{
    return this.http.post<Projectmember>(`http://localhost:3004/projectMember`,inpPjmb);
  }
  GetAllTasks():Observable<any>{
    return this.http.get(`http://localhost:3004/Task`);
  }
  AddNewTask(inpTsk:Task):Observable<Task>{
    return this.http.post<Task>(`http://localhost:3004/Task`,inpTsk);
  }
}

