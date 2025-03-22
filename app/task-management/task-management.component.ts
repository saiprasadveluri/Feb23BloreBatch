import { Component, OnInit } from '@angular/core';
import { TmaccessService } from '../tmaccess.service';
import { Task } from '../task';
import { Projectmember } from '../projectmember';

@Component({
  selector: 'app-task-management',
  templateUrl: './task-management.component.html',
  styleUrls: ['./task-management.component.css']
})
export class TaskManagementComponent implements OnInit {
  projectId: string | null = null; // Current project ID
  tasks: Task[] = []; // Tasks for the current project
  projectMembers: Projectmember[] = []; // Members assigned to the current project
  newTaskName: string = ''; // Name of the new task
  assignedTo: string = ''; // User ID of the assigned member

  constructor(private srv: TmaccessService) {}

  ngOnInit(): void {
    // Get the project ID from sessionStorage
    this.projectId = sessionStorage.getItem('selectedProjectId');

    if (this.projectId) {
      // Fetch tasks for the current project
      this.srv.getTasksByProject(this.projectId).subscribe({
        next: (tasks: Task[]) => {
          this.tasks = tasks;
        },
        error: (err) => {
          console.error('Error fetching tasks:', err);
        }
      });

      // Fetch members of the current project
      this.srv.getProjectMembers(this.projectId).subscribe({
  next: (members: Projectmember[]) => {
    this.srv.getallusers().subscribe({
      next: (users: any[]) => {
        this.projectMembers = members.map(member => {
          const user = users.find(u => u.id === member.memid);
          return {
            ...member,
            name: user ? user.name : 'Unknown',
            role: user ? user.role : 'Unknown'
          };
        });
        console.log('Mapped Project Members:', this.projectMembers); // Debugging
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  },
  error: (err) => {
    console.error('Error fetching project members:', err);
  }
});
    } else {
      console.error('No project ID found in sessionStorage.');
    }
  }

  private generateRandomId(): string {
    return Math.random().toString(36).substr(2, 9); // Generate a random alphanumeric string
  }

  findMemberNameById(memid: string): string {
    const member = this.projectMembers.find(member => member.memid === memid);
    return member ? member.name : 'Unknown';
  }

  createTask(): void {
    if (this.newTaskName && this.assignedTo) {
      const newTask: Task = {
        id: this.generateRandomId(), // Generate a random task ID
        name: this.newTaskName,
        projectId: this.projectId!,
        assignedTo: this.assignedTo,
        status: 'Pending'
      };

      this.srv.createTask(newTask).subscribe({
        next: (task: Task) => {
          this.tasks.push(task); // Add the new task to the list
          this.newTaskName = ''; // Reset the task name
          this.assignedTo = ''; // Reset the assigned user
        },
        error: (err) => {
          console.error('Error creating task:', err);
        }
      });
    }
  }
}