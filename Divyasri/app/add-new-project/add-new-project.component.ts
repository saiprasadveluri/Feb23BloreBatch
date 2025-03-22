import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Newproject } from '../newproject';


@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {

   frmGroup:FormGroup;
  pmList: import("c:/Users/divyasri.m/TaskmanagerProj/taskmanager/src/app/user-info").UserInfo[] | undefined;
    constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
    {
      this.frmGroup=fb.group({
        name:new FormControl('',[Validators.required]),
   
       pm:new FormControl(''),
  
      })
    }
    onAddClick() {
      var name = this.frmGroup.controls['name'].value;
      var pm = this.frmGroup.controls['pm'].value;
    
      var newObj: Newproject = {
        name: name,
        pm: pm
      };
    
      this.srv.AddNewProject(newObj).subscribe({
        next: (res ) => {
          console.log("New project Added: " + res.id);
          this.router.navigate(['admindashboard']);
        },
        error: (err) => {
          console.error("Error adding project:", err);
        }
      });
    }
    ngOnInit(): void {
      this.loadProjectManagers();
  
      // Subscribe to notifications for new users
      this.srv.userAdded$.subscribe(() => {
        this.loadProjectManagers(); // Reload the PM list when a new user is added
      });
    }
    loadProjectManagers(): void {
      this.srv.getUsersByRole('PM').subscribe({
        next: (res) => {
          this.pmList = res;
          console.log('Project Managers:', this.pmList);
        },
        error: (err) => {
          console.error('Error fetching Project Managers:', err);
        }
      });
    }

  }

