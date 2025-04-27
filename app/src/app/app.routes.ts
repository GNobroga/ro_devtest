import { Routes } from '@angular/router';
import { AccessDeniedComponent } from './features/access-denied/access-denied.component';
import { requireRole, UserRoles } from './core/guards/role.guard';
import { authRedirectGuard } from './core/guards/auth-redirect.guard';

export const routes: Routes = [
    {
        path: 'login',
        canActivate: [authRedirectGuard],
        loadChildren: () => import('./features/login/login.module'),
    },
    {
        path: 'register',
        canActivate: [authRedirectGuard],
        loadChildren: () => import('./features/register/register.module'),
    },
    {
        path: 'admin/panel',
        canActivate: [requireRole(UserRoles.Admin)],
        loadChildren: () => import('./features/panel/panel.module'),
    },
    {
        path: 'client',
        canActivate: [requireRole(UserRoles.Customer)],
        loadChildren: () => import('./features/client/client.module'),
    },
    {
        path: 'access-denied',
        component: AccessDeniedComponent,
    },
    {
        path: '**',
        redirectTo: 'login',
    }
];
