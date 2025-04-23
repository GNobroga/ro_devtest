import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../core/components/base-form.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../../core/validators/password-matcher.validator';
import { Router } from '@angular/router';
import { UserService } from '../../core/services/user.service';
import { CreateUser } from '../../core/models/user.model';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent extends BaseFormComponent implements OnInit {

  role = new FormControl<'Admin' | 'Customer'>('Admin');

  protected override form = this.formBuilder.group({
    userName: ['', [Validators.required, Validators.minLength(6)]],
    name: ['', [Validators.required, Validators.minLength(6)]],
    email: ['', [Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    passwordConfirmation: [''],
  });

  constructor(
    readonly userService: UserService,
  ) {
    super();
  }

 
  ngOnInit(): void {
      this.form.controls.passwordConfirmation.addValidators(passwordMatchValidator(this.form, 'password'));
  }

  register() {
    this.userService.createUser(this.form.value as CreateUser, this.role.value!)
      .subscribe(() => {
        this.messageService.add({
          severity: 'success',
          detail: 'Conta criada com sucesso'
        })
        this.router.navigate(['/login']);
      });
  }

}
