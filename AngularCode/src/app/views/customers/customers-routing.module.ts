import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerComponent } from './customer/customer.component';
import {CustomerTypeComponent} from './CustomerType/customer-type/customer-type.component';
import { AuthGuardService} from './../../shared/guards/auth-guard.service';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Customers'
    },
    children: [
      {
        path: '',
        canActivate: [AuthGuardService],
        redirectTo: 'customer'
      },
      {
        path: 'customer',
        component: CustomerComponent,
        canActivate: [AuthGuardService],
        data: {
          title: 'Customer'
        }
      },
      {
        path: 'customergroup',
        component: CustomerTypeComponent,
        canActivate: [AuthGuardService],
        data: {
          title: 'Customer Group'
        }
      },
      {
        path: 'forms',
      //  component: FormsComponent,
        data: {
          title: 'Forms'
        }
      },
      {
        path: 'switches',
       // component: SwitchesComponent,
        data: {
          title: 'Switches'
        }
      },
      {
        path: 'tables',
      //  component: TablesComponent,
        data: {
          title: 'Tables'
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
