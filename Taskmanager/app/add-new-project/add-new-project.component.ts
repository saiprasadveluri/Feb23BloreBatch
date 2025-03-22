import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Projectinfo } from '../projectinfo';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  users: Userinfo[] = []; // List of users to select as PM
  FrmGroup: FormGroup;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private route: Router) {
    this.FrmGroup = this.fb.group({
      name: new FormControl('', [Validators.required]), // Project name
      pm: new FormControl('', [Validators.required]) // Project manager (user ID)
    });
  }

  ngOnInit(): void {
    // Fetch all users to populate the PM dropdown
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.users = res as Userinfo[];
        console.log('Users fetched successfully:', this.users);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }

  OnAddClickp(): void {
    if (this.FrmGroup.invalid) {
      console.error('Form is invalid');
      return;
    }

    const pname = this.FrmGroup.controls['name'].value; // Project name
    const pmid = this.FrmGroup.controls['pm'].value; // Selected PM ID

    const newObj: Projectinfo = {
      name: pname,
      pm: pmid
    };

    this.srv.AddNewProject(newObj).subscribe({
      next: (res) => {
        console.log('Project added successfully:', res);
        this.route.navigate(['admindashboard']); // Navigate to admin dashboard
      },
      error: (err) => {
        console.error('Error adding project:', err);
      }
    });
  }
}