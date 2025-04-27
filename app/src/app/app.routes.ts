import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login',
        loadChildren: () => import('./features/login/login.module'),
    },
    {
        path: 'register',
        loadChildren: () => import('./features/register/register.module'),
    },
    {
        path: 'admin/panel',
        loadChildren: () => import('./features/panel/panel.module'),
    },
    {
        path: 'client',
        loadChildren: () => import('./features/client/client.module'),
    },
    {
        path: '**',
        redirectTo: 'login',
    }
];
