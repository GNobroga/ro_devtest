import { Injectable } from '@angular/core';
import { BaseService } from '../../../core/services/base.service';
import { OrderStatus } from './order.enum';
import { map, Observable, Subject } from 'rxjs';
import { ChangeOrderStatusResult, CreateOrUpdateOrder, DeleteOrderResult, Order, OrderSummary } from './order.model';
import { Filter } from '../../../core/models/filter.model';
import { PageResult } from '../../../core/models/page-result.model';
import { QueryParamsUtils } from '../../../core/utilities/query-params';
import { ApiResponse } from '../../../core/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends BaseService {

  constructor() {
    super('order');
  }

  triggerListReload$ = new Subject();

  getSummary(startDate: Date, endDate: Date, status?: OrderStatus): Observable<OrderSummary> {
    return this.handleRequest<OrderSummary>(
      this.httpClient.post(this.extendApiUrl('/summary'), { startDate, endDate, status })
    ).pipe(map(r => r.data!));
  }

  private list(filters: Filter, endpoint = ''): Observable<PageResult<Order>> {
      const filterQueryParams = QueryParamsUtils.toQueryString(filters);
      return this.handleRequest<PageResult<Order>>(
          this.httpClient.get(this.extendApiUrl(endpoint + '?' + filterQueryParams))
      )
      .pipe(map(response => response.data!));
  }

  listAll(filters: Filter) {
    return this.list(filters);
  }

  listByUser(filters: Filter): Observable<PageResult<Order>> {
    return this.list(filters, '/me');
  }

  getById(id: string): Observable<ApiResponse<Order>> {
      return this.handleRequest(
          this.httpClient.get(this.extendApiUrl(`/${id}`))
      );
  }
      
  create(record: CreateOrUpdateOrder): Observable<ApiResponse<Order>> {
      return this.handleRequest(
          this.httpClient.post(this.apiUrl, record)
      );
  }
  
    //   update(id: string, record: UpdateProduct): Observable<ApiResponse<UpdateProductResult>> {
    //       return this.handleRequest(
    //           this.httpClient.put(this.extendApiUrl(`/${id}`), record),
    //       );
    //   }

    changeOrderStatus(id: string, status: OrderStatus): Observable<ApiResponse<ChangeOrderStatusResult>> {
      return this.handleRequest(
        this.httpClient.put(this.extendApiUrl(`/${id}/status`), { status })
      );
    }

    deleteById(id: string): Observable<ApiResponse<DeleteOrderResult>> {
        return this.httpClient.delete<ApiResponse<DeleteOrderResult>>(this.extendApiUrl(`/${id}`));
    }

}
