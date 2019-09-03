// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductComponent } from './product/product.component';
import { CategoriesComponent } from './categories/categories.component';
import { DataTablesModule } from 'angular-datatables';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ProductsRoutingModule,
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
    ProductComponent,
    CategoriesComponent,
  ]
})
export class ProductsModule { }
