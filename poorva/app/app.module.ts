import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { ReactiveFormsModule } from '@angular/forms';
import{HttpClientModule} from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { RouterModule } from '@angular/router';
import { AddProjectComponent } from './add-project/add-project.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { DbAccessService } from './db-access.service';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewOwnProjectsComponent } from './view-own-projects/view-own-projects.component';
import { ViewProjectMembersComponent } from './view-project-members/view-project-members.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { TaskStatusComponent } from './task-status/task-status.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    MemberDashboardComponent,
    PmDashboardComponent,
    AddNewUserComponent,
    AddProjectComponent,
    ViewUsersComponent,
    ViewProjectsComponent,
    ViewOwnProjectsComponent,
    ViewProjectMembersComponent,
    ViewTaskComponent,
    TaskStatusComponent,
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
  
  ],
  providers: [DbAccessService],
  bootstrap: [AppComponent]
})
export class AppModule { }
