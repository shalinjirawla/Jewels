import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { AuthGuardService } from './../../shared/guards/auth-guard.service';
import{ReceiveNotesComponent} from './receive-notes/receive-notes.component';
const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Purchases'
        },
        children: [
            {
                path: '',
                redirectTo: 'purchases-order'
            },
            {
                path: 'purchases-order',
                component: PurchaseOrderComponent,
                canActivate: [AuthGuardService],
                data: {
                    title: 'Purchases Order'
                }
            },
            {
              path: 'receive-notes',
               component: ReceiveNotesComponent,
              canActivate: [AuthGuardService],
              data: {
                title: 'Receive Notes'
              }
            },

        ]
    }
];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PurchasesRoutingModule { }