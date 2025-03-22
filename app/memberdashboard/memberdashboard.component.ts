import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { TmaccessService } from '../tmaccess.service';
import { TaskComment } from '../comment';
import { Userinfo } from '../userinfo';
import { Projectmember } from '../projectmember';
import { Router } from '@angular/router'; // Import Router

@Component({
  selector: 'app-memberdashboard',
  templateUrl: './memberdashboard.component.html',
  styleUrls: ['./memberdashboard.component.css']
})
export class MemberdashboardComponent implements OnInit {
  tasks: Task[] = [];
  Id: string | null = null;
  newComment: string = '';
  selectedTaskForComment: Task | null = null;
  selectedTaskForView: Task | null = null;
  comments: TaskComment[] = [];
  showCommentsModal: boolean = false;
  user: Userinfo | null = null;
  projectMembers: Projectmember[] = []; // To store project members
  developers: Userinfo[] = [];
  qas: Userinfo[] = [];
  selectedTask: Task | null = null;
  selectedAssignee: string | null = null;
  userss: Userinfo[] = [];
  LoggedinUser: Userinfo = JSON.parse(sessionStorage.getItem('LoggedinUser') as string);

  constructor(private srv: TmaccessService, private router: Router) {} // Inject Router

  ngOnInit(): void {
    const loggedInUser = sessionStorage.getItem('LoggedinUser');

    if (loggedInUser) {
      const user = JSON.parse(loggedInUser);
      this.Id = user.id;

      // Fetch all users
      this.srv.getallusers().subscribe({
        next: (users: Userinfo[]) => {
          this.userss = users; // Store all users

          // Filter users based on role (QA or DEV)
          if (this.LoggedinUser.role == 'DEV') {
            this.userss = users.filter(u => u.role == 'QA');
          } else {
            this.userss = users.filter(u => u.role == 'DEV');
          }

          this.user = users.find(u => u.id === this.Id) || null;

          // Find the Projectmember entry for the logged-in user
          this.srv.getProjectMemberByUserId(this.Id!).subscribe({
            next: (projectMember: Projectmember | null) => {
              if (projectMember!=undefined) {
                // Now we have the projectId, fetch the other project members
                this.srv.getProjectMembers(projectMember.projid).subscribe({
                  next: (members: Projectmember[]) => {
                    // Filter project members to only include those with matching memid and projid
                    this.projectMembers = members.filter(member => member.memid === this.Id && member.projid === projectMember.projid);
                    // Load developers and QAs after project members are loaded
                    console.log(projectMember.projid);
                    this.loadDevelopersAndQAs(users, projectMember.projid);
                  },
                  error: (err: any) => {
                    console.error('Error fetching project members:', err);
                  }
                });
              } else {
                console.warn('Project ID not found for the user.');
                // Optionally: Display a message to the user indicating they are not
                // assigned to a project.
              }
            },
            error: (err) => {
              console.error('Error fetching project member entry:', err);
            }
          });

          console.log('User Role:', this.user ? this.user.role : 'No user found');
        },
        error: (err) => {
          console.error('Error fetching users:', err);
        }
      });
    }

    if (this.Id) {
      this.srv.getAllTasks(this.Id!).subscribe({
        next: (T: Task[]) => {
          this.tasks = T.filter(task => task.assignedTo === this.Id);
        },
        error: (err) => {
          console.error('Error fetching tasks:', err);
        }
      });
    }
  }

  loadDevelopersAndQAs(users: Userinfo[], projectId: string): void {
    console.log('loadDevelopersAndQAs called');
    console.log('Users:', users);
    console.log('Project ID:', projectId);
    console.log('Project Members:', this.projectMembers);
  
    // Filter developers and QAs based on project membership
    this.developers = users.filter(user => {
      const isDeveloper = user.role === 'DEV';
      const isProjectMember = this.projectMembers.some(member => member.memid === user.id && member.projid === projectId);
      console.log(`User ${user.name} (ID: ${user.id}, Role: ${user.role}) - isDeveloper: ${isDeveloper}, isProjectMember: ${isProjectMember}`);
      return isDeveloper && isProjectMember;
    });
  
    this.qas = users.filter(user => {
      const isQA = user.role === 'QA';
      const isProjectMember = this.projectMembers.some(member => member.memid === user.id && member.projid === projectId);
      return isQA && isProjectMember;
    });
  
    console.log('Developers:', this.developers);
    console.log('QAs:', this.qas);
  }

  openCommentModal(task: Task): void {
    this.selectedTaskForComment = task;
  }

  Addcomment(): void {
    if (this.newComment && this.selectedTaskForComment) {
      const newComm: TaskComment = {
        id: this.generateRandomId(),
        title: 'Comment for ' + this.selectedTaskForComment.name,
        description: this.newComment,
        TaskId: this.selectedTaskForComment.id,
        CommentedBy: this.Id!
      };

      this.srv.addcomment(newComm).subscribe({
        next: (comment: TaskComment) => {
          console.log('Comment added:', comment);
          this.newComment = '';
          this.selectedTaskForComment = null;
        },
        error: (err) => {
          console.error('Error adding comment:', err);
        }
      });
    } else {
      console.log('Please enter a comment and select a task.');
    }
  }

  viewComments(task: Task): void {
    this.selectedTaskForView = task;
    this.srv.getCommentsByTaskId(task.id).subscribe({
      next: (comments: TaskComment[]) => {
        this.comments = comments;
        this.showCommentsModal = true;
      },
      error: (err) => {
        console.error('Error fetching comments:', err);
        this.comments = [];
      }
    });
  }

  closeComments(): void {
    this.showCommentsModal = false;
    this.comments = [];
    this.selectedTaskForView = null;
  }

  private generateRandomId(): string {
    return Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
  }

  updateTaskStatus(task: Task, newStatus: string): void {
    task.status = newStatus; // Update the task status
    this.srv.updateTask(task).subscribe({ // Call the updateTask method in your service
      next: (updatedTask) => {
        console.log('Task status updated successfully', updatedTask);
        this.router.navigate(['/memberdashboard']); // Redirect to the member dashboard
      },
      error: (err) => {
        console.error('Error updating task status:', err);
        // Handle error as needed
      }
    });
  }

  openAssignmentModal(task: Task): void {
    this.selectedTask = task;
    console.log('Tasks Passed:', task);
    this.selectedAssignee = null;
    // this.loadDevelopersAndQAs(this.userss, task.projectId); // Load developers and QAs
  }

  assignTask(): void {
    if (this.selectedTask && this.selectedAssignee) {
      this.selectedTask.assignedTo = this.selectedAssignee;
      this.srv.updateTask(this.selectedTask).subscribe({
        next: (updatedTask) => {
          console.log('Task assigned successfully', updatedTask);
          this.closeAssignmentModal();
          this.router.navigate(['/memberdashboard']);
        },
        error: (err) => {
          console.error('Error assigning task:', err);
        }
      });
    } else {
      console.log('Please select a task and an assignee.');
    }
  }

  closeAssignmentModal(): void {
    this.selectedTask = null;
    this.selectedAssignee = null;
  }
}