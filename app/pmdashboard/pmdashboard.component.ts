import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TmaccessService } from '../tmaccess.service';

@Component({
  selector: 'app-pmdashboard',
  templateUrl: './pmdashboard.component.html',
  styleUrls: ['./pmdashboard.component.css']
})
export class PmdashboardComponent implements OnInit {
  projectinfo: any[] = []; // Array to hold projects relevant to the PM
  pmId: string | null = null; // PM ID retrieved from sessionStorage
  selectedProjectId: string | null = null; // Currently selected project ID
  projectMembers: any[] = []; // Array to hold members of the selected project
  userInfo: any[] = []; // Array to hold all user information
  availableUsers: any[] = []; // Array to hold users available for adding to the project
  selectedUserId: string = ''; // ID of the user to be added to the project

  constructor(private srv: TmaccessService, private router: Router) {}

  ngOnInit(): void {
    // Retrieve the logged-in user from sessionStorage
    const loggedInUser = sessionStorage.getItem('LoggedinUser');
    if (loggedInUser) {
      const user = JSON.parse(loggedInUser); // Parse the stored user data
      this.pmId = user.id; // Extract the PM ID from the user object

      // Fetch all projects and filter those relevant to the PM
      this.srv.getallprojects().subscribe({
        next: (projects: any[]) => {
          this.projectinfo = projects.filter(project => project.PM === this.pmId);
        },
        error: (err) => {
          console.error('Error fetching projects:', err);
        }
      });

      // Fetch all users
      this.srv.getallusers().subscribe({
        next: (users: any[]) => {
          this.userInfo = users; // Store all user information
        },
        error: (err) => {
          console.error('Error fetching users:', err);
        }
      });
    } else {
      console.error('No logged-in user found in sessionStorage.');
    }
  }

  loadProjectMembers(projectId: string): void {
    this.selectedProjectId = projectId; // Set the selected project ID
  
    // Ensure userInfo is loaded before mapping project members
    if (this.userInfo.length === 0) {
      console.error('User information is not loaded. Ensure getallusers() is called before this method.');
      return;
    }
  
    this.srv.getProjectMembers(projectId).subscribe({
      next: (members: any[]) => {
        this.projectMembers = members.map(member => {
          const user = this.userInfo.find(user => user.id === member.memid);
          return {
            ...member,
            name: user ? user.name : 'Unknown',
            role: user ? user.role : 'Unknown'
          };
        });
        console.log('Mapped Project Members:', this.projectMembers); // Debugging
        this.filterAvailableUsers(); // Re-filter available users
      },
      error: (err) => {
        console.error('Error fetching project members:', err);
      }
    });
  }

  private filterAvailableUsers(): void {
    this.srv.getAllProjectMembers().subscribe({
      next: (allProjectMembers: any[]) => {
        const assignedUserIds = allProjectMembers.map(member => member.memid); // Get IDs of all assigned members
        this.availableUsers = this.userInfo.filter(
          user => !assignedUserIds.includes(user.id) && (user.role === 'DEV' || user.role === 'QA') // Filter unassigned users with roles DEV or QA
        );
        console.log('Filtered Available Users:', this.availableUsers); // Debugging
      },
      error: (err) => {
        console.error('Error fetching all project members:', err);
      }
    });
  }

  // Add a member to the selected project
  addMemberToProject(): void {
    if (this.selectedProjectId && this.selectedUserId) {
      const newMember = { projid: this.selectedProjectId, memid: this.selectedUserId };
      this.srv.addProjectMember(newMember).subscribe({
        next: () => {
          console.log('Member added successfully');
          if (this.selectedProjectId) {
            this.loadProjectMembers(this.selectedProjectId); // Refresh the members list
          } else {
            console.error('Selected project ID is null.');
          }
          this.selectedUserId = ''; // Reset the selected user
        },
        error: (err) => {
          console.error('Error adding member to project:', err);
        }
      });
    }
  }

  // Navigate to the task management page for a specific project
  navigateToTaskManagement(projectId: string): void {
    sessionStorage.setItem('selectedProjectId', projectId); // Store the project ID in sessionStorage
    this.router.navigate(['taskmanagement', projectId]); // Navigate to the task management page
  }
}