// Angular
import { CommonModule } from '@angular/common';
import { FormsModule , ReactiveFormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';
import { CustomerComponent } from './customer/customer.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import {FirstCharcaterUpperCase} from './../_helpers/custome-pipe';
// import { CardsComponent } from './cards.component';

// // Forms Component


// // Components Routing
 import { CustomersRoutingModule } from './customers-routing.module';
import { CustomerTypeComponent } from './CustomerType/customer-type/customer-type.component';
import { from } from 'rxjs';

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
     TooltipModule.forRoot(),
     ReactiveFormsModule
  ],
  declarations: [
  CustomerComponent,
  CustomerTypeComponent,
  FirstCharcaterUpperCase,
]
})
export class CustomerModule { }
