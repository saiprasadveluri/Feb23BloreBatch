import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { FormControl, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Projectmembers } from '../projectmembers';

@Component({
  selector: 'app-manage-members',
  templateUrl: './manage-members.component.html',
  styleUrls: ['./manage-members.component.css']
})
export class ManageMembersComponent {
  projectId: string = ''; // Selected project ID
  selectedDeveloperId: string = ''; // Selected Developer ID
  selectedQaId: string = ''; // Selected QA ID
  projectList: any[] = []; // List of projects assigned to the PM
  developerList: any[] = []; // List of Developers
  qaList: any[] = []; // List of QA
  frmGroup:FormGroup;
  memberList: any[] = []; 

  constructor(private srv: DbAccessService, private router: Router, private fb: FormBuilder) {

           this.frmGroup = this.fb.group({
            projid:new FormControl('',[Validators.required]),
            memid:new FormControl('',[Validators.required]),
           
           });
  }  
  
  ngOnInit(): void {
    this.loadProjects(); // Load the list of projects
    this.loadAvailableMembers(); // Load the list of available members
  }

  // Load the list of projects
  loadProjects(): void {
    this.srv.getprojectList().subscribe({
      next: (res) => {
        this.projectList = res;
        console.log('Projects:', this.projectList);
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  }

  // Load the list of available members (Developers and QA)
  loadAvailableMembers(): void {
    this.srv.getUsersByRole('DEV').subscribe({
      next: (res) => {
        this.memberList = res; // Assigning the response to the memberList property
        console.log('Available Members:', this.memberList);
      },
      error: (err) => {
        console.error('Error fetching members:', err);
      }
    });
  }
  
  onAddMembers(): void {
    if (this.frmGroup.valid) {
      const projid = this.frmGroup.controls['projid'].value;
      const memid = this.frmGroup.controls['memid'].value;

      const newObj = { projid, memid };

      this.srv.addProjectMember(projid, memid).subscribe({
        next: (res) => {
          console.log('New Member Added:', res);
          this.router.navigate(['pm-dashboard']);
        },
        error: (err) => {
          console.error('Error adding member:', err);
        }
      });
    } else {
      console.error('Form is invalid');
    }
  }
}// Close the constructor block

  // ngOnInit(): void {
  //   this.loadProjects(); // Load projects assigned to the PM
  //   this.loadAvailableUsers(); // Load available users (Developers and QA)
  // }

  // Load projects assigned to the logged-in PM
  // loadProjects(): void {
  //   const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
  //   const pmId = loggedInUser.id; // Get the logged-in PM's ID

  //   this.srv.getProjectsForPM(pmId).subscribe({
  //     next: (res) => {
  //       this.projectList = res; // Assign the response to projectList
  //       console.log('Projects assigned to PM:', this.projectList);
  //     },
  //     error: (err) => {
  //       console.error('Error fetching projects:', err);
  //     }
  //   });
  // }

  // // Load available users (Developers and QA)
  // loadAvailableUsers(): void {
  //   this.srv.getUsersByRole().subscribe({
  //     next: (res) => {
  //       // Separate users into Developers and QA
  //       this.developerList = res.filter((user: any) => user.role === 'DEV'); // Only Developers
  //       this.qaList = res.filter((user: any) => user.role === 'QA'); // Only QA
  //       console.log('Developers:', this.developerList);
  //       console.log('QA:', this.qaList);
  //     },
  //     error: (err) => {
  //       console.error('Error fetching available users:', err);
  //     }
  //   });
  // }

  //Add selected members (Developer and/or QA) to the selected project
//   onAddMembers(): void {
//     if (!this.projectId) {
//     var  projid = this.frmGroup.controls[' projid'].value;
//         var  memid = this.frmGroup.controls[' memid'].value;
      
//         const newObj: Projectmembers = {
//           projid: projid,
//           memid: memid
//         };
      
//         this.srv.addProjectMember(projid, memid).subscribe({
//           next: (res ) => {
//             console.log("New Task Added: " + res.id);
//             this.router.navigate(['pm-dashboard']);
//           },
//           error: (err) => {
//             console.error("Error adding task:", err);
//           }
//         });
//       }
// }
// }
// Add selected members (Developer and/or QA) to the selected project
// onAddMembers(): void {
//   const projid = this.frmGroup.controls['projid'].value; // Get the selected project ID
//   const developerId = this.selectedDeveloperId; // Get the selected Developer ID
//   const qaId = this.selectedQaId; // Get the selected QA ID

//   if (!projid) {
//     console.error('No project selected');
//     return;
//   }

//   // Add Developer if selected
//   if (developerId) {
//     this.srv.addProjectMember(projid, developerId).subscribe({
//       next: () => {
//         console.log(`Developer (ID: ${developerId}) added to project (ID: ${projid}) successfully`);
//       },
//       error: (err) => {
//         console.error('Error adding developer to project:', err);
//       }
//     });
//   }

//   // Add QA if selected
//   if (qaId) {
//     this.srv.addProjectMember(projid, qaId).subscribe({
//       next: () => {
//         console.log(`QA (ID: ${qaId}) added to project (ID: ${projid}) successfully`);
//       },
//       error: (err) => {
//         console.error('Error adding QA to project:', err);
//       }
//     });
//   }

//   // Navigate back to the PM dashboard after adding members
//   this.router.navigate(['pm-dashboard']);
// }
// }