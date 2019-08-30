import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SalesOrderComponent } from './sales-order/sales-order.component';

const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Sales'
      },
      children: [
        {
          path: '',
          redirectTo: 'saleOrder'
        },
        {
          path: 'saleOrder',
          component: SalesOrderComponent,
          data: {
            title: 'Sales'
          }
        },
        
       
      ]
    }
  ];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class CustomersRoutingModule {}