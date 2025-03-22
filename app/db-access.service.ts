// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { UserInfo } from './user-info';
// import { ProjectInfo } from './project-info';
 
// @Injectable({
//   providedIn: 'root'
// })
// export class DbAccessService {
//   // [x: string]: any;
 
//   constructor(private http:HttpClient)
//    {
 
//     }
//     DeleteUser(userId: number): Observable<void> {
//       return this.http.delete<void>(`http://localhost:3004/UserInfo/${userId}`);
//     }
   
//     GetAllUsers():Observable<any>
//     {
//       return this.http.get("http://localhost:3004/UserInfo")
//     }
 
//     AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
//     {
//       return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr)
//     }
 
//     GetAllProjects():Observable<any>
//     {
//       return this.http.get("http://localhost:3004/Project")
//     }
 
//     AddNewProject(inpPro:ProjectInfo):Observable<ProjectInfo>
//     {
//       return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpPro)
//     }

//     assignProjectToUser(userId: number, projectId: number): Observable<void> {
//       return this.http.put<void>(`http://localhost:3004/UserInfo/${userId}`, { projectId });
//     }
// }
  
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
 
@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  apiUrl: any;
  // [x: string]: any;
 
  constructor(private http:HttpClient)
   {
 
    }
    DeleteUser(userId: number): Observable<void> {
      return this.http.delete<void>(`http://localhost:3004/UserInfo/${userId}`);
    }
   
    GetAllUsers():Observable<any>
    {
      return this.http.get("http://localhost:3004/UserInfo")
    }
 
    AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
    {
      return this.http.post<UserInfo>("http://localhost:3004/UserInfo",inpUsr)
    }
 
    // GetAllProjects():Observable<any>
    // {
    //   return this.http.get("http://localhost:3004/Project")
    // }
    GetAllPMs(): Observable<UserInfo[]> {
      return this.http.get<UserInfo[]>("http://localhost:3004/UserInfo?role=PM");
    }
 
    // AddNewProject(inpPro:ProjectInfo):Observable<ProjectInfo>
    // {
    //   return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpPro)
    // }
 
    AddNewProject(project: ProjectInfo): Observable<ProjectInfo> {
      return this.http.post<ProjectInfo>("http://localhost:3004/Project", project);
    } 
    assignProjectToPM(projectId: string, pmId: string): Observable<any> {
      return this.http.put(`${this.apiUrl}/projects/${projectId}/assign`, { pmId });
    }
 
    GetAllProjects(): Observable<ProjectInfo[]> {
      return this.http.get<ProjectInfo[]>("http://localhost:3004/Project");
    }

    getProjectsByPM(pmId: string): Observable<any[]> {
      return this.http.get<any[]>(`${this.apiUrl}/projects?pmId=${pmId}`);
    }

    assignMemberToProject(projectId: string, memberId: string): Observable<any> {
      return this.http.put(`${this.apiUrl}/projects/${projectId}/assign-member`, { memberId });
    }

    removeMemberFromProject(projectId: string, memberId: string): Observable<any> {
      return this.http.put(`${this.apiUrl}/projects/${projectId}/remove-member`, { memberId });
    }

    createTask(newTask: any): Observable<any> {
      return this.http.post(`${this.apiUrl}/tasks`, newTask);
    }

    getTasksByProject(projectId: string): Observable<any[]> {
      return this.http.get<any[]>(`${this.apiUrl}/tasks?projectId=${projectId}`);
    }
  
}
 
 