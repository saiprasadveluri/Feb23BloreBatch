import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewOwnProjectsComponent } from './view-own-projects/view-own-projects.component';
import { AssignMemberComponent } from './assign-member/assign-member.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { QaDashboardComponent } from './qa-dashboard/qa-dashboard.component';
import { DeveloperDashboardComponent } from './developer-dashboard/developer-dashboard.component';
import { ViewCommentComponent } from './view-comment/view-comment.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { ReassignTaskComponent } from './reassign-task/reassign-task.component';
import { ViewOwnTaskComponent } from './view-own-task/view-own-task.component';


@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    MemberDashboardComponent,
    PmDashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    ViewUsersComponent,
    ViewProjectsComponent,
    ViewOwnProjectsComponent,
    AssignMemberComponent,
    CreateTaskComponent,
    ViewTaskComponent,
    QaDashboardComponent,
    DeveloperDashboardComponent,
    ViewCommentComponent,
    AddCommentComponent,
    ViewOwnTaskComponent,
    ReassignTaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
