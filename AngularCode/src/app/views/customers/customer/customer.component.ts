import { Component, OnInit, ViewChild, AbstractType } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CustomerService } from '../../../Services/Customer-Services/customer.service'
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';
import { nextTick } from 'q';
import { JsonPipe } from '@angular/common';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
import * as $ from 'jquery';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})
const reg = /^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+(\.[a-z]{2,}){1,3}(#?\/?[a-zA-Z0-9#]+)*\/?(\?[a-zA-Z0-9-_]+=[a-zA-Z0-9-%]+&?)?$/;


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
  modeltitle: string = "Add Customer";
  addcontacttitle: string = "Add";
  addaddresstitle: string = "Add";
  countryCode: string = "";


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
  LoderoutCustomer: boolean = true;
  IsDefaultAddressDelete: boolean = false;
  CurrencyList: any;
  CountryList: any;
  CustomerTypeList: any;
  DiscountTypeList: any;
  CreditTermsList: any;

  CustomerList: any = { "Customer": [] };

  secondAddress: any;
  NewCondAddress: any;
  Address: any;
  Contact: any;
  ContactList: any = { "Contact": [] };
  AddressList: any = { "Address": [] };
  AddCustomervalue: any = [];
  ngOnInit() {
   
    this.onLoad();
    this.GetCustomerType();
    this.GetCurrencyList();
    this.GetCountry();
    this.GetDiscountType();
    this.GetCreditTermsList();
    
  }
  
  onLoad() {
    this.AddCustomerForm = this.formBuilder.group({
      customerId: [0],
      customerName: ['', Validators.required],
      customerTypeId: [''],
      customerCode: [''],
      website: ['', Validators.pattern(reg)],
      taxRegistrationNumber: ['', [Validators.maxLength(9), Validators.minLength(9)]],
      remarks: [''],
      defaultCreditTerms: [''],
      defaultCreditLimit: [''],
      discountOption: ['0'],
      discountAmount: [''],
      defaultCurrency: [''],

    })

    this.AddCustomerAddressForm = this.formBuilder.group({
      addressId: ['0'],
      addressType: ['', Validators.required],
      address: ['', Validators.required],
      countryId: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      defaultAddress: [false],
    })

    this.AddCustomerContactForm = this.formBuilder.group({
      contactId: ['0'],
      CountryId: [0],
      designation: [''],
      email: ['', Validators.email],
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: ['', [Validators.maxLength(10), Validators.minLength(10)]],
      fax: ['', [Validators.maxLength(13), Validators.minLength(9)]],
      office: ['', [Validators.maxLength(13), Validators.minLength(8)]],
      defaultContact: [false],

    })
  }

  get f() { return this.AddCustomerForm.controls; }
  get con() { return this.AddCustomerContactForm.controls; }
  get add() { return this.AddCustomerAddressForm.controls; }

  addnewcustomer() {
    this.largeModal.show();
    this.onLoad();
  }
 
  allownumberwithdot(event: any) {
    const pattern = /[0-9\+\.]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
  allownumber(event: any) {
    const pattern = /[0-9]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  allowalpha(event: any) {
    const pattern = /[a-z\+\A-Z\+ +\a-z\+\A-Z+]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
  click(event){
    debugger
  }


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
      this.Result = responce;
      if (responce.status) {
        this.GetCustomerList();
        if (this.AddCustomerForm.value.customerId == 0) {
          Swal.fire(
            'Customer Add Successfully',
            responce.message,
            'success'
          )
        }
        else if (this.AddCustomerForm.value.customerId >= 0) {
          Swal.fire(
            'Customer Update Successfully',
            responce.message,
            'success'
          )
        }

      }
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
    });
  }
  CheckAddress(event) {
    this.BillingShippingAddress = event;
  }
  changeaddresstype(event) {
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
      let ele: any = document.getElementById("copyaddress");
      ele.checked = false;
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
        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (AddCustomerAddressForm.value.countryId == result.countryId) {
              this.countryCode ="+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        if (this.ContactList.Contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }

        this.DefaultFlag = false;
      }
      let a = JSON.stringify(AddCustomerAddressForm.value);
      this.secondAddress = JSON.parse(a);
      AddCustomerAddressForm.value.addressId = '_' + Math.random().toString(36).substr(2, 9);
      this.Address = AddCustomerAddressForm.value;

      if (this.Address != undefined) {
        this.AddressList.Address.push(this.Address);
        if (!this.BillingShippingAddress) {
          if (this.AddressList.Address.length != 0) {
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
          let elem: any = document.getElementById("copyaddress");
          elem.checked = false;
          this.AddressList.Address.push(this.secondAddress);
          if (this.AddressList.Address.length != 0) {
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
      if (this.Address.defaultAddress == true) {
        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (AddCustomerAddressForm.value.countryId == result.countryId) {
              this.countryCode = "+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        if (this.ContactList.Contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }
      }
      this.AddressList.Address[elementPos] = this.Address;
      if (this.AddressList.Address.length != 0) {
        this.AddressLenghtcount = true;
        this.Address = null;
      }

    }
  }
  AddNewAddress() {
    this.AddressLenghtcount = false;
    //this.onLoad();
    this.AddCustomerAddressForm = this.formBuilder.group({
      addressId: ['0'],
      addressType: ['', Validators.required],
      address: ['', Validators.required],
      countryId: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      defaultAddress: [false],
    })
    this.addaddresstitle = "Add";
  }
  CancelAddCustomerAddress() {
    if (this.AddressList.Address.length == 0) {
      this.AddressLenghtcount = false;
    } else {
      this.AddressLenghtcount = true;
    }
    this.AddCustomerAddressForm = this.formBuilder.group({
      addressId: ['0'],
      addressType: ['', Validators.required],
      address: ['', Validators.required],
      countryId: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      defaultAddress: [false],
    })
  }
  SetDeafult(valaue: any, i: any) {
    let AnyDefaultSet: boolean = false;
    let a = this.AddressList.Address.map((result: any, index) => {
      if (i == index && !valaue) {
        this.AddressList.Address[index].defaultAddress = true;

        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (this.AddressList.Address[index].countryId  == result.countryId) {
              this.countryCode = "+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        if (this.ContactList.Contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }
        AnyDefaultSet = true;
      } else {
        this.AddressList.Address[index].defaultAddress = false;
        if (!AnyDefaultSet) {
          this.countryCode = "";
        }
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
    this.addaddresstitle = "Update";
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
      if (this.ContactList.Contact.length == 0) {
        this.ContactDefaultFlag = true;
        AddCustomerContactForm.value.defaultContact = true;
        this.ContactDefaultFlag = false;
      }
      if (this.ContactDefaultFlag) {

        AddCustomerContactForm.value.defaultContact = true;
        this.ContactDefaultFlag = false;
      }
      AddCustomerContactForm.value.contactId = '_' + Math.random().toString(36).substr(2, 9);
      this.Contact = AddCustomerContactForm.value;
      if (this.Contact != undefined) {
        this.ContactList.Contact.push(this.Contact);
        if (this.ContactList.Contact.length != 0) {
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
      if (this.ContactList.Contact.length != 0) {
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
    this.addcontacttitle = "Update";
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
        if (this.ContactList.Contact.length == 0) {
          this.ContactLenghtcount = false;
          this.onLoad();
        }
      }
    })
  }

  AddNewContact() {
    this.ContactLenghtcount = false;
    this.addcontacttitle = "Add";
    this.AddCustomerContactForm = this.formBuilder.group({
      contactId: ['0'],
      CountryId: [0],
      designation: [''],
      email: ['', Validators.email],
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: ['', [Validators.maxLength(10), Validators.minLength(10)]],
      fax: ['', [Validators.maxLength(13), Validators.minLength(9)]],
      office: ['', [Validators.maxLength(13), Validators.minLength(8)]],
      defaultContact: [false],

    })

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
    if (this.ContactList.Contact.length == 0) {
      this.ContactLenghtcount = false;
    }
    else {
      this.ContactLenghtcount = true;
    }
    this.AddCustomerContactForm = this.formBuilder.group({
      contactId: ['0'],
      CountryId: [0],
      designation: [''],
      email: ['', Validators.email],
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: ['', [Validators.maxLength(10), Validators.minLength(10)]],
      fax: ['', [Validators.maxLength(13), Validators.minLength(9)]],
      office: ['', [Validators.maxLength(13), Validators.minLength(8)]],
      defaultContact: [false],

    })
  }

  GetCurrencyList() {
    this.customerservice.GetCurrency().subscribe((responce: any) => {
      debugger
      this.CurrencyList = responce.body.data;
    });
  }

  GetCustomerType() {
    this.customerservice.GetCustomerType().subscribe((responce: any) => {
      this.CustomerTypeList = responce.body.data;
      this.GetCustomerList();
    });
  }

  GetCountry() {
    this.customerservice.GetCountry().subscribe((responce: any) => {
      this.CountryList = responce.body.data;
    });
  }

  GetCustomerList() {
    this.customerservice.GetCustomerList().subscribe((responce: any) => {
      let list = responce.body.data
      this.CustomerList.Customer = [];
      if (list.length != 0 && list.length > 0) {
        list.map((result: any, index) => {
          if (this.CustomerTypeList.length != 0) {
            this.CustomerTypeList.map((res: any) => {
              if (list[index].customerTypeId == res.customerTypeId) {
                list[index].customerTypeId = res.customerTypeName;
              }
            });
          }
          this.CustomerList.Customer.push(result);
        });
      }
      this.LoderoutCustomer = false;
    });
  }

  GetDiscountType() {
    this.customerservice.GetDiscountType().subscribe((responce: any) => {
      this.DiscountTypeList = responce.body.data;

    });
  }

  EditCustomer(i: any) {
    this.customerservice.GetCustomerById(i).subscribe((responce: any) => {
      let result = responce.body.data;

      this.AddCustomerForm.patchValue({
        customerId: result.customerId,
        customerName: result.customerName,
        customerTypeId: result.customerTypeId,
        customerCode: result.customerCode,
        website: result.website,
        taxRegistrationNumber: result.taxRegistrationNumber,
        remarks: result.remarks,
        defaultCreditTerms: result.defaultCreditTerms,
        defaultCreditLimit: result.defaultCreditLimit,
        discountOption: result.discountOption,
        discountAmount: result.discountAmount,
        defaultCurrency: result.defaultCurrency,
      })
      result.contactList.contact.map((res: any, index) => {
        this.ContactList.Contact.push(res);
      })
      if (this.ContactList.Contact.length != 0) {
        this.ContactLenghtcount = true;
        this.ContactDefaultFlag = false;
      }
      else {
        this.ContactLenghtcount = false;
        this.ContactDefaultFlag = true;
      }

      result.addressList.address.map((res: any) => {
        if (res.defaultAddress) {
          let countrycodeflag = true;
          this.CountryList.map((result: any) => {
            if (countrycodeflag) {
              if (res.countryId  == result.countryId) {
                this.countryCode ="+" + result.countryCode;
                countrycodeflag = false;
              }
              else {
                this.countryCode = "";
              }
            }
          })

        }
        this.AddressList.Address.push(res);
      })
      if (this.AddressList.Address.length == 0) {
        this.DefaultFlag = true;
        this.AddressLenghtcount = false;
      }
      else {
        this.DefaultFlag = false;
        this.Addresssubmitted = false;
        this.AddressLenghtcount = true;
      }
      this.largeModal.show();
    })

  }

  DeleteCustomer(i: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.customerservice.DeleteCustomer(i).subscribe((responce: any) => {

          this.GetCustomerList();
          if (responce.status) {
            Swal.fire(
              'Deleted!',
              responce.message,
              'success'
            )
          }
        });
      }
    })


  }

  GetCreditTermsList() {
    let result;
    this.customerservice.GetCreditTermsList().subscribe((responce: any) => {
      result = responce;
      this.CreditTermsList = result.body.data;
    });
  }

}
