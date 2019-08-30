import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerComponent } from './customer/customer.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Customers'
    },
    children: [
      {
        path: '',
        redirectTo: 'customer'
      },
      {
        path: 'customer',
        component: CustomerComponent,
        data: {
          title: 'Customer'
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
export class ProductRoutingModule {}
