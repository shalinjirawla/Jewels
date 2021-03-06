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
import {ProductsRoutingModule} from './products-routing.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter'; 
import { Ng2OrderModule } from 'ng2-order-pipe';
import {NgxPaginationModule} from 'ngx-pagination';
import { ServiceComponent } from './service/service.component';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ModalModule.forRoot(),
    TabsModule,
    ProductsRoutingModule,
    // CarouselModule.forRoot(),
    // CollapseModule.forRoot(),
    // PaginationModule.forRoot(),
    // PopoverModule.forRoot(),
    // ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    ReactiveFormsModule,
    DataTablesModule,
    Ng2SearchPipeModule,
    Ng2OrderModule,
    NgxPaginationModule,
  ],
  declarations: [
    ProductComponent,
    CategoriesComponent,
    BrandComponent,
    RawMaterialsComponent,
    ServiceComponent,
    
  ],
})
export class ProductsModule { }
