// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { MastersRoutingModule } from './masters-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { GeneralSetupComponent } from './general-setup/general-setup.component';
import { TestComponent } from './test/test.component';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MastersRoutingModule,
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
  GeneralSetupComponent,
  TestComponent,]
})
export class MastersModule { }
