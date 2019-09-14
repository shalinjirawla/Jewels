import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {SupplierComponent} from './supplier/supplier.component';
import { AuthGuardService} from './../../shared/guards/auth-guard.service';

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
        canActivate: [AuthGuardService],
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
