import { Injectable } from '@angular/core';
import { BaseService } from '../../../core/services/base.service';
import { OrderStatus } from './order.enum';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends BaseService {

  constructor() {
    super('order');
  }

  getSummary(startDate: Date, endDate: Date, status: OrderStatus) {
    return this.handleRequest(
      this.httpClient.get(this.extendApiUrl('/summary'), {  })
    );
  }

    // list(filters: Filter): Observable<PageResult<Product>> {
    //       const filterQueryParams = QueryParamsUtils.toQueryString(filters);
    //       return this.handleRequest<PageResult<Product>>(
    //           this.httpClient.get(this.extendApiUrl(`?${filterQueryParams}`))
    //       )
    //       .pipe(map(response => response.data!));
    //   }
  
    //   getById(id: string): Observable<ApiResponse<Product>> {
    //       return this.handleRequest(
    //           this.httpClient.get(this.extendApiUrl(`/${id}`))
    //       );
    //   }
      
    //   create(record: CreateProduct): Observable<ApiResponse<CreateProductResult>> {
    //       return this.handleRequest(
    //           this.httpClient.post(this.apiUrl, record)
    //       );
    //   }
  
    //   update(id: string, record: UpdateProduct): Observable<ApiResponse<UpdateProductResult>> {
    //       return this.handleRequest(
    //           this.httpClient.put(this.extendApiUrl(`/${id}`), record),
    //       );
    //   }
  
    //   deleteById(id: string): Observable<ApiResponse<DeleteProductResult>> {
    //       return this.handleRequest(
    //           this.httpClient.delete(this.extendApiUrl(`/${id}`))
    //       );
    //   }

}
