import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../../core/components/base-form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../product.service';
import { CreateProduct, Product, UpdateProduct } from '../product.model';

@Component({
  selector: 'app-product-editor',
  standalone: false,
  templateUrl: './product-editor.component.html',
  styleUrl: './product-editor.component.scss'
})
export class ProductEditorComponent extends BaseFormComponent implements OnInit {

  protected override form = this.formBuilder.group({
    id: [''],
    name: ['', [Validators.required]],
    price: [0, [Validators.required, Validators.min(1)]],
    description: ['', [Validators.maxLength(255)]],
  });

  constructor(readonly service: ProductService, readonly cdr: ChangeDetectorRef) {
    super();
  }

  save() {
    this.handleSubmit(() => {
      const record = this.form.value as Product;
      const action$ = !this.isEdition ?
         this.service.create(record as CreateProduct) :
         this.service.update(record.id, record as UpdateProduct);

      action$.subscribe(() => {
        this.service.triggerListReload$.next(true);
        this.close();
      });
    });
  }

  ngOnInit(): void {
    if (this.isEdition) {
        this.service.getById(this.modelId).subscribe(r => this.form.patchValue(r.data!));
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
