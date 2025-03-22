import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Projectinfo } from '../projectinfo';
import { Userinfo } from '../userinfo';
import { Router } from '@angular/router';
import { Projectmember } from '../projectmember';
import { Taskinfo } from '../taskinfo';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit{
    projects: Projectinfo[] = [];
    projmem:Projectmember[]=[];
    users:Userinfo[]=[];
    tasks:Taskinfo[]=[];
     constructor(private srv:DbAccessService,private route:Router) {
    }
    appASSIGN()
      {
        this.route.navigate(['assignproject']);
      }
      appTask()
      {
        this.route.navigate(['assigntask']);
      }
      getProjectName(projectid: string): string {
        const project = this.projects.find((p) => p.id === projectid);
        return project ? project.name : 'Unknown Project';
      }
      getMemberName(memid: string): string {
        const user = this.users.find((u) => u.id === memid);
        return user ? user.name : 'Unknown Member';
      }

      ngOnInit(): void {
        const LoggedinUserData: Userinfo = JSON.parse(
          sessionStorage.getItem('LoggedinUserData') as string
        );
        const id = LoggedinUserData.id;
      
        // Fetch all projects for the logged-in PM
        this.srv.GetAllProjects().subscribe({
          next: (res) => {
            const projects1 = res as Projectinfo[];
            this.projects = projects1.filter((u) => u.pm === id);
            console.log('Projects fetched successfully:', this.projects);
      
            // Fetch all tasks after projects are loaded
            this.srv.GetAllTasks().subscribe({
              next: (res) => {
                const allTasks = res as Taskinfo[];
                // Filter tasks to exclude those with "Unknown Project"
                this.tasks = allTasks.filter(
                  (task) => this.getProjectName(task.projectid) !== 'Unknown Project'
                );
                console.log('Filtered tasks:', this.tasks);
              },
              error: (err) => {
                console.error('Error fetching tasks:', err);
              },
            });
          },
          error: (err) => {
            console.error('Error fetching projects:', err);
          },
        });
      
        // Fetch all project members
        this.srv.GetAllProjectmembers().subscribe({
          next: (res) => {
            const allProjmem = res as Projectmember[];
            // Filter out project members with "Unknown Project"
            this.projmem = allProjmem.filter(
              (pm) => this.getProjectName(pm.projectid) !== 'Unknown Project'
            );
            console.log('Filtered project members:', this.projmem);
          },
          error: (err) => {
            console.error('Error fetching project members:', err);
          },
        });
      
        // Fetch all users
        this.srv.GetAllUsers().subscribe({
          next: (res) => {
            this.users = res as Userinfo[];
            console.log('Users fetched successfully:', this.users);
          },
          error: (err) => {
            console.error('Error fetching users:', err);
          },
        });
      }

      
      

}
