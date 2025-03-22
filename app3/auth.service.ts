import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loggedInUser: { id: string; name: string; role: string } | null = null;

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

  logout() {
    this.loggedInUser = null;
    sessionStorage.removeItem('LoggedinUserData');
  }
}
