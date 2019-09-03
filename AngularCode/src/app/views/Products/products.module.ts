// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ProductComponent } from './product/product.component';
import { CategoriesComponent } from './categories/categories.component';
import { DataTablesModule } from 'angular-datatables';
import { RawMaterialsComponent } from './raw-materials/raw-materials.component';
import {BrandComponent} from './brand/brand.component';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
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
    BrandComponent,
    RawMaterialsComponent,
  ]
})
export class ProductsModule { }
