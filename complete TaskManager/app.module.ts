import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectMembersComponent } from './project-members/project-members.component';
import { TaskManagementComponent } from './task-management/task-management.component';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { CommentComponent } from './comment/comment.component';
import { RoleSelectionComponent } from './role-selection/role-selection.component';



@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    ProjectListComponent,
    ProjectMembersComponent,
    TaskManagementComponent,
    ProjectDashboardComponent,
    UserDashboardComponent,
    CommentComponent,
    RoleSelectionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule
   
   
  
     
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }




