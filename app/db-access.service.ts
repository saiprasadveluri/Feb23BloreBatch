import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjInfo } from './proj-info';
import { ProjMember } from './proj-member';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  constructor(private http:HttpClient) 
  { 

  }
  GetAllUsers():Observable<any>
  {
    return this.http.get("http://localhost:3004/UserInfo");
  }
  GetAllMembers():Observable<any>
  {
    return this.http.get("http://localhost:3004/ProjectMember");
  }
  GetAllProjects():Observable<any>
  {
    return this.http.get("http://localhost:3004/Project");
  }
  AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
  {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr);
  }
 
  AddNewProj(inpProj:ProjInfo):Observable<ProjInfo>{
    return this.http.post<ProjInfo>("http://localhost:3004/Project",inpProj);
  }
  deleteUser(id:string):Observable<any>
  {
    return this.http.delete("http://localhost:3004/UserInfo/${id}");
  }


}
