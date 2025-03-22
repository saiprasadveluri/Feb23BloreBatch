import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Userinfo } from '../userinfo';
import { Task } from '../task';
import { Comments } from '../comments'; // Import Comment model
import { ProjectMembers } from '../project-members'; // Import ProjectMembers model

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  tasks: Task[] = [];
  showTasks: boolean = false;
  newComment: string = ''; // New comment text
  projectMembers: Userinfo[] = []; // Project members

  constructor(private srv: DbAccessService) {}

  ngOnInit(): void {
    this.loadProjectMembers();
  }

  loadProjectMembers(): void {
    const loggedInUser: Userinfo = JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
    if (loggedInUser.id) {
      this.srv.GetProjectMembers(loggedInUser.id).subscribe({
        next: (members: ProjectMembers[]) => {
          const memberIds = members.map(member => member.memid);
          this.srv.GetAllUsers().subscribe({
            next: (users: Userinfo[]) => {
              this.projectMembers = users.filter(user => user.id && memberIds.includes(user.id));
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
      console.error('Logged in user ID is undefined');
    }
  }

  viewTasks(): void {
    const loggedInUser: Userinfo = JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
    this.srv.getTasks().subscribe({
      next: (tasks: Task[]) => {
        if (loggedInUser.role === 'DEV') {
          this.tasks = tasks.filter(task => task.assignedto === loggedInUser.id);
        } else if (loggedInUser.role === 'QA') {
          this.tasks = tasks.filter(task => task.assignedto === loggedInUser.id || task.status === 'Resolve this task');
        }
        this.showTasks = true;
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
  }
  addComment(taskId: string): void {
    const loggedInUser: Userinfo = JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
    const newComment: Comments = {
      taskid: taskId,
      commentedby: loggedInUser.id || '',
      description: this.newComment
    };
    this.srv.addComment(newComment).subscribe({
      next: (comment: Comments) => {
        console.log('Comment added:', comment);
        this.newComment = ''; // Clear the comment input
      },
      error: (err) => {
        console.error('Error adding comment:', err);
      }
    });
  }

  updateTaskStatus(taskId: string, status: string, assignedTo: string): void {
    console.log('updateTaskStatus started');
    console.log('Task ID:', taskId);
    console.log('Status:', status);
    console.log('Assigned To:', assignedTo);

    const loggedInUser: Userinfo = JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
    if (loggedInUser.role === 'DEV' && status === 'Resolve this task') {
      const qaMember = this.projectMembers.find(member => member.role === 'QA');
      if (qaMember) {
        assignedTo = qaMember.id!;
      }
    } else if (loggedInUser.role === 'QA' && status === 'Reopen this task') {
      const devMember = this.projectMembers.find(member => member.role === 'DEV');
      if (devMember) {
        assignedTo = devMember.id!;
      }
    }
    this.srv.updateTaskStatus(taskId, status, assignedTo).subscribe({
      next: (task: Task) => {
        console.log('Task status updated:', task);
        // Optionally, update the task in the local tasks array
        const taskToUpdate = this.tasks.find(t => t.id === taskId);
        if (taskToUpdate) {
          taskToUpdate.status = status;
          taskToUpdate.assignedto = assignedTo;
        }
      },
      error: (err) => {
        console.error('Error updating task status:', err);
      }
    });
    console.log('updateTaskStatus completed');
  }
}