import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectMember } from '../project-member';
import { Task } from '../task';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-project-dashboard',
  templateUrl: './project-dashboard.component.html',
  styleUrls: ['./project-dashboard.component.css']
})
export class ProjectDashboardComponent implements OnInit {
  projectId: string = '';
  members: ProjectMember[] = []; 
  availableUsers: { id: string | undefined; name: string; role: "Project Manager" | "QA" | "Developer"; }[] = []; // All users who can be assigned
  tasks: Task[] = []; 

  selectedMemberId: string = '';
  taskTitle: string = '';
  taskDescription: string = '';
  assignedMemberId: string = '';
  //status:string='';
  taskStatus: any;


  newMemberName: string = ''; 
  newMemberRole: 'Developer' | 'QA' = 'Developer'; 

  constructor(private route: ActivatedRoute, private dbService: DbAccessService) {}

  ngOnInit() {
    this.projectId = this.route.snapshot.paramMap.get('projectId') || ''; 
    this.loadMembers(); 
    this.loadTasks(); 
    this.loadAvailableUsers(); 
  }


  loadMembers() {
    this.dbService.getProjectMembers(this.projectId).subscribe(
      (data) => {
        this.members = data;
      },
      (error) => {
        console.error('Error fetching members:', error);
      }
    );
  }

  /*loadAvailableUsers() {
    this.dbService.GetAllUsers().subscribe(
      (data: UserInfo[]) => {
        this.availableUsers = data
          .filter(user => user.id !== undefined) 
          .map(user => ({
            id: user.id ?? '', 
            name: user.name,
            role: this.convertRole(user.role)
          }));
      },
      (error) => {
        console.error('Error fetching available users:', error);
      }
    );
  }*/

    loadAvailableUsers() {
      this.dbService.GetAllUsers().subscribe(
        (users) => {
         
          this.availableUsers = users.filter((user: { role: string; }) => 
            user.role.toUpperCase() === 'DEVELOPER' || user.role.toUpperCase() === 'QA'
          );
        },
        (error) => {
          console.error('Error fetching available users:', error);
        }
      );
    }
    
  

 
  convertRole(role: string): 'QA' | 'Developer' | 'Project Manager' {
    switch (role.toLowerCase()) {
      case 'qa':
        return 'QA';
      case 'developer':
        return 'Developer';
      case 'project manager':
        return 'Project Manager';
      default:
        throw new Error(`Invalid role: ${role}`);
    }
  }


  assignMember() {
    const selectedUser = this.availableUsers.find(user => user.id === this.selectedMemberId);
    if (selectedUser) {
   
      if (selectedUser.role.toUpperCase() === 'DEVELOPER' || selectedUser.role.toUpperCase() === 'QA') {
        const newMember: ProjectMember = {
          id: selectedUser.id,
          name: selectedUser.name,
          role: selectedUser.role
        };
  
        this.dbService.assignMemberToProject(this.projectId, newMember).subscribe(
          () => {
            console.log('Member assigned successfully.');
            this.loadMembers(); 
          },
          (error) => {
            console.error('Error assigning member:', error);
          }
        );
      } else {
        console.error('Only Developers or QA can be added to the project.');
      }
    } else {
      console.error('No valid user selected for assignment.');
    }
  }
  


  removeMember(memberId: string) {
    this.dbService.removeMemberFromProject(this.projectId, memberId).subscribe(
      () => {
        console.log('Member removed successfully.');
        this.loadMembers(); 
      },
      (error) => {
        console.error('Error removing member:', error);
      }
    );
  }
  


  



  addMember() {
    if (this.newMemberName.trim() && this.newMemberRole) {
      const newMember: ProjectMember = {
        id: new Date().getTime().toString(), 
        name: this.newMemberName.trim(), 
        role: this.newMemberRole
      };
  
      this.dbService.assignMemberToProject(this.projectId, newMember).subscribe(
        () => {
          console.log('Member added successfully');
          this.loadMembers(); 
    
          this.newMemberName = '';
          this.newMemberRole = 'Developer';
        },
        (error) => {
          console.error('Error adding member:', error);
        }
      );
    } else {
      console.error('Member name or role is invalid.');
    }
  }
  

  loadTasks() {
    this.dbService.getTasksForProject(this.projectId).subscribe(
      (data) => {
        console.log('Fetched Tasks:', data); 
        this.tasks = data;
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }

  createTask() {
    const assignedMember = this.members.find(member => member.id === this.assignedMemberId);
    if (assignedMember) {
    
      if (assignedMember.role.toUpperCase() === 'DEVELOPER' || assignedMember.role.toUpperCase() === 'QA') {
        const newTask: Task = {
          id: new Date().getTime().toString(),
          title: this.taskTitle,
          description: this.taskDescription,
          assignedTo: assignedMember,
          status: 'Pending'
        };
  
        this.dbService.createTask(this.projectId, newTask).subscribe(
          () => {
            console.log('Task created successfully.');
            this.loadTasks();
          },
          (error) => {
            console.error('Error creating task:', error);
          }
        );
      } else {
        console.error('Tasks can only be assigned to Developers or QA.');
      }
    } else {
      console.error('No valid member selected for task assignment.');
    }
  }
  

  updateTaskStatus(taskId: string, newStatus: 'In Progress' | 'Completed') {
    const taskToUpdate = this.tasks.find(task => task.id === taskId);
    if (taskToUpdate) {
      taskToUpdate.status = newStatus; 
      console.log(`Task ID ${taskId} status updated to ${newStatus}`);
    }
  }

  deleteTask(taskId: string) {
    this.dbService.deleteTask(taskId).subscribe(
      () => {
        console.log('Task deleted successfully');
        this.loadTasks(); 
      },
      (error) => {
        console.error('Error deleting task:', error);
      }
    );
  }
  
  
}
