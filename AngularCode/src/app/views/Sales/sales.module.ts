// Angular
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { CustomersRoutingModule } from '../Sales/sales-routing.module';
import { QuotationsComponent } from './quotations/quotations.component';
@NgModule({
  declarations: [
    SalesOrderComponent,
    QuotationsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    CustomersRoutingModule,
    ModalModule.forRoot(),
     TabsModule,
     ReactiveFormsModule,
     TooltipModule.forRoot(),
  ]
})
export class SalesModule { }