import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CurrencyService, TaxCodeService, CountryService, ShipmentTermService,ShipmentMethodService } from '../../../Services/Masters-Services/general-setup.service';
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
  ContactList: any = { "Contact": [] };
  AddressList: any = { "Address": [] };
  countryCode: string = "";
  //DropDownlist items
  DefaultCurrenyList: any[];
  DefaultPaymentTermsList: any[];
  DefaultTaxCodeList: any[];
  ShipmentTermsList: any[];
  ShipmentMethodList:any[];
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
  constructor(private FormBuilder: FormBuilder,
    private CurrencyService: CurrencyService,
    private TaxCodeService: TaxCodeService,
    private CountryService: CountryService,
    private ShipmentTermService: ShipmentTermService,
    private ShipmentMethodService:ShipmentMethodService,
  ) { }

  ngOnInit() {
    this.OnloadSupplliers();
    this.GetCountry();
  }
  public OnloadSupplliers() {
    this.AddSuppliersForm = this.FormBuilder.group({
      SupplierId: [0],
      CompanyName: ['', Validators.required],
      SupplierCode: [''],
      Website: [''],
      Remarks: [''],
      DefaultCurrency: [''],
      DefaultPaymentTerms: [''],
      DefaultTax: [''],
      ShipmentTerms: [''],
      Shipmentmethod: [''],

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
    this.GetDropDownValues();
  }

  public GetDropDownValues() {

    this.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        this.DefaultCurrenyList = responce.data;
      }else{
        Swal.fire({
          type:'error',
          title:responce.message,
        });
      }
    });
    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        this.DefaultTaxCodeList = responce.data;
      }else{
        Swal.fire({
          type:'error',
          title:responce.message,
        });
      }
    });
    this.ShipmentTermService.GetShipmentTermList().subscribe((responce: any) => {
      if(responce.status){
        this.ShipmentTermsList=responce.data;
      }else{
        Swal.fire({
          type:'error',
          title:responce.message,
        });
      }
    })
    this.ShipmentMethodService.GetShipmentMethodList().subscribe((responce:any)=>{
      if(responce.status){
        this.ShipmentMethodList=responce.data;
      }else{
        Swal.fire({
          type:'error',
          title:responce.message
        });
      }
    });
  }
  public TabClick(event) {

  }
  public saveSuppliers(AddSuppliersForm: FormControl) {
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
    this.AddSuppliersvalue.AddressList = this.AddressList;
    this.AddSuppliersvalue.ContactList = this.ContactList;
  }

  public AddSupplliersAddress(AddSuppliersAddressForm: FormControl) {
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
        if (this.ContactList.Contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
        else {
          this.ContactLenghtcount = false;
        }

        this.DefaultFlag = false;
      }
      let a = JSON.stringify(AddSuppliersAddressForm.value);
      this.secondAddress = JSON.parse(a);
      AddSuppliersAddressForm.value.addressId = '_' + Math.random().toString(36).substr(2, 9);
      this.Address = AddSuppliersAddressForm.value;

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
    if (this.AddressList.Address.length == 0) {
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
    let a = this.AddressList.Address.map((result: any, index) => {
      if (i == index && !valaue) {
        this.AddressList.Address[index].defaultAddress = true;

        let countrycodeflag = true;
        this.CountryList.map((result: any) => {
          if (countrycodeflag) {
            if (this.AddressList.Address[index].countryId == result.countryId) {
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
              this.OnloadSupplliers();
            }
          }
        })
      }
    })
  }

  public ResetForm() {
    this.FormSubmittedSupplliers = false;
    this.Addresssubmitted = false;
    this.OnloadSupplliers();
    this.largeModal.hide();
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
  public GetCountry() {
    this.CountryService.GetCountryList().subscribe((responce: any) => {
      if (responce.status) {
        this.CountryList = responce.data;
      }
    });
  }

  public AddSuppliersContact(AddSuppliersContactForm: FormControl) {
    this.contactsubmitted = true;
    if (this.AddSuppliersContactForm.invalid) {
      return;
    }
    this.contactsubmitted = false;
    if (AddSuppliersContactForm.value.contactId == 0) {
      if (this.ContactList.Contact.length == 0) {
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
        this.ContactList.Contact.push(this.Contact);
        if (this.ContactList.Contact.length != 0) {
          this.ContactLenghtcount = true;
          this.Contact = null;
        }
      }
    }
    else {
      var elementPos = this.ContactList.Contact.map(function (x) { return x.contactId; }).indexOf(AddSuppliersContactForm.value.contactId);
      this.Contact = AddSuppliersContactForm.value;
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
        let a = this.ContactList.Contact.map((result: any, index) => {
          if (i == index) {
            var elementPos = this.ContactList.Contact.map(function (x) { return x.contactId; }).indexOf(result.contactId);
            this.ContactList.Contact.splice(elementPos, 1)
            if (this.ContactList.Contact.length == 0) {
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
  public CancelAddSuppliersContact() {
    if (this.ContactList.Contact.length == 0) {
      this.ContactLenghtcount = false;
    }
    else {
      this.ContactLenghtcount = true;
    }
    this.AddSuppliersContactForm = this.FormBuilder.group({
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
}
