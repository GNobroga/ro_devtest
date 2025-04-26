import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { catchError, delay, finalize, map, Observable, pipe, throwError } from "rxjs";
import { environment } from "../../../environments/environment";
import { ApiResponse } from "../models/api-response.model";
import { initialize } from "../rxjs/initialize";
import { MessageService } from "primeng/api";
import { ToastrService } from "ngx-toastr";

@Injectable()
export abstract class BaseService {

    isLoading = signal(false);

    errors = signal('');

    protected httpClient = inject(HttpClient);

    protected toastrService = inject(ToastrService);

    constructor(
        protected endpoint: string
    ) {}

    get apiUrl() {
        return `${environment.apiBaseUrl}/${this.endpoint}`
    }

    protected extendApiUrl(endpoint: string[] | string) {
        const path = Array.isArray(endpoint) ? endpoint.join('/') : endpoint;
        return `${this.apiUrl}${path}`;
    }

    protected handleRequest<T>(request$: Observable<any>): Observable<ApiResponse<T>> {
        return request$
            .pipe(
                initialize(() => this.isLoading.set(true)),
                delay(100),
                catchError((err: HttpErrorResponse) => {
                    const response = err.error as ApiResponse<any>;
                    const statusCode = response?.statusCode;
                    const toastr = (statusCode === 400) ? this.toastrService.warning : this.toastrService.error;

                    if (Object.hasOwn(response, 'messages')) {
                        const messages = response['messages'] ?? [];
                        for (let message of messages) {
                           toastr.bind(this.toastrService)(message);
                        }
                    }

                    return throwError(() => err);
                }),
                finalize(() => this.isLoading.set(false))
        );
    }
}