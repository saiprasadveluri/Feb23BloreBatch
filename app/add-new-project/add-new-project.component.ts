import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
 frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=fb.group({
      name:new FormControl('',[Validators.required]),
      pm:new FormControl('',[Validators.required])    
    });
  }
  OnAddProClick()
  {
    var pname=this.frmGroup.controls['name'].value;
    var pmg=this.frmGroup.controls['pm'].value;    
    var newObj:ProjectInfo={
      name:pname,
      pm:pmg
    };
    this.srv.AddNewProject(newObj).subscribe({
      next:(res)=>{
        console.log("New Project Added:"+res.id);
        this.router.navigate(['pmdashboard']);
      },
      error:(err)=>{
 
      }
    })    
  }
   
  }
 
 