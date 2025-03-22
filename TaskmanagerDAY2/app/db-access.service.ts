import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
 import { Observable } from 'rxjs';
import { Userinfo } from './userinfo';
import { Projectinfo } from './projectinfo';
import { Projectmember } from './projectmember';
import { Taskinfo } from './taskinfo';
import { Commentinfo } from './commentinfo';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient) { }

    GetAllUsers():Observable <any> {
     return  this.http.get("http://localhost:3004/userInfo");
    }
    GetAllTasks():Observable <any> {
      return  this.http.get("http://localhost:3004/Task");
     }
    AddNewUser(inpuser:Userinfo):Observable<Userinfo>
    {
      return this.http.post<Userinfo>("http://localhost:3004/userInfo",inpuser);
    }
    GetAllProjects():Observable<any>
    {
      return this.http.get("http://localhost:3004/Project");
    }
    AddNewProject(project: Projectinfo): Observable<any> {
      return this.http.post('http://localhost:3004/Project', project);
    }
    Deleteuser(userid:string):Observable<any>
    {
      return this.http.delete(`http://localhost:3004/userInfo/${userid}`);
    }
    OnClickassign(proj:Projectmember):Observable<Projectmember>
    {
      return this.http.post<Projectmember>('http://localhost:3004/ProjectMember',proj);
    }
    GetAllProjectmembers():Observable<any>
    {
      return this.http.get("http://localhost:3004/ProjectMember");
    }
    Addtask(task:Taskinfo): Observable<any> {
      return this.http.post('http://localhost:3004/Task', task);
    }
    AddComment(comment: Commentinfo): Observable<Commentinfo> {
      return this.http.post<Commentinfo>(`http://localhost:3004/Comment`, comment);
    }
}

 
 
