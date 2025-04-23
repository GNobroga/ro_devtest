import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordMatchValidator(formGroup: FormGroup, passwordKey: string): ValidatorFn {
  return (confirmPasswordControl: AbstractControl): ValidationErrors | null => {
    const passwordControl = formGroup.get(passwordKey);
    const confirmPasswordValue = confirmPasswordControl.value;

    if (!passwordControl) return null; // segurança: campo não encontrado

    return passwordControl.value === confirmPasswordValue
      ? null
      : { passwordMismatch: true };
  };
}