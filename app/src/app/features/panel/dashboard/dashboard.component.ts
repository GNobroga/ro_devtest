import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { OrderStatus } from '../order/order.enum';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../order/order.service';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

  form = new FormGroup({
    startDate: new FormControl(new Date(), [Validators.required]),
    endDate: new FormControl(new Date(), [Validators.required]),
    status: new FormControl(OrderStatus.ALL),
  });

  statusOptions = [
    { label: 'Todos', value: OrderStatus.ALL },
    { label: 'Cancelado', value: OrderStatus.CANCELLED },
    { label: 'Pendente', value: OrderStatus.PENDING },
    { label: 'Pago', value: OrderStatus.PAID },
  ];

  breadcrumbs: MenuItem[] = [
    {
      label: 'Dashboard',
      icon: 'pi pi-home',
    }
  ];

  constructor(readonly toastrService: ToastrService, readonly orderService: OrderService) {}
  

  products: any = [
    { Id: '1', Name: 'Produto A', Total: 100 },
    { Id: '2', Name: 'Produto B', Total: 200 },
    { Id: '3', Name: 'Produto C', Total: 150 },
    { Id: '4', Name: 'Produto D', Total: 300 }
  ];

  productData: any;

  applyFilters() {
    if (this.form.invalid) {
      this.toastrService.warning('Preencha a data de início e fim para prosseguir');
      return;
    }
    const { startDate, endDate, status } = this.form.value;
  
	this.orderService.getSummary(startDate!, endDate!, (status === OrderStatus.ALL) ? undefined : status!)
		.subscribe(summary => {
			console.log(summary);
		});
  }

  ngOnInit() {
    this.productData = {
      labels: this.products.map((product: any) => product.Name),
      datasets: [
        {
          label: 'Total de Vendas',
          data: this.products.map((product: any) => product.Total),
          backgroundColor: '#42A5F5',
          borderColor: '#1E88E5',
          borderWidth: 1
        }
      ]
    };
  }
}
