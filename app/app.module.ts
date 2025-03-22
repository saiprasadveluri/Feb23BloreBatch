import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdduserComponent } from './adduser/adduser.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './user-list/user-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { AccountsComponent } from './accounts/accounts.component';
import { PmdashboardComponent } from './pmdashboard/pmdashboard.component';
import { PmprojectListComponent } from './pmproject-list/pmproject-list.component';




@NgModule({
  declarations: [
    AppComponent,
    AdduserComponent,
    UserListComponent,
    ProjectListComponent,
    AdmindashboardComponent,
    AccountsComponent,
    PmdashboardComponent,
    PmprojectListComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
