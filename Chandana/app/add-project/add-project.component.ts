import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';
//import { AddNewProject } from 'src/add-new-project';
 
@Component({
  selector: 'app-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent {
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
    var newObj:ProjInfo={
      name: pname,
      pm: pmg,
      id: '',
      managerId: 0,
      qa: [],
      developers: []
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