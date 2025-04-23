import { FormBuilder, FormGroup } from "@angular/forms";
import { BaseModel } from "../models/base.model";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { MessageService } from "primeng/api";
import { ToastrService } from "ngx-toastr";

export abstract class BaseFormComponent {
    
    protected abstract form: FormGroup<any>;

    protected formBuilder = inject(FormBuilder);

    protected router = inject(Router);

    protected toastrService = inject(ToastrService);

    handleSubmit(callback: VoidFunction) {
      if (this.form.invalid) {
        this.form.markAllAsTouched();
        return;
      }
      callback();
    }


    getErrorMessage(controlName: string) {
        const controlErrors = this.form.get(controlName)?.errors ?? {};

        const errorMessages: { [key: string]: (err: any) => string } = {
            required: () => 'O campo é obrigatório',
            email: () => 'Email inválido',
            minlength: ({ requiredLength, actualLength }) =>
              `O campo deve ter pelo menos ${requiredLength} caracteres (atualmente tem ${actualLength})`,
            maxlength: ({ requiredLength, actualLength }) =>
              `O campo deve ter no máximo ${requiredLength} caracteres (atualmente tem ${actualLength})`,
            min: ({ min, actual }) =>
              `O valor deve ser no mínimo ${min} (valor atual: ${actual})`,
            max: ({ max, actual }) =>
              `O valor deve ser no máximo ${max} (valor atual: ${actual})`,
            pattern: () => 'formato inválido',
            nullValidator: () => 'Valor inválido',
            requiredTrue: () => 'Você deve aceitar essa opção',
            passwordMismatch: () => 'As senhas não coincidem',
          };
          
          const errors = Object.keys(controlErrors);
          const error = errors?.[0];
          return error != null ? errorMessages[error](controlErrors[error]) : null;
    } 

    isFieldInvalid(field: string): boolean {
      const control = this.form.get(field);
      return !!control && control.invalid && (control.dirty || control.touched);
    }
    
}