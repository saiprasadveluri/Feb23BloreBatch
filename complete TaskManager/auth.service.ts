import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loggedInUser: { id: string; name: string; role: string } | null = null;

  constructor(private router: Router) {}

  
  getLoggedInUser() {
    if (!this.loggedInUser) {
      const user = sessionStorage.getItem('LoggedinUserData');
      if (user) {
        this.loggedInUser = JSON.parse(user);
      }
    }
    return this.loggedInUser;
  }

  
   getLoggedInPMId(): string | null {
    const user = this.getLoggedInUser();
    return user && user.role.toUpperCase() === 'PM' ? user.id : null;
  }


  redirectBasedOnRole() {
    const user = this.getLoggedInUser();
    if (user) {
      if (user.role.toUpperCase() === 'PM') {
        this.router.navigate(['/role-selection']); 
      } else if (user.role.toUpperCase() === 'DEVELOPER' || user.role.toUpperCase() === 'QA') {
        this.router.navigate(['/userdashboard']); 
      }
    } else {
      this.router.navigate(['/login']); 
    }
  }

  logout() {
    this.loggedInUser = null;
    sessionStorage.removeItem('LoggedinUserData');
    this.router.navigate(['/login']);
  }
}
