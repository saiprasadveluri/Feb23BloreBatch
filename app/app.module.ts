import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admindashboard/admindashboard.component';
import { PmdashboardComponent } from './pmdashboard/pmdashboard.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { AddNewUserComponent } from './addnewuser/addnewuser.component';
import { AddnewprojectComponent } from './addnewproject/addnewproject.component';
@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmdashboardComponent,
    MemberdashboardComponent,
    AddNewUserComponent,
    AddnewprojectComponent
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