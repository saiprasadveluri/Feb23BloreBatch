import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PmDashboardGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
    if (loggedInUser.role === 'PM') {
      return true;
    }
    this.router.navigate(['login']);
    return false;
  }
}