import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

export const UserRoles = {
  Admin: 'Admin',
  Customer: 'Customer'
};

export const requireRole: (role: string) => CanActivateFn = function(role: string ) {
  return (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const toastrService = inject(ToastrService);

    if (!authService.isAuthenticated) {
      router.navigate(['/login']).then(() => {
        toastrService.info("Faça login para continuar");
      });
      return false;
    }

    const hasRole = authService.userRoles.includes(role);
    if (!hasRole) {
      router.navigate(['/access-denied']);
    }
    
    return hasRole;
  };
}

