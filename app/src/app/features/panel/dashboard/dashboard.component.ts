import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {


  breadcrumbs: MenuItem[] = [
    {
      label: 'Dashboard',
      icon: 'pi pi-home',
    }
  ];
  

  products: any = [
    { Id: '1', Name: 'Produto A', Total: 100 },
    { Id: '2', Name: 'Produto B', Total: 200 },
    { Id: '3', Name: 'Produto C', Total: 150 },
    { Id: '4', Name: 'Produto D', Total: 300 }
  ];

  productData: any;

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
