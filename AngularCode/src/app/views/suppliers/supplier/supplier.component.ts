import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CurrencyService, TaxCodeService, CountryService, ShipmentTermService, ShipmentMethodService, PaymentTermService } from '../../../Services/Masters-Services/general-setup.service';
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;

  ModelTitleString: string = "Add New Supplier";
  FormSubmittedSupplliers: boolean = false;
  SuppliersList: any[];
  Supplieremplty: boolean = true;
  SupplierNoDataFound: boolean = true;
  AddSuppliersForm: FormGroup;
  SaveBtnTest: string = "Save & Next";
  BillingShippingAddress: boolean = false;
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
  Addresssubmitted: boolean = false;
  CustomerList: any = { "Customer": [] };
  copyAddress: string;
  secondAddress: any;
  NewCondAddress: any;
  Address: any;
  Contact: any;
  ContactList: any = { "contact": [] };
  AddressList: any = { "address": [] };
  countryCode: string = "";
  //DropDownlist items
  DefaultCurrenyList: any[];
  DefaultPaymentTermsList: any[];
  DefaultTaxCodeList: any[];
  ShipmentTermsList: any[];
  ShipmentMethodList: any[];
  DefaultPaymentTermList: any[];
  //Dropwdownlist items

  //SupplierAddress Start
  AddressLenghtcount: boolean = false;
  FormSubmittedSupplliersAddress: boolean = false;
  SuppliersAddressList: any[];
  SupplierAddressemplty: boolean = true;
  AddSuppliersAddressForm: FormGroup;
  addaddresstitle: string = "Save";
  addcontacttitle: string = "Add";
  contactsubmitted: boolean = false;
  //SuppliersAddress End

  //Supplliers conatct start
  ContactLenghtcount: boolean = false;
  AddSuppliersContactForm: FormGroup;
  SuppliersContactList: any[];
  SupplierContactemplty: boolean = true;
  ConatctSaveBtnText: string = "Save";

  AddSuppliersvalue: any = [];
  //Suppliers Conatct end

  //filtering start
  filter:any;
  //filtering end
  constructor(private FormBuilder: FormBuilder,
    private CurrencyService: CurrencyService,
    private TaxCodeService: TaxCodeService,
    private CountryService: CountryService,
    private ShipmentTermService: ShipmentTermService,
    private ShipmentMethodService: ShipmentMethodService,
    private PaymentTermService: PaymentTermService,
    private SupplierServicesService: SupplierServicesService,
  ) { }
  key: string = 'Companyname'; //set default
  reverse: boolean = false;
  sort(key){
    this.key = key;
    this.reverse = !this.reverse;
  }
  p: number = 1;
  listofCount:number=5;
  ngOnInit() {
  
    this.OnloadSupplliers();
    this.GetSuppliersList();
  }
  public ListOfData(Count:number)
  {
    this.listofCount=Count;
  }
  public OnloadSupplliers() {
    this.AddSuppliersForm = this.FormBuilder.group({
      supplierId: [0],
      companyName: ['', Validators.required],
      supplierCode: [''],
      website: [''],
      remarks: [''],
      defaultCurrency: [''],
      defaultPaymentTerms: [''],
      defaultTaxCode: [''],
      shipmentterms: [''],
      shipmentmethod: [''],

    });
    this.AddSuppliersAddressForm = this.FormBuilder.group({
      addressId: ['0'],
      addressType: ['', Validators.required],
      address: ['', Validators.required],
      countryId: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      defaultAddress: [false],
    })

    this.AddSuppliersContactForm = this.FormBuilder.group({
      contactId: ['0'],
      countryId: [0],
      designation: [''],
      email: ['', Validators.email],
      firstName: ['', Validators.required],
      lastName: [''],
      mobile: ['', [Validators.maxLength(10), Validators.minLength(10)]],
      fax: ['', [Validators.maxLength(13), Validators.minLength(9)]],
      office: ['', [Validators.maxLength(13), Validators.minLength(8)]],
      defaultContact: [false],

    })
    this.GetDropDownValues();
  }

  public GetDropDownValues() {
    this.CountryService.GetCountryList().subscribe((responce: any) => {
      if (responce.status) {
        this.CountryList = responce.data;
      }
    });

    this.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        this.DefaultCurrenyList = responce.data;
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        });
      }
    });
    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        this.DefaultTaxCodeList = responce.data;
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        });
      }
    });
    this.ShipmentTermService.GetShipmentTermList().subscribe((responce: any) => {
      if (responce.status) {
        this.ShipmentTermsList = responce.data;
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        });
      }
    })
    this.ShipmentMethodService.GetShipmentMethodList().subscribe((responce: any) => {
      if (responce.status) {
        this.ShipmentMethodList = responce.data;
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message
        });
      }
    });
    this.PaymentTermService.GetPaymentTermList().subscribe((responce: any) => {
      if (responce.status) {
        this.DefaultPaymentTermList = responce.data;
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        })
      }
    });
  }
  public GetSuppliersList() {
    this.SupplierServicesService.GetSuppliersList().subscribe((responce: any) => {
      if (responce.status) {
        this.Supplieremplty = false;
        if (responce.data != null && responce.data != undefined && responce.data.length > 0) {
          this.SuppliersList = responce.data;
          this.SupplierNoDataFound = false;
        } else {
          this.SupplierNoDataFound = true;
        }

      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        });
      }
    });
  }
  public TabClick(event) {

  }
  public saveSuppliers(AddSuppliersForm: any) {
    this.FormSubmittedSupplliers = true;
    if (this.AddSuppliersForm.invalid) {
      document.getElementById("SuppliersDetail-link").click();
      return;
    }
    this.FormSubmittedSupplliers = false;
    if (document.getElementById("SuppliersDetail-link").className == "nav-link active") {
      document.getElementById("SuppliersAddress-link").click();
      return;
    }
    else if (document.getElementById("SuppliersAddress-link").className == "nav-link active") {
      document.getElementById("SuppliersContact-link").click();
      return;
    }
    this.AddSuppliersvalue.SuppliersDetail = AddSuppliersForm.value;
    this.AddSuppliersvalue.SuppliersDetail.defaultCurrency = Number(this.AddSuppliersvalue.SuppliersDetail.defaultCurrency);
    this.AddSuppliersvalue.SuppliersDetail.defaultPaymentTerms = Number(this.AddSuppliersvalue.SuppliersDetail.defaultPaymentTerms);
    this.AddSuppliersvalue.SuppliersDetail.defaultTaxCode = Number(this.AddSuppliersvalue.SuppliersDetail.defaultTaxCode);
    this.AddSuppliersvalue.SuppliersDetail.shipmentterms = Number(this.AddSuppliersvalue.SuppliersDetail.shipmentterms);
    this.AddSuppliersvalue.SuppliersDetail.shipmentmethod = Number(this.AddSuppliersvalue.SuppliersDetail.shipmentmethod);

    this.AddSuppliersvalue.AddressList = this.AddressList;
    this.AddSuppliersvalue.ContactList = this.ContactList;

    this.SupplierServicesService.AddUpdate(this.AddSuppliersvalue).subscribe((responce: any) => {
     
      this.ResetForm();
      this.ModelTitleString="Add New Supplier";
       if (responce.status) {
        Swal.fire({
          type: 'success',
          title: responce.message,
        });
        this.GetSuppliersList();
        this.largeModal.hide();
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        });
      }
    })
  }

  public AddSupplliersAddress(AddSuppliersAddressForm: any) {
    this.Addresssubmitted = true;
    if (this.AddSuppliersAddressForm.invalid) {
      return;
    }
    this.Addresssubmitted = false;
    if (AddSuppliersAddressForm.value.addressId == 0) {

      if (this.DefaultFlag) {
        AddSuppliersAddressForm.value.defaultAddress = true;
        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (AddSuppliersAddressForm.value.countryId == result.countryId) {
              this.countryCode = "+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        
        this.DefaultFlag = false;
      }
      let a = JSON.stringify(AddSuppliersAddressForm.value);
      this.secondAddress = JSON.parse(a);
      AddSuppliersAddressForm.value.addressId = '_' + Math.random().toString(36).substr(2, 9);
      this.Address = AddSuppliersAddressForm.value;

      if (this.Address != undefined) {
        this.AddressList.address.push(this.Address);
        if (!this.BillingShippingAddress) {
          if (this.AddressList.address.length != 0) {
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
          this.AddressList.address.push(this.secondAddress);
          if (this.AddressList.address.length != 0) {
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
      var elementPos = this.AddressList.Address.map(function (x) { return x.addressId; }).indexOf(AddSuppliersAddressForm.value.addressId);
      this.Address = AddSuppliersAddressForm.value;
      var objectFound = this.AddressList.Address[elementPos];
      if (this.Address.defaultAddress == true) {
        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (AddSuppliersAddressForm.value.countryId == result.countryId) {
              this.countryCode = "+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        if (this.ContactList.contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }
      }
      this.AddressList.Address[elementPos] = this.Address;
      if (this.AddressList.address.length != 0) {
        this.AddressLenghtcount = true;
        this.Address = null;
      }

    }
  }
  public AddNewAddress() {
    this.AddressLenghtcount = false;
    //this.onLoad();
    this.AddSuppliersAddressForm = this.FormBuilder.group({
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
  public CancelAddSuppilresAddress() {
    if (this.AddressList.address.length == 0) {
      this.AddressLenghtcount = false;
    } else {
      this.AddressLenghtcount = true;
    }
    this.AddSuppliersAddressForm = this.FormBuilder.group({
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
  public SetDeafult(valaue: any, i: any) {
    let AnyDefaultSet: boolean = false;
    let a = this.AddressList.address.map((result: any, index) => {
      if (i == index && !valaue) {
        this.AddressList.address[index].defaultAddress = true;

        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (this.AddressList.address[index].countryId == result.countryId) {
              this.countryCode = "+" + result.countryCode;
              countrycodeflag = false;
            }
            else {
              this.countryCode = "";
            }
          }
        })
        if (this.ContactList.contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }
        AnyDefaultSet = true;
      } else {
        this.AddressList.address[index].defaultAddress = false;
        if (!AnyDefaultSet) {
          this.countryCode = "";
        }
      }
    })
  }
  public EditAddress(i: any) {
    let a = this.AddressList.Address.map((result: any, index) => {
      if (i == index) {
        this.AddSuppliersAddressForm.patchValue({
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

  public DeleteAddress(i: any) {
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
        let a = this.AddressList.address.map((result: any, index) => {
          if (i == index) {
            var elementPos = this.AddressList.address.map(function (x) { return x.addressId; }).indexOf(result.addressId);
            this.AddressList.address.splice(elementPos, 1);
            let setDefaultAddress = false;
            let a = this.AddressList.address.map((result: any, index) => {
              if (result.defaultAddress) {
                setDefaultAddress = true;
              }
            })
            if (!setDefaultAddress) {
              if (this.AddressList.address.length != 0) {
                this.AddressList.address[0].defaultAddress = true;
              }
            }
            if (this.AddressList.address.length == 0) {
              this.DefaultFlag = true;
              this.CheckboxFlag = true;
              this.AddressLenghtcount = false;

            }
          }
        })
      }
    })
  }
  public EditSuppliers(SupplierId: number) {
    this.ContactList.contact = [];
    this.AddressList.address = [];
    if (SupplierId != 0) {
      this.SupplierServicesService.GetSuppliers(SupplierId).subscribe((responce: any) => {
        if (responce.status) {
          this.largeModal.show();
          this.ModelTitleString="Update Supplier";
          let data = responce.data;
          this.AddSuppliersForm.patchValue({
            supplierId: data.supplierId,
            companyName: data.companyName,
            supplierCode: data.supplierCode,
            website: data.website,
            remarks: data.remarks,
            defaultCurrency: data.defaultCurrency,
            defaultPaymentTerms: data.defaultPaymentTerms,
            defaultTaxCode: data.defaultTaxCode,
            shipmentterms: data.shipmentterms,
            shipmentmethod: data.shipmentmethod,
          });
          if (data.contactList != null && data.contactList != undefined && data.contactList.contact.length > 0) {
            data.contactList.contact.map((res: any, index) => {
              this.ContactList.contact.push(res);
            })
            if (this.ContactList.contact.length != 0) {
              this.ContactLenghtcount = true;
              this.ContactDefaultFlag = false;
            }
            else {
              this.ContactLenghtcount = false;
              this.ContactDefaultFlag = true;
            }
          }
          if (data.addressList != null && data.addressList != undefined && data.addressList.address.length > 0) {
            data.addressList.address.map((res: any, index) => {
              this.AddressList.address.push(res);
            })
            if (this.AddressList.address.length != 0) {
              this.AddressLenghtcount = true;
            }
            else {
              this.AddressLenghtcount = false;
            }
          }
        } else {
          Swal.fire({
            type: 'error',
            title: responce.message,
          });
        }
      });
    }
  }
  public ResetForm() {
    this.FormSubmittedSupplliers = false;
    this.Addresssubmitted = false;
    this.OnloadSupplliers();
    this.largeModal.hide();
    this.AddressList.address=[];
    this.ContactList.contact=[];
    this.AddressLenghtcount=false;
    this.ContactLenghtcount=false;
    this.ModelTitleString="Add New Supplier";
  }
  get f() { return this.AddSuppliersForm.controls; }
  get add() { return this.AddSuppliersAddressForm.controls; }
  get con() { return this.AddSuppliersContactForm.controls; }


  public CheckAddress(event) {
    this.BillingShippingAddress = event;
  }
  public changeaddresstype(event) {
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
  public allownumberwithdot(event: any) {
    const pattern = /[0-9\+\.]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
  public allownumber(event: any) {
    const pattern = /[0-9]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  public allowalpha(event: any) {
    const pattern = /[a-z\+\A-Z\+ +\a-z\+\A-Z+]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  public AddSuppliersContact(AddSuppliersContactForm: any) {
    this.contactsubmitted = true;
    if (this.AddSuppliersContactForm.invalid) {
      return;
    }
    this.contactsubmitted = false;
    if (AddSuppliersContactForm.value.contactId == 0) {
      if (this.ContactList.contact.length == 0) {
        this.ContactDefaultFlag = true;
        AddSuppliersContactForm.value.defaultContact = true;
        this.ContactDefaultFlag = false;
      }
      if (this.ContactDefaultFlag) {

        AddSuppliersContactForm.value.defaultContact = true;
        this.ContactDefaultFlag = false;
      }
      AddSuppliersContactForm.value.contactId = '_' + Math.random().toString(36).substr(2, 9);
      this.Contact = AddSuppliersContactForm.value;
      if (this.Contact != undefined) {
        this.ContactList.contact.push(this.Contact);
        if (this.ContactList.contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
      }
    }
    else {
      var elementPos = this.ContactList.contact.map(function (x) { return x.contactId; }).indexOf(AddSuppliersContactForm.value.contactId);
      this.Contact = AddSuppliersContactForm.value;
      var objectFound = this.ContactList.Contact[elementPos];

      this.ContactList.Contact[elementPos] = this.Contact;
      if (this.ContactList.contact.length != 0) {
        this.ContactLenghtcount = true;
        this.Contact = null;
      }

    }
  }

  EditContact(i: any) {
    let a = this.ContactList.contact.map((result: any, index) => {
      if (i == index) {
        this.AddSuppliersContactForm.patchValue({
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
    let a = this.ContactList.contact.map((result: any, index) => {
      if (!result.defaultContact && !valaue && i == index) {
        this.ContactList.contact[index].defaultContact = true;
      } else {
        this.ContactList.contact[index].defaultContact = false;
      }

    })
  }

  DeleteContact(i: any) {
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
        let a = this.ContactList.contact.map((result: any, index) => {
          if (i == index) {
            var elementPos = this.ContactList.contact.map(function (x) { return x.contactId; }).indexOf(result.contactId);
            this.ContactList.contact.splice(elementPos, 1)
            if (this.ContactList.contact.length == 0) {
              this.ContactLenghtcount = false;
              this.OnloadSupplliers();
            }
          }
        })
      }
    })

  }

  AddNewContact() {
    this.ContactLenghtcount = false;
    this.addcontacttitle = "Add";
    this.AddSuppliersContactForm = this.FormBuilder.group({
      contactId: ['0'],
      countryId: [0],
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
  public CancelAddSuppliersContact() {
    if (this.ContactList.contact.length == 0) {
      this.ContactLenghtcount = false;
    }
    else {
      this.ContactLenghtcount = true;
    }
    this.AddSuppliersContactForm = this.FormBuilder.group({
      contactId: ['0'],
      countryId: [0],
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
  public DeleteSupplier(SuppliersId: number) {
    if (SuppliersId != 0) {
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
          this.SupplierServicesService.DeleteSuppliers(SuppliersId).subscribe((responce: any) => {
            if (responce.status) {
              this.GetSuppliersList();
              this.ContactList.contact = [];
              this.AddressList.address = [];
              Swal.fire({
                type: 'success',
                title: responce.message,
              })
            } else {
              Swal.fire({
                type: 'error',
                title: responce.message,
              })
            }
          });
        }
      })
    }
  }
}
