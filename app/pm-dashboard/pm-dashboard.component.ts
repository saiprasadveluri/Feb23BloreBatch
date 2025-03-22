import { Component, OnInit } from '@angular/core';
import { DBAccessService } from '../dbaccess.service';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: any[] = []; // Array to store projects assigned to the PM
  projectMembers: any[] = []; // Array to store members of a selected project
  availableUsers: any[] = []; // Array to store available users (Developers and QA)
  tasks: any[] = []; // Array to store tasks of a selected project
  selectedProjectId: string | null = null; // ID of the selected project for assigning members
  selectedMemberId: string | null = null; // ID of the selected member to assign
  selectedTaskAssignee: string | null = null; // ID of the member to assign the task
  taskTitle: string = ''; // Title of the task
  taskDescription: string = ''; // Description of the task
  pmId: string = 'b347'; // Replace with dynamic PM ID (e.g., from localStorage or authentication)

  constructor(private dbService: DBAccessService) {}

  ngOnInit(): void {
    this.loadProjects(); // Fetch projects assigned to the PM
    this.loadAvailableUsers(); // Fetch available users (Developers and QA)
  }

  // Fetch projects assigned to the logged-in PM
  loadProjects(): void {
    this.dbService.GetAllProjects().subscribe({
      next: (data) => {
        this.projects = data.filter((project: { pm: string }) => project.pm === this.pmId);
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  }

  // Fetch members of a specific project
  loadProjectMembers(projectId: string): void {
    this.dbService.GetProjectMembers(projectId).subscribe({
      next: (data) => {
        this.projectMembers = data;
      },
      error: (err) => {
        console.error('Error fetching project members:', err);
      }
    });
  }

  // Fetch available users (Developers and QA)
  loadAvailableUsers(): void {
    this.dbService.GetAllUsers().subscribe({
      next: (data) => {
        this.availableUsers = data.filter((user: { role: string }) => user.role === 'Developer' || user.role === 'QA');
      },
      error: (err) => {
        console.error('Error fetching available users:', err);
      }
    });
  }

  // Select a project for assigning members
  selectProject(projectId: string): void {
    this.selectedProjectId = projectId;
    this.loadProjectMembers(projectId);
    this.loadTasks(projectId); // Load members of the selected project
  }

  removeMember(memberId: string): void {
    if (this.selectedProjectId) {
      this.dbService.RemoveProjectMember(memberId).subscribe({
        next: () => {
          alert('Member removed successfully');
          // Ensure selectedProjectId is not null before calling loadProjectMembers
          if (this.selectedProjectId) {
            this.loadProjectMembers(this.selectedProjectId);
          }
        },
        error: (err) => {
          console.error('Error removing member:', err);
        }
      });
    }
  }
  
  assignMember(): void {
    if (this.selectedProjectId && this.selectedMemberId) {
      const newMember = {
        projId: this.selectedProjectId,
        memId: this.selectedMemberId,
        name: this.availableUsers.find(user => user.id === this.selectedMemberId)?.name,
        role: this.availableUsers.find(user => user.id === this.selectedMemberId)?.role
      };
  
      this.dbService.AssignMemberToProject(newMember).subscribe({
        next: () => {
          alert('Member assigned successfully');
          // Ensure selectedProjectId is not null before calling loadProjectMembers
          if (this.selectedProjectId) {
            this.loadProjectMembers(this.selectedProjectId);
          }
          this.selectedMemberId = null; // Reset the selected member
        },
        error: (err) => {
          console.error('Error assigning member:', err);
        }
      });
    }
  }

  // Create a task and assign it to a member
  createTask(): void {
    if (this.selectedProjectId && this.selectedTaskAssignee && this.taskTitle && this.taskDescription) {
      const newTask = {
        projid: this.selectedProjectId,
        title: this.taskTitle,
        description: this.taskDescription,
        tasktype: 'ACTIVITY', // Default task type
        assignedto: this.selectedTaskAssignee,
        status: 'ACTIVE' // Default status
      };

      this.dbService.CreateTask(newTask).subscribe({
        next: () => {
          alert('Task created successfully');
          this.taskTitle = ''; // Reset task title
          this.taskDescription = ''; // Reset task description
          this.selectedTaskAssignee = null; // Reset task assignee
        },
        error: (err) => {
          console.error('Error creating task:', err);
        }
      });
    }
  }
  loadTasks(projectId: string): void {
    this.dbService.GetTasksByProjectId(projectId).subscribe({
      next: (data) => {
        this.tasks = data; // Populate the tasks array
        console.log('Tasks for project:', projectId, this.tasks); // Debugging
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
  }
}