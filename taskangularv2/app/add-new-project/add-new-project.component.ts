import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';


interface UserInfo {
  name: string;
  role: string;
  id: string;
}

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  projectManagers: UserInfo[] = []; // To store filtered PMs

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.getProjectManagers(); // Call the method to fetch PMs on initialization
   // Call the method to fetch projects on initialization
}

  getProjectManagers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (res: UserInfo[]) => {
        this.projectManagers = res.filter(user => user.role?.trim().toUpperCase() === 'PM');
        console.log('Filtered Project Managers:', this.projectManagers);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }

  OnAddProject(): void {
    const id = this.frmGroup.controls['id'].value;
    const name = this.frmGroup.controls['name'].value;
    const pm = this.frmGroup.controls['pm'].value;

    const newObj = { id, name, pm };
    this.srv.AddNewProject(newObj).subscribe({
      next: (res) => {
        console.log('New project added:', res);
        this.router.navigate(['admindashboard']);
      },
      error: (err) => {
        console.error('Error adding project:', err);
      }
    });
  }
}