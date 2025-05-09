import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { BaseFormComponent } from '../../core/components/base-form.component';
import { UserService } from '../../core/services/user.service';
import { passwordMatchValidator } from '../../core/validators/password-matcher.validator';
import { CreateOrUpdateUser } from '../../core/models/user.model';

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
    email: ['',[Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    passwordConfirmation: ['', [Validators.required]],
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
    this.handleSubmit(() => {
      this.userService.createUser(this.form.value as CreateOrUpdateUser, this.role.value!)
        .subscribe(() => {
          this.toastrService.success('Conta criada com sucesso');
          this.router.navigate(['/login']);
        });
    });
  }

}
