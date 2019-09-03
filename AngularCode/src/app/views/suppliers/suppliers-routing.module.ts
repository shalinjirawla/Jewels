import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {SupplierComponent} from './supplier/supplier.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Suppliers'
    },
    children: [
      {
        path: '',
        redirectTo: 'supplier'
      },
      {
        path: 'supplier',
        component: SupplierComponent,
        data: {
          title: 'Supplier'
        }
      },
      
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuppliersRoutingModule { }
