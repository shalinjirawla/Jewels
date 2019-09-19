// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { SuppliersRoutingModule } from './suppliers-routing.module';
import { SupplierComponent } from './supplier/supplier.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter'; 
import { Ng2OrderModule } from 'ng2-order-pipe';
import {NgxPaginationModule} from 'ngx-pagination';
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
    Ng2SearchPipeModule,
    Ng2OrderModule,
    NgxPaginationModule,
  ],
  declarations: [
  SupplierComponent]
})
export class SuppliersModule { }
