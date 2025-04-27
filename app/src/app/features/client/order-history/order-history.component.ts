import { Component, OnInit } from '@angular/core';
import { Order } from '../../panel/order/order.model';
import { MenuItem } from 'primeng/api';
import { OrderService } from '../../panel/order/order.service';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { mergeAll, of, takeUntil } from 'rxjs';
import { Filter } from '../../../core/models/filter.model';

@Component({
  selector: 'app-order-history',
  standalone: false,
  templateUrl: './order-history.component.html',
  styleUrl: './order-history.component.scss'
})
export class OrderHistoryComponent extends BaseListComponent<Order> implements OnInit {

  breadcrumbs: MenuItem[] = [
    {
      label: 'Loja',
      icon: 'pi pi-home',
      routerLink: '/client'
    }
  ];

  constructor(readonly orderService: OrderService) {
    super();
  }

    override ngOnInit(): void {
        super.ngOnInit();
  
        this.orderService.triggerListReload$.asObservable()
          .pipe(takeUntil(this.destroy$))
          .subscribe(() => {
            this.resetKeyword();
            this.loadData();
          });
        
        of(of(this.filters), this.filterChanged$.asObservable().pipe(takeUntil(this.destroy$)))
          .pipe(mergeAll())
          .subscribe(this.loadData.bind(this))
      }
  
      loadData(filters?: Filter) {
        filters ??= this.filters;
        this.execute(this.orderService.listByUser(filters));
    }
    
  getStatusClass(status: 'Pending' | 'Cancelled' | 'Paid'): string {
    switch(status) {
      case 'Paid':
        return 'bg-green-100 text-green-800';
      case 'Pending':
        return 'bg-yellow-100 text-yellow-800';
      case 'Cancelled':
        return 'bg-red-100 text-red-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  }


  translateStatus(status: 'Pending' | 'Cancelled' | 'Paid'): string {
    const statusTranslations = {
      'Pending': 'Pendente',
      'Cancelled': 'Cancelado',
      'Paid': 'Pago'
    };
    return statusTranslations[status] || status;
  }

}
