import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-member-task-list',
  standalone: false,
  templateUrl: './member-task-list.component.html',
  styleUrl: './member-task-list.component.css'
})
export class MemberTaskListComponent implements OnInit {

  tasklist:Task[]=[];
  
  constructor(private srv:DbAccessService){
    
  }

  ngOnInit(): void {
    this.RefreshView();
  }

  RefreshView(){
    var curUser=<UserInfo>JSON.parse(sessionStorage.getItem('LoggedInUserData')||'{}');
    this.srv.GetAllTasks().subscribe({
      next:(res)=>{
        var alltasklist=<Task[]>res;
        this.tasklist=alltasklist.filter((task)=>task.assignedTo==curUser.id);
      }
    });
  }
}
