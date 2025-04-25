import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { Product } from './product.model';
import { PaginatorState } from 'primeng/paginator';
import { ProductService } from './product.service';
import { Filter } from '../../../core/models/filter.model';
import { finalize, merge, mergeAll, of, takeUntil } from 'rxjs';
import { initialize } from '../../../core/rxjs/initialize';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  providers: [],
  standalone: false,
})
export class ProductComponent extends BaseListComponent<Product>{

    constructor(readonly service: ProductService, readonly confirmationService: ConfirmationService, readonly messageService: MessageService) {
      super();
    }

    override ngOnInit(): void {
      super.ngOnInit();

      this.service.triggerListReload$.asObservable()
        .pipe(takeUntil(this.destroy$))
        .subscribe(() => this.loadData());
      
      of(of(this.filters), this.filterChanged$.asObservable().pipe(takeUntil(this.destroy$)))
        .pipe(mergeAll())
        .subscribe(this.loadData.bind(this))
    }

    loadData(filters?: Filter) {
      filters ??= this.filters;
      this.execute(this.service.list(filters));
    }

    confirmDeletion(event: Event, id: string) {
      this.confirmationService.confirm({
          target: event.target as EventTarget,
          message: 'Você tem certeza que deseja prosseguir?',
          header: 'Confirmação',
          closable: true,
          closeOnEscape: true,
          icon: 'pi pi-exclamation-triangle',
          rejectButtonProps: {
              label: 'Cancelar',
              severity: 'secondary',
              outlined: true,
          },
          acceptButtonProps: {
              label: 'Confirmar',
          },
          accept: () => {
            this.service.deleteById(id)
              .subscribe(() => {
                this.messageService.add({ severity: 'info', detail: '1 item foi removido' });
                this.service.triggerListReload$.next(true);
              });
          },
      });
  }

}