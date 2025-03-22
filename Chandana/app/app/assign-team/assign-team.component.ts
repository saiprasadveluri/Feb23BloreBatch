import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assign-team',
  templateUrl: './assign-team.component.html',
  styleUrls: ['./assign-team.component.css']
})
export class AssignTeamComponent implements OnInit {
  assignForm!: FormGroup;
  projects: any[] = [];
  developers: any[] = [];
  qas: any[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.assignForm = this.fb.group({
      project: ['', Validators.required],
      developer: ['', Validators.required],
      qa: ['', Validators.required]
    });

    this.fetchData();
  }

  fetchData(): void {
    this.dataService.getData().subscribe((data: { Project: any[]; UserInfo: any[]; }) => {
      this.projects = data.Project;
      this.developers = data.UserInfo.filter(user => user.role === 'DEVELOPER');
      this.qas = data.UserInfo.filter(user => user.role === 'QA');
    }, (error: any) => {
      console.error('Error fetching data', error);
    });
  }

  onAssignClick(): void {
    if (this.assignForm.valid) {
      const assignment = this.assignForm.value;
      console.log('Assignment:', assignment);
      // Add your logic to handle the assignment submission
    } else {
      console.error('Form is invalid');
    }
  }

  navigateToAssignTeam(): void {
    this.router.navigate(['/assign-team']);
  }
}
