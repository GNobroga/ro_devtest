import { Injectable, signal } from "@angular/core";
import { BaseService } from "../../../core/services/base.service";
import { CreateProduct, CreateProductResult, DeleteProductResult, Product, UpdateProduct, UpdateProductResult } from "./product.model";
import { BehaviorSubject, filter, map, Observable, Subject } from "rxjs";
import { ApiResponse } from "../../../core/models/api-response.model";
import { Filter } from "../../../core/models/filter.model";
import { QueryParamsUtils } from "../../../core/utilities/query-params";
import { PageResult } from "../../../core/models/page-result.model";

@Injectable()
export class ProductService extends BaseService {

    triggerListReload$ = new Subject();

    private keywordSubject = new BehaviorSubject<string>(''); 
    keyword$ = this.keywordSubject.asObservable();

    constructor() {
        super('product');
    }

    setKeyword(keyword: string) {
        this.keywordSubject.next(keyword);
    }

    list(filters: Filter): Observable<PageResult<Product>> {
        const filterQueryParams = QueryParamsUtils.toQueryString(filters);
        return this.handleRequest<PageResult<Product>>(
            this.httpClient.get(this.extendApiUrl(`?${filterQueryParams}`))
        )
        .pipe(map(response => response.data!));
    }

    getById(id: string): Observable<ApiResponse<Product>> {
        return this.handleRequest(
            this.httpClient.get(this.extendApiUrl(`/${id}`))
        );
    }
    
    create(record: CreateProduct): Observable<ApiResponse<CreateProductResult>> {
        return this.handleRequest(
            this.httpClient.post(this.apiUrl, record)
        );
    }

    update(id: string, record: UpdateProduct): Observable<ApiResponse<UpdateProductResult>> {
        return this.handleRequest(
            this.httpClient.put(this.extendApiUrl(`/${id}`), record),
        );
    }

    deleteById(id: string): Observable<ApiResponse<DeleteProductResult>> {
        return this.httpClient.delete<ApiResponse<DeleteProductResult>>(this.extendApiUrl(`/${id}`))
    }
}