import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { ProjectMember } from './project-member';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient)
   { 

   }
   GetAllUsers():Observable<any>
   {
    return this.http.get(" http://localhost:3004/UserInfo");

   }
   AddNewUser(inpUsr:UserInfo):Observable<any>
   {
    return this.http.post(" http://localhost:3004/UserInfo",inpUsr);

   }

   

 
AddNewProject(project: ProjectInfo): Observable<any> {

  return this.http.post('http://localhost:3004/Project', project);

}
GetAllProjects():Observable<any>
   {
    return this.http.get(" http://localhost:3004/Project");

}

getProjectsForManager(pmId: string): Observable<ProjectInfo[]> {
  return this.http.get<ProjectInfo[]>(`http://localhost:3004/Project?pm=${pmId}`);
}




getProjectMembers(projectId: string): Observable<ProjectMember[]> {
  return this.http.get<ProjectMember[]>(`http://localhost:3004/%20members?projectId=${projectId}`);
}




assignMemberToProject(projectId: string, member: ProjectMember): Observable<any> {
  return this.http.post(`http://localhost:3004/%20members`, member);
}


removeMemberFromProject(projectId: string, memberId: string) {
  const url = `http://localhost:3004/ members/${memberId}`;
  return this.http.delete(url);
}


getTasksForProject(projectId: string): Observable<Task[]> {
  return this.http.get<Task[]>(`http://localhost:3004/tasks?projectId=${projectId}`);
}


createTask(projectId: string, task: Task): Observable<any> {
  return this.http.post(`http://localhost:3004/tasks`, task);
}

}







  





