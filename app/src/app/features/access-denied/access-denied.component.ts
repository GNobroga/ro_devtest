import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-access-denied',
  standalone: true,
  templateUrl: './access-denied.component.html',
  styleUrl: './access-denied.component.scss',
  imports: [SharedModule]
})
export class AccessDeniedComponent {
  timestamp: Date = new Date();

  constructor(private location: Location, private router: Router) {}

  goBack() {
    this.location.back();
  }

  goHome() {
    this.router.navigate(['/login']);
  }
}
