import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { formatDate } from '@angular/common';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss']
})
export class SalesOrderComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  @ViewChild('largeModal1', { static: false }) public largeModal1: ModalDirective;
  constructor(private formBuilder: FormBuilder) { }
  //Form List stat
  SalesOrderDetailsForm: FormGroup;
  SalesOrderTypeForm: FormGroup;
  //Form List end

  //btn submiited start
  SalesOrderTabSubmit: boolean = false;
  SalesOrderTabSubmitsecond: boolean = false;
  firsttab: boolean = true;
  secondtab: boolean = false;
  thirdtab: boolean = false;
  SalesOrderTypeTabSubmit: boolean = false;
  //btn submitted end

  SaleTitle: string = "Add New Sales Order";
  SavebtnTitle: string = "Save & Next";
  CurrentDate: Date;
  AddressTypeTiltle: string = "SHIPPING ADDRESS";
  ngOnInit() {
    this.CurrentDate = new Date();
    this.onLoad();
  }
  public onLoad() {
    this.SalesOrderDetailsForm = this.formBuilder.group({
      FormCompany: ['', Validators.required],
      DateOrdered: ['', Validators.required],
      EstimatedDeliveryDate: ['', Validators.required],
      CustomerId: ['', Validators.required],
      CustomerPurchesOrderNumber: [''],
      Remarks: [''],
    });
    this.SalesOrderTypeForm = this.formBuilder.group({
      SalesOrderType: ['', Validators.required],
      Creditterms: [''],
      ShippingMethod: [''],
      SalesRep: [''],
      Currency: [''],
    });
  }

  public TabChange(event: any) {
    debugger
  }
  public SaveSalesOrder(SalesOrderDetailsForm: FormControl, SalesOrderTypeForm: FormControl) {
    debugger
    this.SalesOrderTabSubmit = true;
    if (this.SalesOrderDetailsForm.invalid) {
      document.getElementById("TabOrderDetail-link").click();
      return;
    }
    if (document.getElementById('TabOrderDetail-link').className == "nav-link active") {
      document.getElementById("SaleOrderType-link").click();
      return

    } else if (document.getElementById('SaleOrderType-link').className == 'active') {

    }
    this.SalesOrderTabSubmitsecond = true;
    if (this.SalesOrderTypeForm.invalid) {
      return;
    }

  }

  get f() { return this.SalesOrderDetailsForm.controls; }
  get f1() { return this.SalesOrderTypeForm.controls; }
}
