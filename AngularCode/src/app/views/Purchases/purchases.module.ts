// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
// import { SalesOrderComponent } from './sales-order/sales-order.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PurchasesRoutingModule } from '../Purchases/purchases-routing.module';
import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { DataTablesModule } from 'angular-datatables';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { NgxPaginationModule } from 'ngx-pagination';
import { ReceiveNotesComponent } from './receive-notes/receive-notes.component';
// import { QuotationsComponent } from './quotations/quotations.component';
@NgModule({
    
    imports: [
        CommonModule,
        FormsModule,
        PurchasesRoutingModule,
        ModalModule.forRoot(),
        TabsModule,
        ReactiveFormsModule,
        TooltipModule.forRoot(),
        DataTablesModule,
        Ng2SearchPipeModule,
        Ng2OrderModule,
        NgxPaginationModule,
    ],
    declarations: [PurchaseOrderComponent, ReceiveNotesComponent]
})
export class PurchasesModule { }