import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  projectManagers: any[] = []; 
  constructor(private fb: FormBuilder, private srv: DbAccessService, private route: Router) {
    this.frmGroup = this.fb.group({
      name: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required])
    });
  }
  ngOnInit(): void {
    this.getProjectManagers();
  }
  getProjectManagers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (data: Userinfo[]) => {
        this.projectManagers = data.filter(user => user.role === 'PM');
      },
      error: (err) => {
        console.error("Error fetching Project Managers: ", err);
      }
    });
  }
  
  onAddProject(): void {
    var name = this.frmGroup.controls['name'].value;
    var pm = this.frmGroup.controls['pm'].value;
    var newObj: Project = {
      name: name,
      pm: pm
    };
    this.srv.AddNewProject(newObj).subscribe({
      next: (res: Project) => {
        console.log("New Project Added: " + res.id);
        this.route.navigate(['/admindashboard']);
      },
      error: (err: any) => {
        console.log(err);
      }
    });
  }
}
