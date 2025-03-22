import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddProjectComponent } from './add-project/add-project.component';
import { DbAccessService } from './db-access.service';
import { AssignTeamComponent } from './assign-team/assign-team.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { QaDashboardComponent } from './qa-dashboard/qa-dashboard.component';
import { DeveloperDashboardComponent } from './developer-dashboard/developer-dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    AddProjectComponent,
    AssignTeamComponent,
    CreateTaskComponent,
    QaDashboardComponent,
    DeveloperDashboardComponent,

    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [DbAccessService],
  bootstrap: [AppComponent]
})
export class AppModule { }
