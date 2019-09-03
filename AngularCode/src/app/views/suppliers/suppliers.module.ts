// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { SuppliersRoutingModule } from './suppliers-routing.module';

import { DataTablesModule } from 'angular-datatables';
import { SupplierComponent } from './supplier/supplier.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SuppliersRoutingModule,
    ModalModule.forRoot(),
    // BsDropdownModule.forRoot(),
    TabsModule,
    // CarouselModule.forRoot(),
    // CollapseModule.forRoot(),
    // PaginationModule.forRoot(),
    // PopoverModule.forRoot(),
    // ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    ReactiveFormsModule,
    DataTablesModule
  ],
  declarations: [
  SupplierComponent]
})
export class SuppliersModule { }
