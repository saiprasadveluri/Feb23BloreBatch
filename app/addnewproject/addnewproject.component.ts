import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DBAccessService } from '../dbaccess.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addnewproject',
  templateUrl: './addnewproject.component.html',
  styleUrls: ['./addnewproject.component.css']
})
export class AddnewprojectComponent implements OnInit {
  frmGroup: FormGroup;
  projectManagers: any[] = []; // Array to store PMs

  constructor(private fb: FormBuilder, private srv: DBAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      name: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.loadProjectManagers(); // Fetch PMs when the component initializes
  }

  // Fetch users with the role "PM"
  loadProjectManagers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (data) => {
        this.projectManagers = data.filter((user: { role: string }) => user.role === 'PM');
      },
      error: (err) => {
        console.error('Error fetching project managers:', err);
      }
    });
  }

  OnAddClick(): void {
    const pname = this.frmGroup.controls['name'].value;
    const pm = this.frmGroup.controls['pm'].value;

    const pnewObj = {
      name: pname,
      pm: pm,
    };

    this.srv.AddNewProject(pnewObj).subscribe({
      next: (res) => {
        console.log('Project added successfully:', res);
        this.router.navigate(['admindashboard']);
      },
      error: (err) => {
        console.error('Error adding project:', err);
      }
    });
  }
}