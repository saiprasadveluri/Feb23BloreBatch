import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-role-selection',
  templateUrl: './role-selection.component.html',
  styleUrls: ['./role-selection.component.css']
})
export class RoleSelectionComponent {
  constructor(private router: Router, private authService: AuthService) {}


  goToPmDashboard() {
    this.router.navigate(['/pmdashboard']);
  }

  goToUserDashboard() {
    this.router.navigate(['/userdashboard']);
  }
}
