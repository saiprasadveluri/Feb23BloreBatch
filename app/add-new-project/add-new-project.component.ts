import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ProjectInfo } from '../project-info';
import { Router } from '@angular/router';
import { TmaccessService } from '../tmaccess.service';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  userInfo: any[] = []; // Array to hold project managers
  projectinfo: any[]=[];
  constructor(private fb: FormBuilder, private srv: TmaccessService, private router: Router) {
    this.frmGroup = fb.group({
      name: new FormControl('', [Validators.required]),
      PM: new FormControl('', [Validators.required]) // Dropdown for project managers
    });
  }

  ngOnInit(): void {
    // Fetch all projects and store them in the projectinfo array
    this.srv.getallprojects().subscribe({
      next: (projects: any[]) => {
        this.projectinfo = projects; // Assign the fetched projects to the projectinfo array
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  
    // Fetch project managers
    this.getProjectManagers();
  }

  getProjectManagers(): void {
    this.srv.getallusers().subscribe({
      next: (users: any[]) => {
        // Filter users with the role of "Project Manager"
        this.userInfo = users.filter(user => user.role === 'PM');
      },
      error: (err) => {
        console.error('Error fetching project managers:', err);
      }
    });
  }
  OnAddClick(): void {
    const uname = this.frmGroup.controls['name'].value;
    const upmId = this.frmGroup.controls['PM'].value; // Get the selected PM ID

    const obj: ProjectInfo = {
      name: uname,
      PM: upmId // Store the PM ID instead of the name
    };

    this.srv.AddNewProject(obj).subscribe({
      next: (res) => {
        console.log('New project added: ' + res.id);
        this.router.navigate(['admindashboard']);
      },
      error: (err) => {
        console.error('Error adding project:', err);
      }
    });
  }
}