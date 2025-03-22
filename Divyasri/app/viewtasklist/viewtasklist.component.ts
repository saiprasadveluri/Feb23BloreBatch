import { Component } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-viewtasklist',
  templateUrl: './viewtasklist.component.html',
  styleUrls: ['./viewtasklist.component.css']
})
export class ViewtasklistComponent {
  id:string='';
  taskList:Task []=[];
  constructor(private srv:DbAccessService,private router:Router)
  {

  }

ngOnInit(): void {      
  this.RefreshView();
}

RefreshView():void
{
  this.srv. gettaskList().subscribe({
          next:(res)=>{
           var AlltaskList:Task[]=<Task[]>res;
           this.taskList=AlltaskList;
           console.log("updated task list:",this.taskList);
          },
          error:(err)=>{
            console.log(err);
          }
    });
}
}
