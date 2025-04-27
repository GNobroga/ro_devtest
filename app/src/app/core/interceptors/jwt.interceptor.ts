import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  if (authService.isAuthenticated) {
    const authorizedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${authService.accessToken}`
      }
    });
    return next(authorizedRequest);
  }

  return next(req);
};