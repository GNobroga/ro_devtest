import { Component, computed, OnInit, signal } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BaseFormComponent } from '../../../core/components/base-form.component';
import { FormGroup } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent extends BaseFormComponent implements OnInit {

  isEditingProfile = signal(false);

  protected override form = this.formBuilder.group({
    name: [{ value: '', disabled: true }],
    userName: [{ value: '', disabled: true }],
    email: [{ value: '', disabled: true }],
    password: [{ value: '', disabled: true }],
    passwordConfirmation: [{ value: '', disabled: true }],
  });

  breadcrumbs: MenuItem[] = [
    {
      label: 'Dashboard',
      icon: 'pi pi-home',
      routerLink: '/admin/panel'
    },
    {
      label: 'Perfil',
      icon: 'pi pi-user',
    }
  ];

  user = computed(() => this.authService.user());

  constructor(readonly authService: AuthService) {
    super();
  }


  
}
