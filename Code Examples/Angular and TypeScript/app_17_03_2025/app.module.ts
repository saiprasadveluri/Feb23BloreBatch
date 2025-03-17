import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserlistComponent } from './userlist/userlist.component';
import { UseritemComponent } from './useritem/useritem.component';
import { AdduserComponent } from './adduser/adduser.component';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdUserListComponent } from './ad-user-list/ad-user-list.component';
import { AdRestaurantListComponent } from './ad-restaurant-list/ad-restaurant-list.component';

@NgModule({
  declarations: [
    AppComponent,
    UserlistComponent,
    UseritemComponent,
    AdduserComponent,
    AdminDashboardComponent,
    AdUserListComponent,
    AdRestaurantListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
