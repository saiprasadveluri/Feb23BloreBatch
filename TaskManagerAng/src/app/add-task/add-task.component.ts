import { Component, OnInit } from '@angular/core';
import { ProjectMember } from '../project-member';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task';
import { Project } from '../project';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {
  projMem: ProjectMember[] = [];
  frmGroup: FormGroup;
  projid: string[] = [];
  message: string = '';
  taskList: Task[] = [];
  project: Project[] = [];
  filteredMembers: ProjectMember[] = [];

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router, private route: ActivatedRoute) {
    this.frmGroup = fb.group({
      projid: new FormControl('', [Validators.required]),
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      tasktype: new FormControl('', [Validators.required]),
      assignedto: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.projid.push(id);
      }
      console.log(this.projid);
    });
    this.GetProjMem();
    this.GetTasks();
  }

  GetProjMem(): void {
    this.srv.GetAllProjMem().subscribe({
      next: (res) => {
        var projmem1: ProjectMember[] = <ProjectMember[]>res;
        console.log('id',this.projid)
        console.log(projmem1);
        this.projid = this.projid[0].split(',');
        this.projMem = projmem1.filter((u) => this.projid.includes(u.projid));
        console.log('Proj -',this.projMem);
        if (this.projMem == undefined) {
          this.message = 'No Tasks Available or Can Be Assigned';
        }
      }
    });
  }

  GetProjects(): void {
    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        var proj1: Project[] = <Project[]>res;
        this.project = proj1.filter((u) => u.id && this.projid.includes(u.id));
      }
    });
  }

  GetTasks(): void {
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        var taskList1: Task[] = <Task[]>res;
        this.taskList = taskList1.filter((u) => this.projid.includes(u.projid));
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onProjectChange(): void {
    const selectedProjectId = this.frmGroup.controls['projid'].value;
    this.filteredMembers = this.projMem.filter(pm => pm.projid === selectedProjectId);
  }

  OnAddTask(): void {
    var projid = this.frmGroup.controls['projid'].value;
    var title = this.frmGroup.controls['title'].value;
    var description = this.frmGroup.controls['description'].value;
    var tasktype = this.frmGroup.controls['tasktype'].value;
    var assignedto = this.frmGroup.controls['assignedto'].value;
    var status = this.frmGroup.controls['status'].value;

    var obj:Task={projid:projid,title:title,description:description,tasktype:tasktype,assignedto:assignedto,status:status}
    this.srv.AddTask(obj).subscribe({
      next:(res)=>{
        this.router.navigate(['pmdashboard']);
      },
      error:()=>{

      }
    })
  }
}