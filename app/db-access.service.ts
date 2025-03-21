// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { forkJoin, map, Observable, switchMap } from 'rxjs';

// interface ProjectMember {
//   projid: string;
//   memid: string;
//   name:string;
// }
// import { UserInfo } from './user-info';
// import { ProjectInfo } from './project-info';
// import { TaskInfo } from './task-info';

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

//     // GetAllProjects():Observable<any>
//     // {
//     //   return this.http.get("http://localhost:3004/Project")
//     // }
//     GetAllPMs(): Observable<UserInfo[]> {
//       return this.http.get<UserInfo[]>("http://localhost:3004/UserInfo?role=PM");
//     }

//     // AddNewProject(inpPro:ProjectInfo):Observable<ProjectInfo>
//     // {
//     //   return this.http.post<ProjectInfo>("http://localhost:3004/Project",inpPro)
//     // }

//     AddNewProject(project: ProjectInfo): Observable<ProjectInfo> {
//       return this.http.post<ProjectInfo>("http://localhost:3004/Project", project);
//     }
//     GetAllProjects(): Observable<ProjectInfo[]> {
//       return this.http.get<ProjectInfo[]>("http://localhost:3004/Project");
//     }
//     GetProjectsByPM(pmId: string): Observable<ProjectInfo[]> {
//       return this.http.get<ProjectInfo[]>(`http://localhost:3004/Project?pm=${pmId}`);
//     }
//     GetMembersByProject(projid: string): Observable<any[]> {
//       return this.http.get<ProjectMember[]>(`http://localhost:3004/ProjectMember?projid=${projid}`).pipe(
//         switchMap((projectMembers: ProjectMember[]) => {
//           if (projectMembers.length === 0) {
//             return new Observable<any[]>(observer => {
//               observer.next([]);
//               observer.complete();
//             });
//           }
//           const memberRequests = projectMembers.map(pm =>
//             this.http.get<UserInfo>(`http://localhost:3004/UserInfo/${pm.memid}`)
//           );
//           return forkJoin(memberRequests);
//         })
//       );
//     }
    
//     RemoveMemberFromProject(id: string): Observable<void> {
//       return this.http.delete<void>(`http://localhost:3004/ProjectMember/${id}`);
//     }

//     AssignMembersToProject(projid: string, memberIds: string[]): Observable<void> {
//       const requests = memberIds.map(memid =>
//         this.http.post<void>(`http://localhost:3004/ProjectMember`, { projid, memid })
//       );
//       return forkJoin(requests).pipe(map(() => {}));
//     }


//     GetTasksByProject(projid: string): Observable<TaskInfo[]> {
//       return this.http.get<TaskInfo[]>(`http://localhost:3004/Task?projid=${projid}`);
//     }
//     CreateTask(task: Omit<TaskInfo, 'id'>): Observable<TaskInfo> {
//       return this.http.post<TaskInfo>('http://localhost:3004/Task', task);
//     }
    
    
// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, Observable, switchMap } from 'rxjs';

interface ProjectMember {
  projid: string;
  memid: string;
  name: string;
}

import { UserInfo } from './user-info';
import { ProjectInfo } from './project-info';
import { TaskInfo } from './task-info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {
  constructor(private http: HttpClient) {}

  // User Management
  DeleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:3004/UserInfo/${userId}`);
  }

  GetAllUsers(): Observable<any> {
    return this.http.get("http://localhost:3004/UserInfo");
  }

  AddNewUser(inpUsr: UserInfo): Observable<UserInfo> {
    return this.http.post<UserInfo>("http://localhost:3004/UserInfo", inpUsr);
  }

  GetAllPMs(): Observable<UserInfo[]> {
    return this.http.get<UserInfo[]>("http://localhost:3004/UserInfo?role=PM");
  }

  // Project Management
  AddNewProject(project: ProjectInfo): Observable<ProjectInfo> {
    return this.http.post<ProjectInfo>("http://localhost:3004/Project", project);
  }

  GetAllProjects(): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>("http://localhost:3004/Project");
  }

  GetProjectsByPM(pmId: string): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>(`http://localhost:3004/Project?pm=${pmId}`);
  }

  // Project Member Management
  GetMembersByProject(projid: string): Observable<any[]> {
    return this.http.get<ProjectMember[]>(`http://localhost:3004/ProjectMember?projid=${projid}`).pipe(
      switchMap((projectMembers: ProjectMember[]) => {
        if (projectMembers.length === 0) {
          return new Observable<any[]>(observer => {
            observer.next([]);
            observer.complete();
          });
        }
        const memberRequests = projectMembers.map(pm =>
          this.http.get<UserInfo>(`http://localhost:3004/UserInfo/${pm.memid}`)
        );
        return forkJoin(memberRequests);
      })
    );
  }

  RemoveMemberFromProject(id: string): Observable<void> {
    return this.http.delete<void>(`http://localhost:3004/ProjectMember/${id}`);
  }

  AssignMembersToProject(projid: string, memberIds: string[]): Observable<void> {
    const requests = memberIds.map(memid =>
      this.http.post<void>(`http://localhost:3004/ProjectMember`, { projid, memid })
    );
    return forkJoin(requests).pipe(map(() => {}));
  }

  // Task Management
  GetTasksByProject(projid: string): Observable<TaskInfo[]> {
    return this.http.get<TaskInfo[]>(`http://localhost:3004/Task?projid=${projid}`);
  }

  CreateTask(task: Omit<TaskInfo, 'id'>): Observable<TaskInfo> {
    return this.http.post<TaskInfo>('http://localhost:3004/Task', task);
  }
}
