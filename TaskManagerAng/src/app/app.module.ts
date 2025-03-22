import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { ReactiveFormsModule } from '@angular/forms';
import{HttpClientModule} from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AssignProjectComponent } from './assign-project/assign-project.component';
import { TaskAssignedComponent } from './task-assigned/task-assigned.component';
import { AddCommentsComponent } from './add-comments/add-comments.component';
import { QaPlaceComponent } from './qa-place/qa-place.component';
import { DeveloperPlaceComponent } from './developer-place/developer-place.component';

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
    AssignProjectComponent,
    TaskAssignedComponent,
    AddCommentsComponent,
    QaPlaceComponent,
    DeveloperPlaceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
