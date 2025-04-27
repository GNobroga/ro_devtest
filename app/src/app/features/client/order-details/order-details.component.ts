import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from '../../panel/order/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from '../../panel/order/order.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { OrderStatus } from '../../panel/order/order.enum';

@Component({
  selector: 'app-order-details',
  standalone: false,
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.scss'
})
export class OrderDetailsComponent implements OnInit {

  @Input()
  id!: string;

  order!: Order;
  
  constructor(
    readonly orderService: OrderService, 
    readonly router: Router,
    readonly activatedRoute: ActivatedRoute,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    if (this.id == null) {
      this.router.navigate(['/client/order-history']);
    } else {
      this.orderService.getById(this.id!).subscribe(response => {
        this.order = response.data!;
      });
    }
  }

  closeModal() {
    this.router.navigate(['../../'], { relativeTo: this.activatedRoute });
  }

  translateStatus(status: 'Pending' | 'Cancelled' | 'Paid'): string {
    const statusTranslations = {
      'Pending': 'Pendente',
      'Cancelled': 'Cancelado',
      'Paid': 'Pago'
    };
    return statusTranslations[status] || status;
  }

  confirmCancel(event: Event): void {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Tem certeza que deseja excluir este pedido?',
      header: 'Confirmar Cancelamento',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sim, cancelar',
      rejectLabel: 'Não',
      accept: () => {
        this.orderService.deleteById(this.id!).subscribe(() => {
          this.orderService.triggerListReload$.next(true);
          this.cancelOrder();
        });
      }
    });
  }

  private cancelOrder(): void {
    this.messageService.add({
      severity: 'success',
      summary: 'Sucesso',
      detail: 'Pedido cancelado com sucesso'
    });
    this.closeModal();
  }
}
