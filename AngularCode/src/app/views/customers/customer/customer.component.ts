import { Component, OnInit,ViewChild } from '@angular/core';
import {ModalDirective} from 'ngx-bootstrap/modal';
import {CustomerModel} from '../../../Models/Customer-Model';
import {CustomerService} from '../../../Services/customer.service';
import {FormControl,FormGroup,FormBuilder, Validators} from '@angular/forms';

import { map } from 'rxjs/operators';
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  @ViewChild('largeModal', {static: false}) public largeModal: ModalDirective;
  constructor(private customerservice:CustomerService,
      private formBuilder:FormBuilder) { }
  Result:any;
  AddressLenghtcount:boolean=false;
  AddCustomerForm:FormGroup;
  AddCustomerAddressForm:FormGroup;
  AddCustomerContactForm:FormGroup;
  CurrencyList:any;
  DiscountOptionValue=0;
  CustomerTypeList:any;
  DefaultFlag:boolean=true;
  Address:any;
  AddressList:any={ "Address": [] };
  ngOnInit() {
    this.onLoad();
    this.GetCurrencyList();
    this.GetCustomerType();
  }
  onLoad(){
    this.AddCustomerForm=this.formBuilder.group({
      CustomerId:[0],
      CustomerName:['' ],
      CustomerTypeId:['0'],
      CustomerCode:[''],
      Website:[''],
      TaxRegistrationNumber:[''],
      Remarks:[''],
      DefaultCreditTerms:[''],
      DefaultCreditLimit:[''],
      DiscountOption:['0'],
      DiscountAmount:[''],
      DiscountPercentage:[''],
      DefaultCurrency:['0'],

    })

    this.AddCustomerAddressForm=this.formBuilder.group({
      AddressId:[0],
      AddressType : ['0'],
      Address : [''],
      Country : ['0'],
      State : [''],
      City : [''],
      PostalCode : [''],
      DefaultAddress:[false],
    })

    this.AddCustomerContactForm=this.formBuilder.group({
      Designation:[''],
      Email:[''],
      FirstName:[''],
      LastName:[''],
      Mobile:[''],
      Fax:[''],
      Office:[''],
    })
  }
  ChangeDiscountvalue(value:any)
  {
    debugger
    if(value=="1"){
      this.DiscountOptionValue=1;
    }
    else if(value=="2"){
      this.DiscountOptionValue=2;
    }else{
      this.DiscountOptionValue=0;
    }

  }
  AddCustomer(AddCustomerForm:FormControl){
    debugger
    this.customerservice.AddCustomer(AddCustomerForm.value).subscribe((responce: any) => {
      debugger
      this.Result = responce;
      this.ngOnInit();
    });
  }
  AddCustomerAddress(AddCustomerAddressForm:FormControl){
    debugger
    if(this.DefaultFlag){
      AddCustomerAddressForm.value.DefaultAddress=true;
      this.DefaultFlag=false;
    }
    this.Address=AddCustomerAddressForm.value;
    if(this.Address!=undefined){
    this.AddressList.Address.push(this.Address);
    if(this.AddressList.Address.lenght!=0){
    this.AddressLenghtcount=true;
    this.Address=null;
  }
  }
  }
  AddNewAddress(){
    this.AddressLenghtcount=false;
    this.onLoad();
  }
  SetDeafult(valaue:any,i:any)
  {
    let a = this.AddressList.Address.map((result: any,index) => {
      if(result.DefaultAddress){
        this.AddressList.Address[index].DefaultAddress=false;
      } 
      if(i==index){
        this.AddressList.Address[index].DefaultAddress=true;
      }
    })
  }

  EditAddress(i:any){
    let a = this.AddressList.Address.map((result: any,index) => {
      if(i==index){
        this.AddCustomerAddressForm.patchValue({
          AddressType:result.AddressType,
          Address : result.Address,
          Country : result.Country,
          State : result.State,
          City : result.City,
          PostalCode : result.PostalCode,
          DefaultAddress:result.DefaultAddress,
        })
      }
    })
    this.AddressLenghtcount=false;
  }

  GetCurrencyList(){
    this.customerservice.GetCurrency().subscribe((responce: any) => {
      this.CurrencyList = responce.body.data;
    });
  }
  GetCustomerType(){
    this.customerservice.GetCustomerType().subscribe((responce: any) => {
      this.CustomerTypeList = responce.body.data;
    });
  }
  
}
