import { Component, Input, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../../core/components/base-form.component';
import { FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../product.service';

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
    price: ['', [Validators.required]],
    description: ['', [Validators.maxLength(255)]],
  });

  constructor(readonly service: ProductService) {
    super();
  }

  ngOnInit(): void {
    if (this.id) {

    }
  }


  close() {
    this.router.navigate(['..'], { relativeTo: this.activatedRoute });
  }
}
