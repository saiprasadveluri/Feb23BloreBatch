import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { Projectinfo } from './projectinfo';
import { Projectmember } from './projectmember';
import { Tasks } from './tasks';
import { Comments } from './comments';
 
@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  
 
  constructor(private http:HttpClient) { }
 
  GetAllUsers():Observable <any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }
  AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
  {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr);
  }
  AddNewTask(inpTask:Tasks):Observable<Tasks>{
    return this.http.post<Tasks>("http://localhost:3004/Task",inpTask);
  }
  DeleteUser(inpusr:string):Observable<any>{
    return this.http.delete(`http://localhost:3004/UserInfo/${inpusr}`);
  }
  GetAllProjects():Observable <any> {
    return this.http.get("http://localhost:3004/ProjectInfo");
  }
  DeleteProject(inpusr:string):Observable<any>{
    return this.http.delete(`http://localhost:3004/ProjectInfo/${inpusr}`);
  }
  AddNewProject(inpProj:Projectinfo):Observable<Projectinfo>
  {
    return this.http.post<Projectinfo>("http://localhost:3004/ProjectInfo",inpProj);
  }
  GetAllProjectMember():Observable <any> {
    return this.http.get("http://localhost:3004/projectMember");
  }
  GetAllTasks():Observable <any> {
    return this.http.get("http://localhost:3004/Task");
  }
  AddNewProjectMember(inpPrjmem:Projectmember):Observable<Projectmember>
  {
    return this.http.post<Projectmember>("http://localhost:3004/projectMember",inpPrjmem);
  }
  AddComment(inpComment:Comments):Observable<Comments>{
    return this.http.post<Comments>("http://localhost:3004/Comment",inpComment);
  }
  DeleteProjectMember(id:string):Observable<any>{
    return this.http.delete(`http://localhost:3004/projectMember/${id}`);
  }

  
}
              