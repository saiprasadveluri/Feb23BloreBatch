import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

interface Tasks {
  id?: string;
  projid: string;
  title: string;
  description: string;
  tasktype: string;
  assignedto: string;
  status: string;
}

interface TaskComment {
  id: string;
  taskid: string;
  commentedby: string;
  description: string;
}
interface UserInfo{
  id?:string;
  name:string;
  email:string;
  password:string;
  role:string;
}

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  statusForm!: FormGroup;
  tasks: Tasks[] = [];
  comments: TaskComment[] = []
  user:UserInfo[]=[]
  memberRoles: Map<string, string> = new Map();
  userId: string | null = null;
  selectedTask: Tasks | null = null; 
  commentForm!: FormGroup; 
  activeSection: string | null = null; // Tracks the active section
  userRole: string | null = null; // Tracks the user's role
  projectMembers: { memid: string; name: string }[] = []; // Stores project members
  assignableMembers: { memid: string; name: string }[] = []; // Stores filtered members for assignment
  selectedAssignee: string | null = null; // Tracks the selected user for assignment
  assignForm: FormGroup; // Form for task assignment

  constructor(
    private fb: FormBuilder,
    private srv: DbAccessService,
    private router: Router
  ) {
    this.commentForm = this.fb.group({
      description: ['', Validators.required],
    });

    this.statusForm = this.fb.group({
      status: ['', Validators.required],
    });

    this.assignForm = this.fb.group({
      assignee: [''], // Form control for selected assignee
    });
  }

  ngOnInit(): void {
    this.getLoggedInUserId();
    this.getTasks();
    this.initializeCommentForm();
    this.initializeStatusForm();
    this.getUserRole(); // Fetch the user's role
    this.getProjectMembers(); // Fetch project members
  
    // Fetch roles and filter assignable members
    this.getRolesForMembers();
  }
  initializeStatusForm(): void {
    this.statusForm = this.fb.group({
      status: ['', [Validators.required, Validators.minLength(3)]]
    });
  }
  getLoggedInUserId(): void {
    const sessionData = sessionStorage.getItem('LoggedinUserData');
    if (sessionData) {
      const userData = JSON.parse(sessionData);
      this.userId = userData?.id || null;
      console.log('Logged-in User ID:', this.userId);
    } else {
      console.warn('No valid session data found for the logged-in user');
    }
  }

  getTasks(): void {
    if (this.userId) {
      this.srv.GetTasks().subscribe({
        next: (res: Tasks[]) => {
          this.tasks = res.filter(task => task.assignedto === this.userId);
          console.log('Tasks assigned to the user:', this.tasks);
        },
        error: (err) => {
          console.error('Error fetching tasks:', err);
        }
      });
    } else {
      console.warn('User ID is not available. Cannot fetch tasks.');
    }
  }

  initializeCommentForm(): void {
    this.commentForm = this.fb.group({
      description: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  addComment(task: Tasks): void {
    console.log('Comment button clicked for task:', task);
    this.selectedTask = task; 
    this.activeSection = 'addComment';
  }

  submitComment(): void {
    if (this.selectedTask && this.userId && this.commentForm.valid) {
      const newComment: TaskComment = {
        id: this.generateUniqueId(), 
        taskid: this.selectedTask.id || '', 
        commentedby: this.userId, 
        description: this.commentForm.value.description 
      };

      console.log('Submitting comment:', newComment);

      
      this.srv.AddComment(newComment).subscribe({
        next: (response) => {
          console.log('Comment added successfully:', response);
          alert('Comment added successfully!');
          this.selectedTask = null; 
          this.commentForm.reset(); 
          this.activeSection = null;
        },
        error: (err) => {
          console.error('Error adding comment:', err);
        }
      });
    } else {
      console.warn('Invalid comment submission');
    }
  }

  cancelComment(): void {
    this.selectedTask = null; 
    this.commentForm.reset(); 
    this.activeSection = null;
  }

  generateUniqueId(): string {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let uniqueId = '';
    for (let i = 0; i < 8; i++) {
      uniqueId += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return uniqueId;
  }
  updateTaskStatus(task: Tasks): void {
    console.log('Update Status button clicked for task:', task);
    this.selectedTask = task; 
    this.statusForm.reset(); 
    this.activeSection = 'updateStatus';
  }
  
  submitStatusUpdate(): void {
    if (this.selectedTask && this.statusForm.valid) {
      const newStatus = this.statusForm.value.status;
  
      this.srv.UpdateTaskStatus(this.selectedTask.id || '', newStatus).subscribe({
        next: (response) => {
          console.log('Task status updated successfully:', response);
          alert('Task status updated successfully!');
          this.selectedTask!.status = newStatus; 
          this.selectedTask = null; 
          this.statusForm.reset(); 
          this.activeSection = null;
        },
        error: (err) => {
          console.error('Error updating task status:', err);
        }
      });
    } else {
      console.warn('Invalid status update attempt');
    }
  }
  
  cancelStatusUpdate(): void {
    this.selectedTask = null; 
    this.statusForm.reset(); 
    this.activeSection = null;
  }
viewComments(task: Tasks): void {
  console.log('View Comments button clicked for task:', task);
  this.selectedTask = task; 
  this.activeSection = 'viewComments';
  this.srv.GetCommentsByTaskId(task.id || '').subscribe({
    next: (res: TaskComment[]) => {
      this.comments = res; 
      console.log('Comments for task:', this.comments);
    },
    error: (err) => {
      console.error('Error fetching comments:', err);
    }
  });
}

closeComments(): void {
  this.selectedTask = null; 
  this.comments = []; 
  this.activeSection = null;
}

getUserRole(): void {
  const sessionData = sessionStorage.getItem('LoggedinUserData');
  if (sessionData) {
    const userData = JSON.parse(sessionData);
    this.userRole = userData?.role || null; // Assuming role is stored in session data
    console.log('Logged-in User Role:', this.userRole);
  } else {
    console.warn('No valid session data found for the logged-in user');
  }
}

resolveTask(task: Tasks): void {
  if (task) {
    console.log('Resolve button clicked for task:', task);
    this.srv.UpdateTaskStatus(task.id || '', 'Resolved').subscribe({
      next: (response) => {
        console.log('Task resolved successfully:', response);
        alert('Task resolved successfully!');
        task.status = 'Resolved'; // Update the task's status locally
      },
      error: (err) => {
        console.error('Error resolving task:', err);
      }
    });
  }
}
reopenTask(task: Tasks): void {
  if (task) {
    console.log('Reopen button clicked for task:', task);
    this.srv.UpdateTaskStatus(task.id || '', 'Reopened').subscribe({
      next: (response) => {
        console.log('Task reopened successfully:', response);
        alert('Task reopened successfully!');
        task.status = 'Reopened'; // Update the task's status locally
      },
      error: (err) => {
        console.error('Error reopening task:', err);
      }
    });
  }
}
getProjectMembers(): void {
  this.srv.GetprojectMembers().subscribe({
    next: (members: { memid: string; name: string }[]) => {
      this.projectMembers = members;
      console.log('Project Members:', this.projectMembers);
      //this.fetchRolesForMembers();
    },
    error: (err) => {
      console.error('Error fetching project members:', err);
    },
  });
}
getRolesForMembers(): void {
  if (this.projectMembers && this.projectMembers.length > 0) {
    const memIds = this.projectMembers.map(member => member.memid); // Extract memids

    this.srv.GetUserinfoRoles(memIds).subscribe({
      next: (userinfo: { id: string; role: string }[]) => {
        // Create a map of memid to role
        this.memberRoles = new Map<string, string>();
        userinfo.forEach(user => {
          this.memberRoles.set(user.id, user.role);
        });

        console.log('Mapped Roles:', this.memberRoles);
      },
      error: (err) => {
        console.error('Error fetching roles for members:', err);
      }
    });
  } else {
    console.warn('No project members available to fetch roles.');
  }
}
filterAssignableMembers(): void {
  if (!this.memberRoles || !this.userRole) {
    console.warn('Member roles or user role is not available.');
    return;
  }

  // Filter members based on the logged-in user's role
  if (this.userRole === 'QA') {
    // If logged-in user is QA, show members with the role 'Dev'
    this.assignableMembers = Array.from(this.memberRoles.entries())
      .filter(([memid, role]) => role === 'Dev')
      .map(([memid, role]) => ({
        memid,
        name: this.projectMembers.find((member) => member.memid === memid)?.name || 'Unknown',
      }));
  } else if (this.userRole === 'Dev') {
    // If logged-in user is Dev, show members with the role 'QA'
    this.assignableMembers = Array.from(this.memberRoles.entries())
      .filter(([memid, role]) => role === 'QA')
      .map(([memid, role]) => ({
        memid,
        name: this.projectMembers.find((member) => member.memid === memid)?.name || 'Unknown',
      }));
  }

  console.log('Filtered Assignable Members:', this.assignableMembers);
}

assignTask(task: Tasks): void {
  const selectedAssignee = this.assignForm.get('assignee')?.value;

  if (selectedAssignee) {
    console.log(`Assigning task ${task.title} to user ${selectedAssignee}`);
    this.srv.AssignTask(task.id || '', selectedAssignee).subscribe({
      next: (response) => {
        console.log('Task assigned successfully:', response);
        alert('Task assigned successfully!');
        this.getTasks(); // Refresh tasks
      },
      error: (err) => {
        console.error('Error assigning task:', err);
      },
    });

    // Remove logged-in user from the project
    this.srv.RemoveUserFromProject(this.userId || '').subscribe({
      next: (response) => {
        console.log('Logged-in user removed from project:', response);
        alert('You have been removed from the project.');
        this.router.navigate(['/dashboard']); // Redirect to dashboard
      },
      error: (err) => {
        console.error('Error removing user from project:', err);
      },
    });
  } else {
    alert('Please select a user to assign the task.');
  }
}
}
