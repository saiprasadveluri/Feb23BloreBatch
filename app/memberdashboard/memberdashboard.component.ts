import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { TmaccessService } from '../tmaccess.service';

@Component({
  selector: 'app-memberdashboard',
  templateUrl: './memberdashboard.component.html',
  styleUrls: ['./memberdashboard.component.css']
})
export class MemberdashboardComponent implements OnInit{
    tasks: Task[]=[];
    Id: string | null = null ;
    constructor(private srv:TmaccessService) {}


    ngOnInit(): void {
      const loggedInUser = sessionStorage.getItem('LoggedinUser');
      if (loggedInUser) {
        const user = JSON.parse(loggedInUser);
        this.Id = user.id;
      }
      if (this.Id) {
        this.srv.getAllTasks(this.Id!).subscribe({ // Use the non-null assertion operator
          next: (T: any[]) => {
            this.tasks = T.filter(task => task.assignedTo === this.Id); // Corrected filtering
          },
          error: (err) => {
            console.error('Error fetching tasks:', err);
          }
        });
      }
    }
}
