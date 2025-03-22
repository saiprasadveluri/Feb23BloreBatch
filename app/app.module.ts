import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { AdmindashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddnewuserComponent } from './addnewuser/addnewuser.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { DisplayProjectListComponent } from './display-project-list/display-project-list.component';
// import { TaskComponent } from './task/task.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdmindashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddnewuserComponent,
    AddNewProjectComponent,
    DisplayProjectListComponent,
    // TaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule // Add FormsModule here
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }