import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AccountsComponent } from './accounts/accounts.component';
import { AdminDashBoardComponent } from './admin-dash-board/admin-dash-board.component';
import { PmDashBoardComponent } from './pm-dash-board/pm-dash-board.component';
import { AddnewuserComponent } from './addnewuser/addnewuser.component';
import { UserListComponent } from './user-list/user-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { AddnewprojectComponent } from './addnewproject/addnewproject.component';
import { PmProjectListComponent } from './pm-project-list/pm-project-list.component';
import { AssignmembersComponent } from './assignmembers/assignmembers.component';
import { AddnewtaskComponent } from './addnewtask/addnewtask.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { MemberTaskListComponent } from './member-task-list/member-task-list.component';
import { MemberCommentListComponent } from './member-comment-list/member-comment-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountsComponent,
    AdminDashBoardComponent,
    PmDashBoardComponent,
    AddnewuserComponent,
    UserListComponent,
    ProjectListComponent,
    AddnewprojectComponent,
    PmProjectListComponent,
    AssignmembersComponent,
    AddnewtaskComponent,
    MemberdashboardComponent,
    MemberTaskListComponent,
    MemberCommentListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
