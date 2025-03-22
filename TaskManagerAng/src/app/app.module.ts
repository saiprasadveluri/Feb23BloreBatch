import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { PmProjectListComponent } from './pm-project-list/pm-project-list.component';
import { AddProjectMemberComponent } from './add-project-member/add-project-member.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { PmTaskListComponent } from './pm-task-list/pm-task-list.component';
import { MemberTaskListComponent } from './member-task-list/member-task-list.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { MemberCommentListComponent } from './member-comment-list/member-comment-list.component';
import { MemberUpdateTaskComponent } from './member-update-task/member-update-task.component';


@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    UserListComponent,
    ProjectListComponent,
    AddNewProjectComponent,
    PmProjectListComponent,
    AddProjectMemberComponent,
    AddTaskComponent,
    PmTaskListComponent,
    MemberTaskListComponent,
    AddCommentComponent,
    MemberCommentListComponent,
    MemberUpdateTaskComponent
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
