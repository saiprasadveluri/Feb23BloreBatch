import { Component } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project.info'; // Assuming this is the new interface for project details
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
  frmGroup: FormGroup;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = fb.group({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required]),
    });
  }

  OnAddClick() {
    const projectId = this.frmGroup.controls['id'].value;
    const projectName = this.frmGroup.controls['name'].value;
    const managerName = this.frmGroup.controls['pm'].value;

    const newProject: ProjectInfo = {
      id: projectId,
      name: projectName,
      pm: managerName,
    };

    this.srv.AddNewProject(newProject).subscribe({
      next: (res) => {
        console.log("New project added: " + res.id);
        this.router.navigate(['pmdashboard']); // Navigate to the project dashboard
      },
      error: (err) => {
        console.error("Error adding project:", err);
      }
    });
  }
}
