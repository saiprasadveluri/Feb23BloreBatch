import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  users: { id: number, name: string, role: string }[] = [];
  selectedUserId: number | null = null; 

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      pid: new FormControl('', [Validators.required]),
      pname: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit() {
    this.loadUsers(); 
  }

  loadUsers() {
    this.srv.GetAllUsers().subscribe(
      (users) => {
        this.users = users;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  OnAddProject() {
    if (this.frmGroup.valid) {
      const pid = this.frmGroup.controls['pid'].value;
      const pname = this.frmGroup.controls['pname'].value;
      const pm = this.selectedUserId; 

      const newObj: ProjectInfo = {
        pid: pid,
        pname: pname,
        pm: pm !== null ? pm.toString() : '',
      };

      this.srv.AddNewProject(newObj).subscribe({
        next: (res) => {
          console.log('New Project Added: ' + res.id);
          this.router.navigate(['admindashboard']);
        },
        error: (err) => {
          console.error('Error adding project:', err);
        }
      });
    } else {
      alert('Please fill in all required fields.');
    }
  }
}


