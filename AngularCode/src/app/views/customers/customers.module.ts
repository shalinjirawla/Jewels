// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CustomerComponent } from './customer/customer.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
// import { CardsComponent } from './cards.component';

// // Forms Component


// // Components Routing
 import { CustomersRoutingModule } from './customers-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CustomersRoutingModule,
    ModalModule.forRoot(),
    // BsDropdownModule.forRoot(),
     TabsModule,
    // CarouselModule.forRoot(),
    // CollapseModule.forRoot(),
    // PaginationModule.forRoot(),
    // PopoverModule.forRoot(),
    // ProgressbarModule.forRoot(),
     TooltipModule.forRoot()
  ],
  declarations: [
  CustomerComponent,
]
})
export class CustomerModule { }
