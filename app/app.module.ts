// import { NgModule } from '@angular/core';
//  import { BrowserModule } from '@angular/platform-browser';
 
//  import { AppRoutingModule } from './app-routing.module';
//  import { AppComponent } from './app.component';
//  import { AccountComponent } from './account/account.component';
//  import{ReactiveFormsModule} from '@angular/forms';
//  import{HttpClientModule} from '@angular/common/http';
//  import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
//  import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
//  import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
//  import { AddNewUserComponent } from './add-new-user/add-new-user.component';
//  import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
//  import { DeleteUserComponent } from './delete-user/delete-user.component';
//  import { AddProjectComponent } from './add-project/add-project.component';
//  import { ViewProjectsComponent } from './view-projects/view-projects.component';
//  import { ViewUsersComponent } from './view-users/view-users.component';
// import { AddTaskComponent } from './add-task/add-task.component';
 
//  @NgModule({
//    declarations: [
//      AppComponent,
//      AccountComponent,
//      AdminDashboardComponent,
//      PmDashboardComponent,
//      MemberDashboardComponent,
//      AddNewUserComponent,
//      AddNewProjectComponent,
//      DeleteUserComponent,
//      AddProjectComponent,
//      ViewProjectsComponent,
//      ViewUsersComponent,
//      AddTaskComponent
//    ],
//    imports: [
//      BrowserModule,
//      AppRoutingModule,
//      ReactiveFormsModule,
//      HttpClientModule
//    ],
//    providers: [],
//    bootstrap: [AppComponent]
//  })
//  export class AppModule { }


import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Import FormsModule and ReactiveFormsModule
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { AdmindashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { AddProjectComponent } from './add-project/add-project.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdmindashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    DeleteUserComponent,
    AddProjectComponent,
    ViewProjectsComponent,
    ViewUsersComponent,
    AddTaskComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, // Add FormsModule here
    ReactiveFormsModule, // Add ReactiveFormsModule here
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
