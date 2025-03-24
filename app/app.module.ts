import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AddNewProjectMemberComponent } from './add-new-project-member/add-new-project-member.component';
import { AddNewTaskComponent } from './add-new-task/add-new-task.component';
import { AddNewCommentComponent } from './add-new-comment/add-new-comment.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AuthService } from './auth.service';
import { DbAccessService } from './db-access.service'; 

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    AdminDashboardComponent,
    PmDashboardComponent,
    MemberDashboardComponent,
    AddNewUserComponent,
    AddNewProjectComponent,
    AddNewProjectMemberComponent,
    AddNewTaskComponent,
    AddNewCommentComponent,
    UserDashboardComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AuthService, DbAccessService], 
  bootstrap: [AppComponent]
})
export class AppModule { }