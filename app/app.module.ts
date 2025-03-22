import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { PmdashboardComponent } from './pmdashboard/pmdashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { TaskManagementComponent } from './task-management/task-management.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdmindashboardComponent,
    MemberdashboardComponent,
    PmdashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    TaskManagementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
