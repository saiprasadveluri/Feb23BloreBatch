import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Taskinfo } from '../taskinfo';
import { Projectinfo } from '../projectinfo';
import { Commentinfo } from '../commentinfo';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-qa-place',
  templateUrl: './qa-place.component.html',
  styleUrls: ['./qa-place.component.css']
})
export class QaPlaceComponent implements OnInit {
  tasks: Taskinfo[] = [];
  projects: Projectinfo[] = [];
  comments: Commentinfo[] = [];
  devUsers: Userinfo[] = [];
  qaForms: { [key: string]: FormGroup } = {}; // Dictionary to store developer selection forms
  loggedInUserId: string = '';

  constructor(private srv: DbAccessService, private fb: FormBuilder) {}

  ngOnInit(): void {
    // Retrieve logged-in user data from sessionStorage
    const LoggedinUserData: Userinfo = JSON.parse(
      sessionStorage.getItem('LoggedinUserData') as string
    );
    this.loggedInUserId = LoggedinUserData.id||'';

    // Fetch all tasks assigned to the logged-in QA user
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        const allTasks = res as Taskinfo[];
        this.tasks = allTasks.filter((task) => task.assignedto === this.loggedInUserId);
        console.log('Filtered tasks for QA:', this.tasks);

        // Initialize forms for each task
        this.tasks.forEach((task) => {
          if (task.id) {
            this.qaForms[task.id] = this.fb.group({
              devUser: [''] // Default value for developer selection
            });
          }
        });
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });

    // Fetch all developers
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        const allUsers = res as Userinfo[];
        this.devUsers = allUsers.filter((user) => user.role === 'DEV');
        console.log('Developers fetched successfully:', this.devUsers);
      },
      error: (err) => {
        console.error('Error fetching developers:', err);
      }
    });
  }

  // Get project name by project ID
  getProjectName(projectid: string): string {
    const project = this.projects.find((p) => p.id === projectid);
    return project ? project.name : 'Unknown Project';
  }

  // Get who assigned the task
  getAssignedBy(assignedto: string): string {
    const user = this.devUsers.find((u) => u.id === assignedto) || { name: 'PM' };
    return user.name;
  }

  // Get comments for a specific task
  getCommentsForTask(taskid: string): Commentinfo[] {
    return this.comments.filter((comment) => comment.taskid === taskid);
  }

  // Close a task
  closeTask(task: Taskinfo): void {
    const updatedTask = { ...task, status: 'CLOSED' };

    this.srv.UpdateTask(updatedTask).subscribe({
      next: (res) => {
        console.log('Task closed successfully:', res);
        task.status = 'CLOSED'; // Update the status locally
      },
      error: (err) => {
        console.error('Error closing task:', err);
      }
    });
  }

  // Reassign task to a developer
  reassignTaskToDev(task: Taskinfo): void {
    const selectedDevId = this.qaForms[task.id||''].controls['devUser'].value;
    if (!selectedDevId) {
      console.error('No developer selected for task:', task.id);
      return;
    }

    const updatedTask = { ...task, assignedto: selectedDevId, status: 'ACTIVE' };

    this.srv.UpdateTask(updatedTask).subscribe({
      next: (res) => {
        console.log('Task reassigned to developer successfully:', res);
        task.assignedto = selectedDevId; // Update the assigned user locally
        task.status = 'ACTIVE'; // Update the status locally
      },
      error: (err) => {
        console.error('Error reassigning task to developer:', err);
      }
    });
  }
}