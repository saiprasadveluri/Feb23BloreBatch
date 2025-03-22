
// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
// import { DbAccessService } from '../db-access.service';
// import { ActivatedRoute, Router } from '@angular/router';

// interface ProjectMembers  
//  {
// "id": string,
// "project": string,
// "memid": string
// }

// @Component({
//   selector: 'app-manage-tasks',
//   templateUrl: './manage-tasks.component.html',
//   styleUrls: ['./manage-tasks.component.css']
// })

// export class ManageTasksComponent implements OnInit {
//   frmGroup: FormGroup;
//   project: { id: string; name: string } | null = null;
//   mem:ProjectMembers[]=[];

//   constructor(private fb: FormBuilder, private srv: DbAccessService, private route: ActivatedRoute,private router:Router) {
//     // Define the form structure
//     {
//       // Define the form structure and include project and projid without input
//       this.frmGroup = this.fb.group({
//         projid: new FormControl({ value: '', disabled: true }, [Validators.required]), // Preloaded but disabled
//         project: new FormControl({ value: '', disabled: true }), // Preloaded but disabled
//         title: new FormControl('', [Validators.required]),
//         description: new FormControl('', [Validators.required]),
//         tasktype: new FormControl('', [Validators.required]),
//         assignedto: new FormControl('', [Validators.required]),
//         status: new FormControl('', [Validators.required]),
//       });
//     }
//   }
//     // ngOnInit() {
//     //   this.project = this.srv.getProjectData();
//     //   console.log('Project Data:', this.project);
//     //   this.members();
//     // }
//     ngOnInit(): void {
//       // Fetch project details
//       this.project = this.srv.getProjectData();
//       console.log('Project Data:', this.project);
    
//       // Ensure projid is set correctly in the form
//       if (this.project && this.project.id) {
//         this.frmGroup.patchValue({ projid: this.project.id });
//       }
    
//       // Populate the members dropdown
//       this.members();
//     }

//   onSubmit(): void {
//     if (this.frmGroup.valid) {
//       console.log('Form Data:', this.frmGroup.value);
//       // Submit the form data (example: sending it to the database)
//       this.srv.SubmitTask(this.frmGroup.value).subscribe({
//         next: (response) => {
//           console.log('Task submitted successfully:', response);
//         },
//         error: (err) => {
//           console.error('Error submitting task:', err);
//         }
//       });
//     } else {
//       console.warn('Form is invalid');
//     }
//     console.log("task submitted succesfully");
//     this.router.navigate(['/pmdashboard']);
//   }
//   members() {
//     if (this.project && this.project.id) {
//       this.srv.GetprojectMembers().subscribe({
//         next: (res: ProjectMembers[]) => {
//           // Filter members based on the project ID
//           this.mem = res.filter(member => member.project === this.project!.id);
//         },
//         error: (err) => {
//           console.error('Error fetching project members:', err);
//         }
//       });
//     } else {
//       console.warn('No project data available to filter members');
//       this.mem = []; // Clear the members list if no project is available
//     }
//   }

//}
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manage-tasks',
  templateUrl: './manage-tasks.component.html',
  styleUrls: ['./manage-tasks.component.css']
})
export class ManageTasksComponent implements OnInit {
  frmGroup!: FormGroup;
  project: { id: string; name: string } | null = null; 
  mem: { memid: string }[] = []; 

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {}

  ngOnInit(): void {

    this.project = this.srv.getProjectData();
    console.log('Project Data:', this.project);

    
    this.frmGroup = this.fb.group({
      projid: [this.project?.id || '', Validators.required], 
      title: ['', Validators.required],
      description: ['', Validators.required],
      tasktype: ['', Validators.required],
      assignedto: ['', Validators.required],
      status: ['', Validators.required]
    });

    
    this.members();
  }

  members(): void {
    if (this.project && this.project.id) {
      this.srv.GetprojectMembers().subscribe({
        next: (res: { project: string; memid: string }[]) => {
          
          this.mem = res.filter(member => member.project === this.project!.id);
          console.log('Filtered Members:', this.mem);
        },
        error: (err) => {
          console.error('Error fetching project members:', err);
        }
      });
    } else {
      console.warn('No project data available to filter members');
      this.mem = []; 
    }
  }

  onSubmit(): void {
    if (this.frmGroup.valid) {
      console.log('Form Data:', this.frmGroup.value);
      this.srv.SubmitTask(this.frmGroup.value).subscribe({
        next: (response) => {
          console.log('Task submitted successfully:', response);
          this.router.navigate(['/pmdashboard']); 
        },
        error: (err) => {
          console.error('Error submitting task:', err);
        }
      });
    } else {
      console.warn('Form is invalid');
    }
  }
}