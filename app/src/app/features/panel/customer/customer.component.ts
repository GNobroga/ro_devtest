import { Component } from '@angular/core';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { User } from '../../../core/models/auth.model';
import { UserService } from '../../../core/services/user.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { mergeAll, of, takeUntil } from 'rxjs';
import { Filter } from '../../../core/models/filter.model';

@Component({
  selector: 'app-customer',
  standalone: false,
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.scss'
})
export class CustomerComponent  extends BaseListComponent<User> {

   constructor(readonly service: UserService, readonly confirmationService: ConfirmationService, readonly messageService: MessageService) {
        super();
      }
  
      override ngOnInit(): void {
        super.ngOnInit();
  
        this.service.triggerListReload$.asObservable()
          .pipe(takeUntil(this.destroy$))
          .subscribe(() => {
            this.resetKeyword();
            this.loadData();
          });
        
        of(of(this.filters), this.filterChanged$.asObservable().pipe(takeUntil(this.destroy$)))
          .pipe(mergeAll())
          .subscribe(this.loadData.bind(this))
      }
  
      loadData(filters?: Filter) {
        filters ??= this.filters;
        this.execute(this.service.listCustomers(filters));
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
                  this.loadData();
                });
            },
        });
    }
}
