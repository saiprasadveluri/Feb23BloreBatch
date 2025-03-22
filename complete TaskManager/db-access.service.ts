import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { ProjectMember } from './project-member';
import { Task } from './task';
import { CommentInfo } from './comment-info';

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




/*assignMemberToProject(projectId: string, member: ProjectMember): Observable<any> {
  return this.http.post(`http://localhost:3004/%20members`, member);
}*/


assignMemberToProject(projectId: string, member: ProjectMember): Observable<any> {
  const payload = { ...member, projectId }; // Include projectId in the request body
  return this.http.post(`http://localhost:3004/ members`, payload);
}




/*removeMemberFromProject(projectId: string, memberId: string): Observable<any> {
  return this.http.delete("http://localhost:3004/members/${memberId}");
}*/

removeMemberFromProject(projectId: string, memberId: string): Observable<any> {
  return this.http.delete(`http://localhost:3004/ members/${memberId}`);
}





getTasksForProject(projectId: string): Observable<Task[]> {
  return this.http.get<Task[]>(`http://localhost:3004/tasks?projectId=${projectId}`);
}



/*createTask(projectId: string, task: Task): Observable<any> {
  return this.http.post(`http://localhost:3004/tasks`, task);
}*/

createTask(projectId: string, task: Task): Observable<any> {
  const payload = { ...task, projectId }; // Include projectId in the request body
  return this.http.post(`http://localhost:3004/tasks`, payload);
}


// Fetch tasks assigned to a specific user
/*getTasksForUser(userId: string): Observable<Task[]> {
  return this.http.get<Task[]>(`http://localhost:3004/tasks?assignedTo.id=${userId}`);
}*/

/*getTasksForUser(userId: string): Observable<Task[]> {
  return this.http.get<Task[]>(`http://localhost:3004/tasks?assignedTo.id=${userId}`);
}*/

getTasksForUser(userId: string): Observable<Task[]> {
  return this.http.get<Task[]>(`http://localhost:3004/tasks?assignedTo.id=${userId}`);
}


getCommentsForTask(taskId: string): Observable<CommentInfo[]> {
  return this.http.get<CommentInfo[]>(`http://localhost:3004/comments?taskId=${taskId}`);
}


addCommentToTask(taskId: string, comment: CommentInfo): Observable<any> {
  return this.http.post(`http://localhost:3004/comments`, comment);
}


reassignTask(taskId: string, newUserId: string): Observable<any> {
  return this.http.patch(`http://localhost:3004/tasks/${taskId}`, {
    assignedTo: { id: newUserId }
  });
}


deleteTask(taskId: string): Observable<any> {
  return this.http.delete(`http://localhost:3004/tasks/${taskId}`);
}

}








  





