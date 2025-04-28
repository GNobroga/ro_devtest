import { Component, inject, OnDestroy, OnInit } from "@angular/core";
import { debounceTime, finalize, Observable, Subject, take, takeUntil, tap } from "rxjs";
import { Filter } from "../models/filter.model";
import { PageResult } from "../models/page-result.model";
import { PaginatorState } from "primeng/paginator";
import { Utils } from "../utilities/utils";
import { FormControl } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";

@Component({ 
    selector: 'app-base-list',
    template: '',
})
export abstract class BaseListComponent<T> implements OnInit, OnDestroy {
    
    protected activatedRoute = inject(ActivatedRoute);

    protected items: T[] = [];

    protected destroy$ = new Subject();

    protected keyword = new FormControl('');

    isLoading = false;

    protected filters: Filter = {
        keyword: '',
        page: 1,
        pageSize: 5,
        sort: 'asc',
        sortBy: 'id',
    };

    protected pageResult!: PageResult<T>;

    protected filterChanged$ = new Subject<Filter>();

    execute(obs$: Observable<PageResult<T>>) {
        this.isLoading = true;
        obs$.pipe(
            takeUntil(this.destroy$), finalize(() => this.isLoading = false))
            .subscribe(this.setItems.bind(this));
    }

    ngOnInit(): void {
        this.keyword.valueChanges.pipe(debounceTime(200), takeUntil(this.destroy$))
            .subscribe(this.setKeyword.bind(this));
    }

    ngOnDestroy(): void {
        this.destroy$.next(true);
    }

    setPagination(page: number, pageSize: number) {
        this.filters = { ...this.filters, page, pageSize };
        this.filterChanged$.next(this.filters);
    }

    setItems(pageResult: PageResult<T>) {
        this.items = pageResult.items;
        this.pageResult = pageResult;
    }

    setKeyword(keyword: string | null) {
        this.filters = { ...this.filters, keyword: keyword! };
        this.filterChanged$.next(this.filters);
    }

    resetKeyword() {
        this.keyword.setValue('');
    }

    onPageChange(event: PaginatorState) {
        const page = (event.page ?? 0) + 1;
        const pageSize = (event.rows ?? 100);
        this.setPagination(page, pageSize);
    }
}

