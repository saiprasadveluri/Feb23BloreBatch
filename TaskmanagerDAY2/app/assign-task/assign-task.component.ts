// import { Component } from '@angular/core';
// import { Projectinfo } from '../projectinfo';
// import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
// import { DbAccessService } from '../db-access.service';
// import { Router } from '@angular/router';
// import { Userinfo } from '../userinfo';
// import { Projectmember } from '../projectmember';

// @Component({
//   selector: 'app-assign-task',
//   templateUrl: './assign-task.component.html',
//   styleUrls: ['./assign-task.component.css']
// })
// export class AssignTaskComponent {
//       users:Userinfo[] = []; 
//         projects: Projectinfo[] = [];
//         projects2:Projectinfo[]=[];
//         projm:Projectmember[]=[];
//      FrmGroup:FormGroup;
//       constructor(private fb:FormBuilder,private srv:DbAccessService,private route:Router)
//        {
//          this.FrmGroup=this.fb.group({
//            projectid: new FormControl('', [Validators.required]),
//            title: new FormControl('', [Validators.required]),
//            description: new FormControl('', [Validators.required]),
//            tasktype: new FormControl('', [Validators.required]),
//            assignedto: new FormControl('', [Validators.required]),
//            status: new FormControl('', [Validators.required]),
//          });
//        }
//        getprojusers():void
//        {
//         const pid=this.FrmGroup.controls['projectid'].value;
        
        
//        }
//        Addtask(): void {
//         if (this.FrmGroup.invalid) {
//           console.error('Form is invalid');
//           return;
//         }
      
//         const projectid = this.FrmGroup.controls['projectid'].value;
//         const title = this.FrmGroup.controls['title'].value;
//         const description = this.FrmGroup.controls['description'].value;
//         const tasktype = this.FrmGroup.controls['tasktype'].value;
//         const assignedto = this.FrmGroup.controls['assignedto'].value;
//         const status = this.FrmGroup.controls['status'].value;
      
//         const newObj = {
//           projectid: projectid,
//           title: title,
//           description: description,
//           tasktype: tasktype,
//           assignedto: assignedto,
//           status: status,
//         };
      
//         this.srv.Addtask(newObj).subscribe({
//           next: (res) => {
//             console.log('Task added successfully:', res);
//             this.route.navigate(['pmdashboard']); // Navigate to PM Dashboard
//           },
//           error: (err) => {
//             console.error('Failed to add task:', err);
//           },
//         });
//       }

//        ngOnInit(): void {
//         this.srv.GetAllUsers().subscribe({
//           next: (res) => {
//              var users1:Userinfo[]=<Userinfo[]> res;
//              this.users=users1.filter((u)=>u.role=='DEV'||u.role=='QA')
    
//             console.log('Users fetched successfully:', this.users);
//           },
//           error: (err) => {
//             console.error('Error fetching users:', err);
//           }
//         });
    
//         var LoggedinUserData:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUserData' )as string);
//            var id=LoggedinUserData.id;
    
//             this.srv.GetAllProjects().subscribe({
//               next: (res) => {
//                  var projects1 = res as Projectinfo[];
//                 this.projects=projects1.filter((u)=>u.pm==id);
    
//                 console.log('Projects fetched successfully:', this.projects);
//               },
//               error: (err) => {
//                 console.error('Error fetching users:', err);
//               }
//             }); 
            
//        }
// }

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Userinfo } from '../userinfo';
import { Projectinfo } from '../projectinfo';
import { Projectmember } from '../projectmember';

@Component({
  selector: 'app-assign-task',
  templateUrl: './assign-task.component.html',
  styleUrls: ['./assign-task.component.css']
})
export class AssignTaskComponent implements OnInit {
  users: Userinfo[] = [];
  projects: Projectinfo[] = [];
  projm: Projectmember[] = [];
  filteredUsers: Userinfo[] = []; // Users filtered by selected project
  FrmGroup: FormGroup;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private route: Router) {
    this.FrmGroup = this.fb.group({
      projectid: new FormControl('', [Validators.required]),
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      tasktype: new FormControl('', [Validators.required]),
      assignedto: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    // Fetch all users
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.users = res as Userinfo[];
        console.log('Users fetched successfully:', this.users);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });

    // Fetch all projects
    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        this.projects = res as Projectinfo[];
        console.log('Projects fetched successfully:', this.projects);
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });

    // Fetch all project members
    this.srv.GetAllProjectmembers().subscribe({
      next: (res) => {
        this.projm = res as Projectmember[];
        console.log('Project members fetched successfully:', this.projm);
      },
      error: (err) => {
        console.error('Error fetching project members:', err);
      }
    });
  }

  // Filter users based on selected project
    getprojusers(): void {
    const selectedProjectId = this.FrmGroup.controls['projectid'].value;
    console.log('Selected Project ID:', selectedProjectId);
    console.log('Project Members:', this.projm);
  
    const assignedMemberIds = this.projm
      .filter((pm) => pm.projectid === selectedProjectId)
      .map((pm) => pm.memid);
  
    console.log('Assigned Member IDs:', assignedMemberIds);
  
    this.filteredUsers = this.users.filter((user) => user.id && assignedMemberIds.includes(user.id));
    console.log('Filtered users for project:', this.filteredUsers);
  }

  Addtask(): void {
    if (this.FrmGroup.invalid) {
      console.error('Form is invalid');
      return;
    }

    const projectid = this.FrmGroup.controls['projectid'].value;
    const title = this.FrmGroup.controls['title'].value;
    const description = this.FrmGroup.controls['description'].value;
    const tasktype = this.FrmGroup.controls['tasktype'].value;
    const assignedto = this.FrmGroup.controls['assignedto'].value;
    const status = this.FrmGroup.controls['status'].value;

    const newObj = {
      projectid: projectid,
      title: title,
      description: description,
      tasktype: tasktype,
      assignedto: assignedto,
      status: status,
    };

    this.srv.Addtask(newObj).subscribe({
      next: (res) => {
        console.log('Task added successfully:', res);
        this.route.navigate(['pmdashboard']); // Navigate to PM Dashboard
      },
      error: (err) => {
        console.error('Failed to add task:', err);
      },
    });
  }
}