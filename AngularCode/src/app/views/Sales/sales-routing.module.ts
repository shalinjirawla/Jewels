import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SalesOrderComponent } from './sales-order/sales-order.component';
import { AuthGuardService} from './../../shared/guards/auth-guard.service';

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
          canActivate: [AuthGuardService],
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