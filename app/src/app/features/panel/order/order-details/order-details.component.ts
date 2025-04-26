import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from '../order.service';
import { BaseFormComponent } from '../../../../core/components/base-form.component';
import { FormGroup } from '@angular/forms';
import { Utils } from '../../../../core/utilities/utils';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from '../order.model';
import { OrderStatusUtils } from '../order.enum';

@Component({
  selector: 'app-order-details',
  standalone: false,
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.scss'
})
export class OrderDetailsComponent implements OnInit {

  order: Order | null = null;

  @Input()
  id!: string;

  constructor(readonly service: OrderService, readonly router: Router, readonly activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
      if (Utils.isNullOrWhitespace(this.id)) {
       this.closeModal();
      } else {
        this.loadData();
      }
  }

  loadData() {
    this.service.getById(this.id).subscribe(resp => {
      this.order = resp.data!;
    })
  }

  getStatusSeverity(status: 'Cancelled' | 'Paid' | 'Pending') {
    switch (status) {
      case 'Paid':
        return 'success';
      case 'Pending':
        return 'warn';
      case 'Cancelled':
        return 'danger';
      default:
        return 'info';
    }
  }

  updateOrderStatus(status: 'Cancelled' | 'Paid' | 'Pending') {
    this.service.changeOrderStatus(this.id, OrderStatusUtils.parse(status))
      .subscribe(() => this.loadData());
  }

  closeModal() {
    this.router.navigate(['../../'], { relativeTo: this.activatedRoute });
  }
}
