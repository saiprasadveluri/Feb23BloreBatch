import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Import ReactiveFormsModule
import { RouterModule } from '@angular/router'; // Import RouterModule
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule

import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';

import { AppRoutingModule } from './app-routing.module';
import { CreatetaskComponent } from './createtask/createtask.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { DbAccessService } from './db-access.service';
import { AddprojectlistComponent } from './addprojectlist/addprojectlist.component';
import { AddProjectMembersComponent } from './add-project-members/add-project-members.component';
import { ManageMembersComponent } from './manage-members/manage-members.component';
import { AddprojectmembersComponent } from './addprojectmembers/addprojectmembers.component';
import { ViewuserlistComponent } from './viewuserlist/viewuserlist.component';
@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AddNewUserComponent,
    AdmindashboardComponent,
    PmDashboardComponent,
    CreatetaskComponent ,
    AddNewProjectComponent,
    AddprojectlistComponent,
    AddprojectmembersComponent,
    ManageMembersComponent,
    ViewuserlistComponent
 
    
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule, // Add ReactiveFormsModule here
    RouterModule, // Add RouterModule here
    HttpClientModule, // Add HttpClientModule here,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }