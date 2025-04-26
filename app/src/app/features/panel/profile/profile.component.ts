import { Component, computed, effect, OnInit, signal } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BaseFormComponent } from '../../../core/components/base-form.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { CreateOrUpdateUser, User } from '../../../core/models/user.model';
import { Subject, takeUntil } from 'rxjs';
import { passwordMatchValidator } from '../../../core/validators/password-matcher.validator';
import { UserService } from '../../../core/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent extends BaseFormComponent implements OnInit {

  isEditingProfile = new Subject();

  displayPasswordControl = new FormControl({ value: false, disabled: true });

  protected override form = this.formBuilder.group({
    id: ['', Validators.required],
    name: ['', [Validators.required]],
    userName: ['', [Validators.required]],
    email: ['',[Validators.required, Validators.email]],
    password: [''],
    passwordConfirmation: [''],
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

  constructor(readonly authService: AuthService, readonly userService: UserService) {
    super();
  }

  updateProfile() {
    this.handleSubmit(() => {
      const formValues = this.form.value as CreateOrUpdateUser & { id: string };
      this.userService.updateAdmin(formValues.id, formValues).subscribe(() => {
        this.authService.tryToLoadUserProfile()
          .then(() => {
            this.toastrService.success("Perfil atualizado com sucesso!");
          });
      });
    });
  }
 
  ngOnInit(): void {
    this.authService.user$.pipe(takeUntil(this.destroy$)).subscribe(user => {
      this.form.patchValue(user!);
      this.form.disable();
    });

    this.displayPasswordControl.valueChanges.pipe(takeUntil(this.destroy$)).subscribe(val => {
        const actionName = val ? 'addValidators' : 'removeValidators';
        this.form.get('password')?.[actionName]?.([Validators.required, Validators.minLength(6)]);
        this.form.get('passwordConfirmation')?.[actionName]?.(passwordMatchValidator(this.form, 'password'));
    });

    this.isEditingProfile.asObservable().pipe(takeUntil(this.destroy$)).subscribe(val => {
      const actionName = this.form.disabled ? 'enable' : 'disable';
      this.form[actionName]?.();
      this.displayPasswordControl[actionName]?.();
      if (this.displayPasswordControl.disabled) {
        this.displayPasswordControl.setValue(false);
      }
    });
  }


  
}
