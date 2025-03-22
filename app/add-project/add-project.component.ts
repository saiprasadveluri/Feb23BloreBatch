// import { Component, OnInit } from '@angular/core';
//  import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
//  import { DbAccessService } from '../db-access.service';
//  import { ProjectInfo } from '../project-info';
//  import { UserInfo } from '../user-info';
  
//  @Component({
//    selector: 'app-add-project',
//    templateUrl: './add-project.component.html',
//    styleUrls: ['./add-project.component.css']
//  })
//  export class AddProjectComponent implements OnInit {
//    frmGroup: FormGroup;
//    pms: UserInfo[] = [];
  
//    constructor(private fb: FormBuilder, private dbService: DbAccessService) {
//      this.frmGroup = fb.group({
//        name: new FormControl('', [Validators.required]),
//        // description: new FormControl('', [Validators.required]),
//        pm: new FormControl('', [Validators.required])
//      });
//    }
  
//    ngOnInit() {
//      this.dbService.GetAllPMs().subscribe((data: UserInfo[]) => {
//        this.pms = data;
//      });
//    }
  
//    OnAddProjectClick() {
//      const project: ProjectInfo = {
//        name: this.frmGroup.controls['name'].value,
//        // description: this.frmGroup.controls['description'].value,
//        pm: this.frmGroup.controls['pm'].value
//      };
  
//      this.dbService.AddNewProject(project).subscribe({
//        next: (res) => {
//          alert('Project added successfully!');
//        },
//        error: (err) => {
//          console.error('Error adding project:', err);
//        }
//      });
//    }
//  }

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent implements OnInit {
  frmGroup: FormGroup;
  pms: UserInfo[] = [];

  constructor(private fb: FormBuilder, private dbService: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      projectName: ['', [Validators.required]],
      pmId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.dbService.getAllPMs().subscribe((data: UserInfo[]) => {
      this.pms = data;
    });
  }

  onAddProjectClick(): void {
    const projectName = this.frmGroup.controls['projectName'].value;
    const pmId = this.frmGroup.controls['pmId'].value;

    const newProject: ProjectInfo = {
      id: '', // Assign a proper id value here
      name: projectName,
      pmId: pmId,
      description: ''
    };

    this.dbService.addProject(newProject).subscribe({
      next: (res: any) => {
        console.log('Project added successfully');
        this.router.navigate(['admindashboard']); // Navigate back to Admin Dashboard
      },
      error: (error: any) => {
        console.error('Error adding project', error);
      }
    });
  }
}