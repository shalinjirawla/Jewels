import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CustomerModel } from '../../../Models/Customer-Model';
import { CustomerService } from '../../../Services/customer.service';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { map } from 'rxjs/operators';
import { nextTick } from 'q';
import { JsonPipe } from '@angular/common';
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private customerservice: CustomerService,
    private formBuilder: FormBuilder) { }
  Result: any;
  savebtntitle: string = "Save & Next";
  copyAddress: string;
  BillingShippingAddress: boolean = false;
  submitted: boolean = false;
  contactsubmitted: boolean = false;
  Addresssubmitted: boolean = false;

  AddressLenghtcount: boolean = false;
  ContactLenghtcount: boolean = false;

  AddCustomerForm: FormGroup;
  AddCustomerAddressForm: FormGroup;
  AddCustomerContactForm: FormGroup;

  ContactDefaultFlag: boolean = true;
  DefaultFlag: boolean = true;
  CheckboxFlag: boolean = false;

  CurrencyList: any;
  CountryList: any;
  CustomerTypeList: any;
  DiscountTypeList: any;

  secondAddress: any;
  NewCondAddress: any;

  Address: any;
  Contact: any;
  ContactList: any = { "Contact": [] };
  AddressList: any = { "Address": [] };
  AddCustomervalue: any = [];
  ngOnInit() {
    this.onLoad();
    this.GetCurrencyList();
    this.GetCustomerType();
    this.GetCountry();
    this.GetDiscountType();
  }
  onLoad() {
    this.AddCustomerForm = this.formBuilder.group({
      customerId: [0],
      customerName: ['', Validators.required],
      customerTypeId: [''],
      customerCode: [''],
      website: [''],
      taxRegistrationNumber: [''],
      remarks: [''],
      defaultCreditTerms: [''],
      defaultCreditLimit: [''],
      discountOption: ['0'],
      discountAmount: [''],
      discountPercentage: [''],
      defaultCurrency: ['0'],

    })

    this.AddCustomerAddressForm = this.formBuilder.group({
      addressId: ['0'],
      addressType: ['0'],
      address: ['', Validators.required],
      countryId: ['0'],
      state: [''],
      city: [''],
      postalCode: [''],
      defaultAddress: [false],
    })

    this.AddCustomerContactForm = this.formBuilder.group({
      contactId: ['0'],
      designation: [''],
      email: [''],
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: ['', Validators.maxLength(10)],
      fax: [''],
      office: [''],
      defaultContact: [false],

    })
  }

  get f() { return this.AddCustomerForm.controls; }
  get con() { return this.AddCustomerContactForm.controls; }
  get add() { return this.AddCustomerAddressForm.controls; }

  AddCustomer(AddCustomerForm: FormControl) {
    this.submitted = true;
    if (this.AddCustomerForm.invalid) {
      document.getElementById("customerDetail-link").click();
      return;
    }
    this.submitted = false;
    if (document.getElementById("customerDetail-link").className == "nav-link active") {
      document.getElementById("customerAddress-link").click();
      return;
    }
    else if (document.getElementById("customerAddress-link").className == "nav-link active") {
      document.getElementById("customerContact-link").click();
      this.savebtntitle = "Save";
      return;
    }
    this.AddCustomervalue.CustomerDetail = AddCustomerForm.value;
    this.AddCustomervalue.AddressList = this.AddressList;
    this.AddCustomervalue.ContactList = this.ContactList;
    this.customerservice.AddCustomer(this.AddCustomervalue).subscribe((responce: any) => {
      debugger
      this.Result = responce;
      //responce.body.status
      this.onLoad();
      this.CheckboxFlag = false;
      this.Addresssubmitted = false;
      this.AddressLenghtcount = true;
      this.Address = null;
      this.ContactList = { "Contact": [] };
      this.AddressList = { "Address": [] };
    });
  }
  CheckAddress(event) {
    this.BillingShippingAddress = event;
  }
  changeaddresstype(event) {
    debugger
    if (event == "1") {
      this.CheckboxFlag = true;
      this.copyAddress = "Shiping Address is same as Billing Address"
    }
    else if (event == "2") {
      this.CheckboxFlag = true;
      this.copyAddress = "Billing Address is same as Shiping Address"
    }
    else {
      this.CheckboxFlag = false;
      // document.getElementById("copyaddress").checked = false;
    }
  }
  AddCustomerAddress(AddCustomerAddressForm: FormControl) {
    this.Addresssubmitted = true;
    if (this.AddCustomerAddressForm.invalid) {
      return;
    }
    this.Addresssubmitted = false;
    if (AddCustomerAddressForm.value.addressId == 0) {

      if (this.DefaultFlag) {
        AddCustomerAddressForm.value.defaultAddress = true;
        this.DefaultFlag = false;
      }
      let a = JSON.stringify(AddCustomerAddressForm.value);
      this.secondAddress = JSON.parse(a);
      AddCustomerAddressForm.value.addressId = '_' + Math.random().toString(36).substr(2, 9);
      this.Address = AddCustomerAddressForm.value;

      if (this.Address != undefined) {
        this.AddressList.Address.push(this.Address);
        if (!this.BillingShippingAddress) {
          if (this.AddressList.Address.lenght != 0) {
            this.CheckboxFlag = false;
            this.Addresssubmitted = false;
            this.AddressLenghtcount = true;
            this.Address = null;
            this.ContactLenghtcount = true;
            this.Contact = null;
          }
        }
        else {
          this.BillingShippingAddress = false;
          this.secondAddress.addressId = '_' + Math.random().toString(36).substr(2, 9) + "_";
          if (this.Address.addressType == "1") {
            this.secondAddress.addressType = "2";
          }
          else if (this.Address.addressType == "2") {
            this.secondAddress.addressType = "1";
          }
          this.secondAddress.defaultAddress = false;
          // document.getElementById("copyaddress").checked = false;
          this.AddressList.Address.push(this.secondAddress);
          if (this.AddressList.Address.lenght != 0) {
            this.CheckboxFlag = false;
            this.Addresssubmitted = false;
            this.secondAddress = null;
            this.AddressLenghtcount = true;
            this.Address = null;
          }
        }
      }

    }
    else {
      var elementPos = this.AddressList.Address.map(function (x) { return x.addressId; }).indexOf(AddCustomerAddressForm.value.addressId);
      this.Address = AddCustomerAddressForm.value;
      var objectFound = this.AddressList.Address[elementPos];

      this.AddressList.Address[elementPos] = this.Address;
      if (this.AddressList.Address.lenght != 0) {
        this.AddressLenghtcount = true;
        this.Address = null;
      }

    }
  }
  AddNewAddress() {
    this.AddressLenghtcount = false;
    this.onLoad();
  }
  CancelAddCustomerAddress() {
    this.AddressLenghtcount = true;
    this.onLoad();
  }
  SetDeafult(valaue: any, i: any) {
    let a = this.AddressList.Address.map((result: any, index) => {
      if (result.defaultAddress) {
        this.AddressList.Address[index].defaultAddress = false;
      }
      if (i == index) {
        this.AddressList.Address[index].defaultAddress = true;
      }
    })
  }

  EditAddress(i: any) {
    let a = this.AddressList.Address.map((result: any, index) => {
      if (i == index) {
        this.AddCustomerAddressForm.patchValue({
          addressId: result.addressId,
          addressType: result.addressType,
          address: result.address,
          countryId: result.countryId,
          state: result.state,
          city: result.city,
          postalCode: result.postalCode,
          defaultAddress: result.defaultAddress,
        })
      }
    })
    this.AddressLenghtcount = false;
  }

  DeleteAddress(i: any) {
    let a = this.AddressList.Address.map((result: any, index) => {
      if (i == index) {
        var elementPos = this.AddressList.Address.map(function (x) { return x.addressId; }).indexOf(result.addressId);
        this.AddressList.Address.splice(elementPos, 1);
        let setDefaultAddress = false;
        let a = this.AddressList.Address.map((result: any, index) => {
          if (result.defaultAddress) {
            setDefaultAddress = true;
          }

        })
        if (!setDefaultAddress) {
          if (this.AddressList.Address.length != 0) {
            this.AddressList.Address[0].defaultAddress = true;
          }
        }
        if (this.AddressList.Address.length == 0) {
          this.DefaultFlag = true;
          this.CheckboxFlag = true;
          this.AddressLenghtcount = false;
          this.onLoad();
        }
      }
    })

  }

  AddCustomerContact(AddCustomerContactForm: FormControl) {
    this.contactsubmitted = true;
    if (this.AddCustomerContactForm.invalid) {
      return;
    }
    this.contactsubmitted = false;
    if (AddCustomerContactForm.value.contactId == 0) {
      if (this.ContactDefaultFlag) {
        AddCustomerContactForm.value.defaultContact = true;
        this.ContactDefaultFlag = false;
      }
      AddCustomerContactForm.value.contactId = '_' + Math.random().toString(36).substr(2, 9);
      this.Contact = AddCustomerContactForm.value;
      if (this.Contact != undefined) {
        this.ContactList.Contact.push(this.Contact);
        if (this.ContactList.Contact.lenght != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
      }
    }
    else {
      var elementPos = this.ContactList.Contact.map(function (x) { return x.contactId; }).indexOf(AddCustomerContactForm.value.contactId);
      this.Contact = AddCustomerContactForm.value;
      var objectFound = this.ContactList.Contact[elementPos];

      this.ContactList.Contact[elementPos] = this.Contact;
      if (this.ContactList.Contact.lenght != 0) {
        this.ContactLenghtcount = true;
        this.Contact = null;
      }

    }
  }

  EditContact(i: any) {
    let a = this.ContactList.Contact.map((result: any, index) => {
      if (i == index) {
        this.AddCustomerContactForm.patchValue({
          contactId: result.contactId,
          designation: result.designation,
          email: result.email,
          firstName: result.firstName,
          lastName: result.lastName,
          mobile: result.mobile,
          defaultContact: result.defaultContact,
          fax: result.fax,
          office: result.office
        })
      }
    })
    this.ContactLenghtcount = false;
  }

  SetDeafultContact(valaue: any, i: any) {
    let a = this.ContactList.Contact.map((result: any, index) => {
      if (result.defaultContact) {
        this.ContactList.Contact[index].defaultContact = false;
      }
      if (i == index) {
        this.ContactList.Contact[index].defaultContact = true;
      }
    })
  }

  DeleteContact(i: any) {
    let a = this.ContactList.Contact.map((result: any, index) => {
      if (i == index) {
        var elementPos = this.ContactList.Contact.map(function (x) { return x.contactId; }).indexOf(result.contactId);
        this.ContactList.Contact.splice(elementPos, 1)
        if (this.ContactList.Contact.lenght == undefined) {
          this.ContactLenghtcount = false;
          this.onLoad();
        }
      }
    })
  }

  AddNewContact() {
    this.ContactLenghtcount = false;
    this.onLoad();
  }

  ResetForm() {
    this.largeModal.hide();
    this.onLoad();
    this.CheckboxFlag = false;
    this.ContactLenghtcount = false;
    this.Addresssubmitted = false;
    this.AddressLenghtcount = true;
    this.Address = null;
    this.DefaultFlag = true;
    this.AddressLenghtcount = false;
    this.ContactList = { "Contact": [] };
    this.AddressList = { "Address": [] };
    document.getElementById("customerDetail-link").click();
  }

  CancelAddCustomerContact() {
    this.ContactLenghtcount = true;
    this.onLoad();
  }

  GetCurrencyList() {
    this.customerservice.GetCurrency().subscribe((responce: any) => {
      this.CurrencyList = responce.body.data;
    });
  }

  GetCustomerType() {
    this.customerservice.GetCustomerType().subscribe((responce: any) => {
      this.CustomerTypeList = responce.body.data;
    });
  }

  GetCountry() {
    this.customerservice.GetCountry().subscribe((responce: any) => {
      this.CountryList = responce.body.data;
    });
  }

  GetDiscountType() {
    this.customerservice.GetDiscountType().subscribe((responce: any) => {
      this.DiscountTypeList = responce.body.data;
    });
  }
}
