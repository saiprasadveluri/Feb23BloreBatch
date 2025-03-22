import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ManageTaskComponent } from './manage-task/manage-task.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { ProjectsListComponent } from './projects-list/projects-list.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectMembersComponent } from './project-members/project-members.component';
import { ProjectDasboardComponent } from './project-dasboard/project-dasboard.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    ProjectsListComponent,
    ManageTaskComponent,
    ProjectMembersComponent,
    ProjectDasboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
