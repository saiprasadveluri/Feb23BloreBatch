import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  projList: Project[] = [];
  manlist:UserInfo[]=[];
  frmGroup:FormGroup;
  
    constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
      this.frmGroup=fb.group({
        name:new FormControl('',[Validators.required]),
        mid:new FormControl('',[Validators.required]),
        
      })
    }
  
  ngOnInit(): void {
    this.srv.GetAllUsers().subscribe({
      next:(res)=>{
          var AllUsers=<UserInfo[]>res;
          console.log(AllUsers);
          this.manlist=AllUsers.filter((u)=>u.role.toUpperCase()=='PMANAGER');

      },
      error:(err)=>{

      }
    })
    
  }

  OnAddProject():void{
    var name = this.frmGroup.controls['name'].value;
    var mid = this.frmGroup.controls['mid'].value;
    

    var obj:Project={name:name,pm:mid};
    this.srv.AddNewProject(obj).subscribe(
      {
        next:(res)=>{
          this.router.navigate(['admindashboard']);
        },
        error:()=>{

        }
      }
    )
  }

}
