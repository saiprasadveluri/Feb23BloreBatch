// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   constructor() {}

//   getLoggedInUserId(): string {
//     // Replace this with actual logic to fetch the logged-in user's ID
//     return '1'; // Example user ID
//   }
// }
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInUser: { id: string; role: string } | null = null;

  login(userId: string, role: string): void {
    this.loggedInUser = { id: userId, role: role };
  }

  getLoggedInUserId(): string {
    return this.loggedInUser?.id || '';
  }

  getLoggedInUserRole(): string {
    return this.loggedInUser?.role || '';
  }

  isLoggedIn(): boolean {
    return this.loggedInUser !== null;
  }
}