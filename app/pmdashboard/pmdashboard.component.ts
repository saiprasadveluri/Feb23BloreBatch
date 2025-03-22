import { Component, OnInit } from '@angular/core';
import { ProjectInfo } from '../project-info';
import {Router} from '@angular/router';
import { DBAccessService } from '../dbaccess.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-pmdashboard',
  templateUrl: './pmdashboard.component.html',
  styleUrls: ['./pmdashboard.component.css']
})
export class PmdashboardComponent implements OnInit {
  //frmGroup:FormGroup | undefined;
  projects :ProjectInfo[]=[];
  
  constructor (private srv: DBAccessService,private router:Router){
  }
  ngOnInit(): void{
    var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem("LoggedInUserData") as string);
    if(LoggedInUser){
      this.srv.GetAllProjects().subscribe({
       next:(res)=>{
        var proj:ProjectInfo[] = <ProjectInfo[]>res;
        this.projects = proj.filter((p)=>p.pm==LoggedInUser.id)
        
       },
       error:(err)=>{
        console.error(err)
       }
      });
    }else{
      console.error('No pm name found in sessionStorages')
    }
    
  }
  // viewProjectDetails(name:string) {
  //   this.srv.GetAllProjectsById(name).subscribe({
  //     next: (project) => {
  //       console.log('Project Details:', project);
  //     },
  //     error: (err) => {
  //       console.error('Error fetching project details:', err);
  //     }
  //   });
  // }
  
  
  // NavigateToAddNewProject(){
  //   this.router.navigate(['addnewproject'])
  // }

  
}

