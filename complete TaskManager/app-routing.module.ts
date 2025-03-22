import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { RoleSelectionComponent } from './role-selection/role-selection.component';

const routes: Routes = [
  {path:"login",component:AccountComponent},
  {path:'admindashboard',component:AdminDashboardComponent,
    canActivate:[myrouteGuard], data:{reqRole:['ADMIN']}},
  {path:'pmdashboard',component:PmDashboardComponent,
    canActivate:[myrouteGuard], data:{reqRole:['PM']}},
  {path:'Memberdashboard',component:MemberDashboardComponent,
    canActivate:[myrouteGuard], data:{reqRole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  { path: 'addnewproject', component: AddNewProjectComponent },
  { path: 'project-dashboard/:projectId', component: ProjectDashboardComponent },
  { path: 'userdashboard', component: UserDashboardComponent, 
    canActivate: [myrouteGuard], data: { reqRole: ['DEVELOPER', 'QA', 'PM'] } },
  { path: 'role-selection', component: RoleSelectionComponent, 
    canActivate: [myrouteGuard], data: { reqRole: ['PM'] } },

  {path:'',redirectTo:'login',pathMatch:'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
