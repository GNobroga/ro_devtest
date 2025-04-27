import { Component, computed } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { OrderStatus } from '../order/order.enum';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../order/order.service';
import { OrderSummary, OrderSummaryProduct } from '../order/order.model';
import { DateUtils } from '../../../core/utilities/date-utils';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

  form = new FormGroup({
    startDate: new FormControl(DateUtils.generateFirstDayOfMonth(), [Validators.required]),
    endDate: new FormControl(DateUtils.generateLastDayOfMonth(), [Validators.required]),
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

  constructor(
	readonly toastrService: ToastrService, 
	readonly orderService: OrderService) {}

  orderSummary: OrderSummary = {
	products: [],
	revenue: 0,
	totalClients: 0,
	totalOrders: 0
  }; 

  productData: any;

  applyFilters(): void {
	if (this.form.invalid) {
	  this.toastrService.warning('Preencha a data de início e fim para prosseguir');
	  return;
	}
  
	const { startDate, endDate, status } = this.form.value;
  
	if (!startDate || !endDate) {
	  this.toastrService.warning('Datas de início e fim são obrigatórias');
	  return;
	}
  
	const selectedStatus = (status === OrderStatus.ALL ? undefined : status)!;
  
	this.orderService
	  .getSummary(startDate, endDate, selectedStatus)
	  .subscribe(this.processOrderSummary.bind(this));
  }
  
  
  processOrderSummary(summary: OrderSummary) {
	this.orderSummary = summary;
	const { products } = summary;
	console.log(products)
	this.productData = {
		labels: products.map((product: any) => product.name),
		datasets: [
		  {
			label: 'Total de Vendas',
			data: products.map((product: any) => product.total),
			backgroundColor: '#42A5F5',
			borderColor: '#1E88E5',
			borderWidth: 1
		  }
		]
	};
  }

  calculateAverageTicket(totalRevenue: number, totalOrders: number): number {
		if (totalOrders === 0) {
			return 0;
		}
		return totalRevenue / totalOrders;	
	}


  ngOnInit() {
	this.applyFilters();
  }
}
