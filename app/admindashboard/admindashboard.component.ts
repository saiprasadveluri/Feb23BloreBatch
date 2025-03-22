import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { ProjectInfo } from '../project-info';
import { DBAccessService } from '../dbaccess.service';
 
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  users: UserInfo[]=[];
  projects:ProjectInfo[]=[];
  constructor(private router:Router,private srv:DBAccessService ) {
   
  }
  ngOnInit(): void {
    this.loadUsers();
    this.loadProjects();
  }
  loadUsers(){
    this.srv.GetAllUsers().subscribe({
      next: (res: UserInfo[])=>{
      this.users=res;
      console.log('Users:',this.users);
      },
      error:(err: any)=>{
        console.error('Error fetching users:',err);
      }
    });
  }
  loadProjects(){
    this.srv.GetAllProjects().subscribe({
      next:(res:ProjectInfo[])=>{
        this.projects=res;
        console.log('Projects:',this.projects);
      },
      error:(err)=>{
        console.error('Error fetching projects',err)
      }
    });
  }
  NavigateToAddNewUser()
  {
    this.router.navigate(['addnewuser']);
  }
  NavigateToAddNewProject()
  {
    this.router.navigate(['addnewproject']);
  }
  deleteUser(id: string){
    this.srv.DeleteUser(id).subscribe({
      next:()=>{
        console.log('User with ID ${id} deleted sucessfully');
        this.loadUsers();
      },
      error: (err)=>{
        console.error('Error deleting user',err)
      }
    });
  }
}