import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Tasks } from '../tasks';
import { Projectinfo } from '../projectinfo';
import { Comments } from '../comments';
import { UserInfo } from '../user-info';
 
@Component({
  selector: 'app-developer-place',
  templateUrl: './developer-place.component.html',
  styleUrls: ['./developer-place.component.css']
})
export class DeveloperPlaceComponent implements OnInit {
  tasks: Tasks[] = [];
  projects: Projectinfo[] = [];
  comments: Comments[] = [];
  qaUsers: UserInfo[] = [];
  taskForms: { [key: string]: FormGroup } = {}; // Dictionary to store forms for each task
  qaForms: { [key: string]: FormGroup } = {}; // Dictionary to store QA user selection forms
  loggedInUserId: string = '';
 
  constructor(private srv: DbAccessService, private fb: FormBuilder) {}
 
  ngOnInit(): void {
    // Retrieve logged-in user data from sessionStorage
    const LoggedinUserData: UserInfo = JSON.parse(
      sessionStorage.getItem('LoggedinUserData') as string
    );
    this.loggedInUserId = LoggedinUserData.id || '';
 
    // Fetch all tasks assigned to the logged-in developer
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        const allTasks = res as Tasks[];
        this.tasks = allTasks.filter((task) => task.assignedto === this.loggedInUserId);
        console.log('Filtered tasks for developer:', this.tasks);
 
        // Initialize forms for each task
        this.tasks.forEach((task) => {
          if (task.id) {
            this.taskForms[task.id] = this.fb.group({
              status: [task.status]
            });
            this.qaForms[task.id] = this.fb.group({
              qaUser: [''] // Default value for QA user selection
            });
          }
        });
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
 
    // Fetch all QA users
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        const allUsers = res as UserInfo[];
        this.qaUsers = allUsers.filter((user) => user.role === 'QA');
        console.log('QA users fetched successfully:', this.qaUsers);
      },
      error: (err) => {
        console.error('Error fetching QA users:', err);
      }
    });
  }
 
  // Get project name by project ID
  getProjectName(projectid: string): string {
    const project = this.projects.find((p) => p.id === projectid);
    return project ? project.name : 'Unknown Project';
  }
 
  // Get comments for a specific task
  getCommentsForTask(taskid: string): Comments[] {
    return this.comments.filter((comment) => comment.taskid === taskid);
  }
 
  // Update task status
  UpdateTask(task: Tasks): void {
    const updatedStatus = this.taskForms[task.id||''].controls['status'].value;
    const updatedTask = { ...task, status: updatedStatus };
 
    this.srv.UpdateTask(updatedTask).subscribe({
      next: (res) => {
        console.log('Task status updated successfully:', res);
        task.status = updatedStatus; // Update the status locally
      },
      error: (err) => {
        console.error('Error updating task status:', err);
      }
    });
  }
 
  // Assign task to a QA user
  assignTaskToQa(task: Tasks): void {
    const selectedQaId = this.qaForms[task.id||''].controls['qaUser'].value;
    if (!selectedQaId) {
      console.error('No QA user selected for task:', task.id);
      return;
    }
 
    const updatedTask = { ...task, assignedto: selectedQaId, status: 'ACTIVE' };
 
    this.srv.UpdateTask(updatedTask).subscribe({
      next: (res) => {
        console.log('Task assigned to QA successfully:', res);
        task.assignedto = selectedQaId; // Update the assigned user locally
        task.status = 'ACTIVE'; // Update the status locally
      },
      error: (err) => {
        console.error('Error assigning task to QA:', err);
      }
    });
  }
}