import { Injectable } from "@angular/core";
import { map, Observable, Subject } from "rxjs";
import { Filter } from "../models/filter.model";
import { PageResult } from "../models/page-result.model";
import { CreateOrUpdateUser, DeleteUserResult, User } from "../models/user.model";
import { QueryParamsUtils } from "../utilities/query-params";
import { BaseService } from "./base.service";
import { ApiResponse } from "../models/api-response.model";
import { Utils } from "../utilities/utils";

@Injectable({
    providedIn: 'root',
})
export class UserService extends BaseService {
    
    triggerListReload$ = new Subject();

    constructor() {
        super('user');
    }

    listCustomers(filters: Filter) {
        return this.list(filters, '/customer');
    }

    listAdmins(filters: Filter) {
        return this.list(filters, '/admin');
    }

    private list(filters: Filter, endpoint: string): Observable<PageResult<User>> {
        const filterQueryParams = QueryParamsUtils.toQueryString(filters);
        return this.handleRequest<PageResult<User>>(
            this.httpClient.get(this.extendApiUrl(`${endpoint}?${filterQueryParams}`))
        )
        .pipe(map(response => response.data!));
    }
    
    getById(id: string): Observable<ApiResponse<User>> {
        return this.handleRequest(
            this.httpClient.get(this.extendApiUrl(`/${id}`))
        );
    }

    createUser(user: CreateOrUpdateUser, role: 'Admin' | 'Customer') {
        return role === 'Admin' ? this.createAdmin(user) : this.createCustomer(user);
    }

    updateCustomer(id: string, record: CreateOrUpdateUser) {
        return this.handleRequest(
            this.httpClient.put(this.extendApiUrl(`/customer/${id}`), record)
        );
    }

    updateAdmin(id: string, record: CreateOrUpdateUser) {
        if (Utils.isNullOrWhitespace(record.password)) {
            Reflect.deleteProperty(record, 'password');
            Reflect.deleteProperty(record, 'passwordConfirmation');
        }
        return this.handleRequest(
            this.httpClient.put(this.extendApiUrl(`/admin/${id}`), record)
        );
    }

    createCustomer(user: CreateOrUpdateUser) {
        return this.handleRequest(
            this.httpClient.post(this.extendApiUrl('/customer'), user)
        );
    }

    createAdmin(user: CreateOrUpdateUser) {
        return this.handleRequest(
            this.httpClient.post(this.extendApiUrl('/admin'), user)
        );
    }

    deleteById(id: string) {
        return this.httpClient.delete<ApiResponse<DeleteUserResult>>(this.extendApiUrl(`/${id}`))
    }
}