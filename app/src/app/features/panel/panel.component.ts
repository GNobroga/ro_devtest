import { Component, computed, effect, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { filter, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-panel',
  standalone: false,
  templateUrl: './panel.component.html',
  styleUrl: './panel.component.scss'
})
export class PanelComponent implements OnInit {

  drawerVisible = true;

  currentUrl = location.pathname;

  keywordControl = new FormControl('');

  destroy$ = new Subject();

  items: MenuItem[] = [
    {
      label: 'Perfil',
      icon: 'pi pi-user',
      routerLink: '/admin/panel/profile',
    },
    {
      label: 'Sair',
      icon: 'pi pi-sign-out',
      command: () => this.authService.logout(),
    }
  ];

  constructor(
    readonly router: Router, 
    readonly authService: AuthService
  ) {}

  ngOnInit(): void {
      this.router.events.pipe(filter(e => e instanceof NavigationEnd))
        .subscribe(e => this.currentUrl = e.url);
  }

  hasPathInUrl(path: string, pathMatch: 'full' | 'prefix') {
    return pathMatch === 'prefix' ? this.currentUrl.includes(path) : this.currentUrl === path;
  }

  getActiveRouteClasses(path: string, patchMatch: 'full' | 'prefix' = 'prefix') {
    //console.log(path, this.hasPathInUrl(path, patchMatch))
    return this.hasPathInUrl(path, patchMatch) ? 'bg-blue-600 !text-white hover:!bg-blue-500' : '';
  }
  
}
