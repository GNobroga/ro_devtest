import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer.component';

const routes: Routes = [
   {
      path: '',
      component: CustomerComponent,
      children: [
        // {
        //   path: 'new',
        //   component: ProductEditorComponent,
        // },
        // {
        //   path: ':id/edit',
        //   component: ProductEditorComponent,
        // }
      ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
