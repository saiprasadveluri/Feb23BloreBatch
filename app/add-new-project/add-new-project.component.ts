import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  projectManagers: UserInfo[] = [];

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = fb.group({
      name: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users: UserInfo[]) => {
        this.projectManagers = users.filter(user => user.role.toUpperCase() === 'PM');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OnAddClick() {
    var name = this.frmGroup.controls['name'].value;
    var description = this.frmGroup.controls['description'].value;
    var pm = this.frmGroup.controls['pm'].value;
    var newProject: ProjectInfo = {name: name, description: description, pm: pm };
    
    this.srv.AddNewProject(newProject).subscribe({
      next: (res) => {
        console.log("New Project Added" + res.id);
        this.router.navigate(['/admindashboard']);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
  }
}
