import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient) 
  {

   }
   GetAllUsers():Observable<any>{
    return this.http.get("http://localhost:3004/UserInfo");
   }
   AddNewUser(inpUsr:UserInfo):Observable<UserInfo>{
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr);
   }
   AddNewProject(inpProj:ProjectInfo):Observable<ProjectInfo>{
    return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpProj);
   }
   GetAllProjects():Observable<any>{
    return this.http.get("http://localhost:3004/Project");
   }
   GetAllProjectMembers():Observable<any>{
    return this.http.get("http://localhost:3004/Project Member");
   }
   GetOwnProjects(managerId: string): Observable<any[]> {
    console.log(`Fetching projects for manager ID: ${managerId}`);
    return this.http.get<any[]>(`http://localhost:3004/Project?pm=${managerId}`).pipe(
      tap(data => console.log('Data fetched:', data))
    );
  }
}
