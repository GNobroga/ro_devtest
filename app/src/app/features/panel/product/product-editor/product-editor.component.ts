import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../../core/components/base-form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../product.service';
import { CreateProduct, Product, UpdateProduct } from '../product.model';
import { Utils } from '../../../../core/utilities/utils';

@Component({
  selector: 'app-product-editor',
  standalone: false,
  templateUrl: './product-editor.component.html',
  styleUrl: './product-editor.component.scss'
})
export class ProductEditorComponent extends BaseFormComponent implements OnInit {

  model: Product = {} as Product;

  protected override form = this.formBuilder.group({
    id: [''],
    name: ['', [Validators.required]],
    imageUrl: [''],
    price: [0, [Validators.required, Validators.min(1)]],
    description: ['', [Validators.maxLength(255)]],
  });


  constructor(readonly service: ProductService, readonly cdr: ChangeDetectorRef) {
    super();
  }

  save() {
    this.handleSubmit(() => {
      const record = this.form.value as Product;
      console.log(record)
      const action$ = !this.isEdition ?
         this.service.create(record as CreateProduct) :
         this.service.update(record.id, record as UpdateProduct);

      action$.subscribe(() => {
        this.service.triggerListReload$.next(true);
        this.close();
      });
    });
  }

  hasValueInControl(path: string) {
    return !Utils.isNullOrWhitespace(this.form.get(path)?.value);
  }

  getControlValue(path: string) {
    return this.form.get(path)?.value;
  }

  ngOnInit(): void {
    if (this.isEdition) {
        this.service.getById(this.modelId).subscribe(r => {
          this.model = r.data!;
          this.form.patchValue(this.model);
        });
    }
    this.cdr.detectChanges();
  }

  close() {
    if (this.isEdition) {
      this.router.navigate(['../..'], { relativeTo: this.activatedRoute });
    } else {
      this.router.navigate(['..'], { relativeTo: this.activatedRoute });
    }
  }
}
