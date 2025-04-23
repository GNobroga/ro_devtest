import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { LoginCredentials } from '../../core/models/auth.model';
import { BaseFormComponent } from '../../core/components/base-form.component';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: false,
})
export class LoginComponent extends BaseFormComponent {

  protected override form = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  }, { updateOn: 'change' })

  constructor(readonly authService: AuthService) {
    super();
  }

  login() {
    this.authService.login(this.form.value as LoginCredentials) 
      .subscribe();
  }
}
