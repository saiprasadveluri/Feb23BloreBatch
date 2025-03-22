import { Component } from '@angular/core';
import { FormControl, Validators,FormBuilder,FormGroup } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user.info';
import { ProjectInfo } from '../project.info';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
    this.frmGroup=fb.group({
    name:new FormControl('',[Validators.required]),
      
    PM:new FormControl('',[Validators.required]),

    });
  }
  OnAddClick(){
    var pname=this.frmGroup.controls['name'].value;
    
    var pm=this.frmGroup.controls['PM'].value;
    var newObj:ProjectInfo={
      name:pname,
      PM:pm,
      
    };
    this.srv.AddNewProject(newObj).subscribe({
      next:(res)=>{
        console.log("new project added: "+res.id);
        this.router.navigate(['admindashboard']);

      },
      error:(err)=>{

      }
    })

  }

}
