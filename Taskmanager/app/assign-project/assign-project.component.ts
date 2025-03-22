import { Component } from '@angular/core';
import { Userinfo } from '../userinfo';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Projectinfo } from '../projectinfo';
import { Projectmember } from '../projectmember';

@Component({
  selector: 'app-assign-project',
  templateUrl: './assign-project.component.html',
  styleUrls: ['./assign-project.component.css']
})
export class AssignProjectComponent {
  users:Userinfo[] = []; 
   projects: Projectinfo[] = [];
   projm:Projectmember[]=[];
  FrmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private route:Router)
  {
    this.FrmGroup=this.fb.group({
      projectid: new FormControl('', [Validators.required]),
      memid: new FormControl('', [Validators.required])
    });
  }

  loadusers():void{
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
         var users1:Userinfo[]=<Userinfo[]> res;
         this.users=users1.filter((u)=>u.role=='DEV'||u.role=='QA')

        console.log('Users fetched successfully:', this.users);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });

    var LoggedinUserData:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUserData' )as string);
       var id=LoggedinUserData.id;

        this.srv.GetAllProjects().subscribe({
          next: (res) => {
             var projects1 = res as Projectinfo[];
            this.projects=projects1.filter((u)=>u.pm==id);

            console.log('Projects fetched successfully:', this.projects);
          },
          error: (err) => {
            console.error('Error fetching users:', err);
          }
        });
  }
  ngOnInit(): void {
    this.loadusers();
  }
  OnClickassign()
  {
    if (this.FrmGroup.invalid) {
      console.error('Form is invalid');
      return;
    }
  
    const projectid = this.FrmGroup.controls['projectid'].value;
    const memid = this.FrmGroup.controls['memid'].value;
  
    console.log('Form Values:', { projectid, memid });
  
    const newAssignment: Projectmember = {
      projectid: projectid,
      memid: memid,
    };
  
    this.srv.OnClickassign(newAssignment).subscribe({
      next: (res) => {
        console.log('Project assigned successfully:', res);
        this.route.navigate(['pmdashboard']); // Navigate to PM Dashboard
      },
      error: (err) => {
        console.error('Error assigning project:', err);
      },
    });
  }

}
