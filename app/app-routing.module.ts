import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AddNewProjectMemberComponent } from './add-new-project-member/add-new-project-member.component';
import { AddNewTaskComponent } from './add-new-task/add-new-task.component';
import { myrouteGuard } from './myroute.guard';

const routes: Routes = [
  { path: 'login', component: AccountComponent },
  { path: 'admindashboard', component: AdminDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['ADMIN'] } },
  { path: 'pmdashboard', component: PmDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['PM'] } },
  { path: 'memberdashboard', component: MemberDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['PM'] } },
  { path: 'addnewuser', component: AddNewUserComponent},
  { path: 'addnewproject', component: AddNewProjectComponent},
  { path: 'add-new-task/:projectId', component: AddNewTaskComponent},
  { path: 'add-new-project-member/:projectId', component: AddNewProjectMemberComponent},
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }