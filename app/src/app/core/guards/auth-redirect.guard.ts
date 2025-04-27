import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRoles } from './role.guard';

export const authRedirectGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if (authService.isAuthenticated) {
    if (authService.userRoles.includes(UserRoles.Admin)) {
      router.navigate(['/admin/panel']);
    } else if (authService.userRoles.includes(UserRoles.Customer)) {
      router.navigate(['/client/store']);
    } else {
      router.navigate(['/access-denied'])
    }
    return false;
  }

  return true;
};
