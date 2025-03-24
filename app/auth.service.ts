import { Injectable } from '@angular/core';
import { DbAccessService } from './db-access.service';
import { UserInfo } from './user-info';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUser: UserInfo | null = null;

  constructor(private dbAccessService: DbAccessService) {}

  login(email: string, password: string): Observable<boolean> {
    return this.dbAccessService.GetAllUsers().pipe(
      map(users => {
        const user = users.find((u: UserInfo) => u.email === email && u.password === password);
        if (user) {
          this.currentUser = user;
          return true;
        } else {
          return false;
        }
      })
    );
  }

  getCurrentUser(): UserInfo | null {
    return this.currentUser;
  }

  logout(): void {
    this.currentUser = null;
  }
}