import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdduserComponent } from './adduser/adduser.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { AccountsComponent } from './accounts/accounts.component';

const routes: Routes = [
  {path:"login",component:AccountsComponent},
  { path: 'admindashboard', component: AdmindashboardComponent },
  {path:'adduser',component:AdduserComponent},
  //{ path: 'pm-dashboard', component: PmDashboardComponent },
 // { path: 'developer-dashboard', component: DeveloperDashboardComponent },
 // { path: 'qa-dashboard', component: QaDashboardComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
