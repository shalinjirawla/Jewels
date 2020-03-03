import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CurrencyService, CreditTermsService,AdditionalChargeService, SalesOrderTypeService, CountryService, WarehouseService, TaxCodeService, ShipmentMethodService, ShipmentTermService, PaymentTermService } from '../../../Services/Masters-Services/general-setup.service';
import Swal from 'sweetalert2'
import { from, VirtualTimeScheduler } from 'rxjs';
import { threadId } from 'worker_threads';
import { finalize } from 'rxjs/operators';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,
})

@Component({
  selector: 'app-general-setup',
  templateUrl: './general-setup.component.html',
  styleUrls: ['./general-setup.component.scss']
})
export class GeneralSetupComponent implements OnInit {

  saving: boolean = false;
  // Currency Model start
  @ViewChild('CurrencyModal', { static: false }) public CurrencyModal: ModalDirective;
  CurrencyModelTitleString: string = "Add Currency";
  CurrencySubmiited: boolean = false;
  CurrencyForm: FormGroup;
  CurrencyResponce: any;
  CurrencyList: any[] = [];
  CurrencybtnText = "Save Change";
  // Currency Model end

  // Credit Term start
  @ViewChild('CreditTermModal', { static: false }) public CreditTermModal: ModalDirective;
  CreditTermModalModelTitleString: string = "Credit Terms";
  CreditTermSubmiited: boolean = false;
  CreditTermForm: FormGroup;
  CreditTermResponce: any;
  CreditTermList: any[] = [];
  CreditTermbtnText = "Save Change";
  // Credit Term end

  //Country Start
  @ViewChild('CountryModal', { static: false }) public CountryModal: ModalDirective;
  CountryModalTitle: string = "Countries"
  Countrysubmit: boolean = false;
  CountryForm: FormGroup;
  tabflag: boolean = false;
  CoutryList: any[] = [];
  //Country End

  //Location Start
  @ViewChild('LocationModal', { static: false }) public LocationModal: ModalDirective;
  LocationTitle: string = "Location"
  Locationsubmit: boolean = false;
  LocationForm: FormGroup;
  LocationList: any[] = [];
  //Location End

  //TaxCode Start
  @ViewChild('TaxcodeModal', { static: false }) public TaxcodeModal: ModalDirective;
  TaxcodeTitle: string = "Tax Code"
  Taxcodesubmit: boolean = false;
  TaxcodeForm: FormGroup;
  TaxcodeList: any[] = [];
  //TaxCode End

  //ShipmentTerm Start
  @ViewChild('ShipmentTermModal', { static: false }) public ShipmentTermModal: ModalDirective;
  ShipmentTermTitle: string = "Shipment Term"
  ShipmentTermsubmit: boolean = false;
  ShipmentTermForm: FormGroup;
  ShipmentTermList: any[] = [];
  //ShipmentTerm End

  //ShipmentMethod Start
  @ViewChild('ShipmentMethodModal', { static: false }) public ShipmentMethodModal: ModalDirective;
  ShipmentMethodTitle: string = "Shipment Method"
  ShipmentMethodsubmit: boolean = false;
  ShipmentMethodForm: FormGroup;
  ShipmentMethodList: any[] = [];
  //ShipmentMethod End

  //Payment Term Start
  @ViewChild('PaymentTermModel', { static: false }) public PaymentTermModel: ModalDirective;
  PaymentTermTitle: string = "Shipment Method"
  PaymentTermsubmit: boolean = false;
  PaymentTermForm: FormGroup;
  PaymentTermList: any[] = [];
  //Payment Term End
  //Payment Term Start
  @ViewChild('SalesOrderTypeModel', { static: false }) public SalesOrderTypeModel: ModalDirective;
  SalesOrderTypeTitle: string = "Sales Order Type"
  SalesOrderTypesubmit: boolean = false;
  SalesOrderTypeForm: FormGroup;
  SalesOrderTypeList: any[] = [];
  //Payment Term End
   //Additional Charge Start
   @ViewChild('AdditionalChargeModel', { static: false }) public AdditionalChargeModel: ModalDirective;
   AdditionalChargeTitle: string = "Sales Order Type"
   AdditionalChargesubmit: boolean = false;
   AdditionalChargeForm: FormGroup;
   AdditionalChargeList: any[] = [];
   //Additional Charge End
  constructor(private FormBuilder: FormBuilder,
    private CountryService: CountryService,
    private WarehouseService: WarehouseService,
    private CurrencyService: CurrencyService,
    private CreditTermsService: CreditTermsService,
    private TaxCodeService: TaxCodeService,
    private ShipmentTermService: ShipmentTermService,
    private ShipmentMethodService: ShipmentMethodService,
    private PaymentTermService: PaymentTermService,
    private SalesOrderTypeService: SalesOrderTypeService,
    private AdditionalChargeService: AdditionalChargeService,
  ) { }

  ngOnInit() {
    this.OnloadCurrency();
    this.OnloadCrediTTerms();
    this.OnloadPaymentTerms();
    this.onLoadCoutry();
    this.onLoadLocation();
    this.onLoadTaxCode();
    this.onLoadShipmentTerm();
    this.OnloadPaymentTerms();
    this.onLoadShipmentMethod();
    this.onLoadSalesOrderTypeList();
    this.onLoadAdditionalChargeList();
  }
  //#region Currency Section Start 

  public OnloadCurrency() {
    this.CurrencyForm = this.FormBuilder.group({
      CurrencyId: [0],
      CurrencyName: ['', Validators.required],
      Code: ['', Validators.required],
      Status: [true],
    });
    this.GetCurrencyList();
    this.CurrencySubmiited = false;
  }
  public GetCurrencyList() {
    this.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        this.CurrencyList = responce.data;
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        })
      }
    })
  }
  public AddCurrency(CurrencyForm: any) {
    this.CurrencySubmiited = true;
    if (this.CurrencyForm.invalid) {
      return;
    }
    this.CurrencyService.SaveCurrency(this.CurrencyForm.value).subscribe((responce: any) => {
      if (responce.status) {
        this.OnloadCurrency();
        document.getElementById("CurrencyList-link").click();
        Toast.fire({
          type: 'success',
          title: responce.message,
        })
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        })
      }
      this.CurrencySubmiited = false;
      this.CurrencybtnText = "Save Change";
      this.CurrencyModelTitleString = "Add Currency";
    });

  }
  public GetCurrency(CurrencyId: any) {
    if (CurrencyId != 0) {
      this.CurrencyService.GetCurrency(CurrencyId).subscribe((responce: any) => {
        if (responce.status) {
          this.CurrencybtnText = "Update Change";
          this.CurrencyModelTitleString = "Upadte Currency";
          this.CurrencyResponce = responce.data;
          this.CurrencyForm.patchValue({
            CurrencyId: this.CurrencyResponce.currencyId,
            CurrencyName: this.CurrencyResponce.currencyName,
            Code: this.CurrencyResponce.code,
            Status: this.CurrencyResponce.status,
          })
          document.getElementById("CurrencyFor-link").click();
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      })
    }
  }
  public DeleteCurrency(CurrencyId: any) {
    if (CurrencyId != 0) {
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
          this.CurrencyService.DeleteCurrency(CurrencyId).subscribe((responce: any) => {
            this.GetCurrencyList();
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
  }
  public CurrencyChange(event, CurrencyId) {
    if (CurrencyId != 0) {
      this.CurrencyService.CureencyStatusChange(CurrencyId, event).subscribe((responce: any) => {
        if (responce.status) {
          this.GetCurrencyList();
          Toast.fire({
            type: 'success',
            title: responce.message,
          })
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      });
    }
  }
  get fCurrency() { return this.CurrencyForm.controls; }

  Currencytabclick(event) {
    if (event == "CurrencyList-link") {
      this.OnloadCurrency();
    }
  }


  //#endregion Currency Section End 

  //#region Credit Term Section Start
  public OnloadCrediTTerms() {
    this.CreditTermForm = this.FormBuilder.group({
      CreditTermId: [0],
      Code: ['', Validators.required],
      Duration: ['', Validators.required],
      Description: [''],
    });
    this.GetCreditTermsList();
  }
  public GetCreditTermsList() {
    this.CreditTermsService.GetCreditTermsist().subscribe((responce: any) => {
      if (responce.status) {
        this.CreditTermList = responce.data;
      }
    })
  }
  public AddCreditTerms(CreditTermForm: any) {
    this.CreditTermSubmiited = true;
    if (CreditTermForm.invalid) {
      return;
    }
    this.CreditTermsService.SaveCreditTerms(CreditTermForm.value).subscribe((responce: any) => {
      if (responce.status) {
        this.CreditTermSubmiited = false;
        this.OnloadCrediTTerms();
        Toast.fire({
          type: 'success',
          title: responce.message,
        });
        document.getElementById("CreditTermList-link").click();
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        });
      }
    });
  }
  public GetCreditTerms(CreditTermsId: any) {
    if (CreditTermsId != 0) {
      this.CreditTermsService.GetCreditTerms(CreditTermsId).subscribe((responce: any) => {
        if (responce.status) {
          this.CreditTermResponce = responce.data;
          this.CreditTermForm.patchValue({
            CreditTermId: this.CreditTermResponce.creditTermId,
            Code: this.CreditTermResponce.code,
            Duration: this.CreditTermResponce.duration,
            Description: this.CreditTermResponce.description,
          });
          document.getElementById("CreditTermFor-link").click();
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      });
    }
  }
  public DeleteCreditTerms(CreditTermsId: any) {
    if (CreditTermsId != 0) {
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
          this.CreditTermsService.DeleteCreditTerms(CreditTermsId).subscribe((responce: any) => {
            this.GetCreditTermsList();
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
  }
  get fCreditTerms() { return this.CreditTermForm.controls; }

  ResetCreditTerm() {
    this.CreditTermSubmiited = false;
    this.CreditTermModal.hide();
  }

  CreditTermtabclick(event) {

    if (event == "CreditTermList-link") {
      this.OnloadCrediTTerms();
      document.getElementById("CreditTermList-link").click();
    }
  }

  OpenCreditTermModal() {
    this.CreditTermSubmiited = false;
    this.OnloadCrediTTerms();
    this.CreditTermModal.show();
  }

  //#endregion Credit Term Section Start

  //#region Payment Section Start
  public OnloadPaymentTerms() {
    this.PaymentTermForm = this.FormBuilder.group({
      paymentTermId: [0],
      code: ['', Validators.required],
      duration: [, Validators.required],
      description: [''],
    });
    this.GetPaymentTermList();
    this.PaymentTermsubmit = false;
  }

  get fPaymentTerms() { return this.PaymentTermForm.controls; }

  public AddPaymentTerms(PaymentTermForm: any) {
    this.PaymentTermsubmit = true;
    if (PaymentTermForm.invalid) {
      return;
    }
    this.PaymentTermsubmit = false;
    if (PaymentTermForm.value.paymentTermId == 0) {
      this.PaymentTermService.AddPaymentTerm(PaymentTermForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.OnloadPaymentTerms();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("PaymentTermList-link").click();
        }

      })
    }
    else {
      this.PaymentTermService.UpdatePaymentTerm(PaymentTermForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.OnloadPaymentTerms();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("PaymentTermList-link").click();
        }

      })
    }
  }

  GetPaymentTermList() {
    this.PaymentTermService.GetPaymentTermList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.PaymentTermList = responce.data;
        }
      }
    });
  }

  GetPaymentTerm(Id: any) {
    this.PaymentTermService.GetPaymentTermById(Id).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        if (result != null) {
          this.PaymentTermForm.patchValue({
            paymentTermId: result.paymentTermId,
            code: result.code,
            duration: result.duration,
            description: result.description,
          })
          document.getElementById("PaymentTermFor-link").click();
        }
      }
    })
  }
  DeletePaymentTerm(Id: any) {
    if (Id != 0) {
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
          this.PaymentTermService.DeletePaymentTerm(Id).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.OnloadPaymentTerms();
            }
          });
        }
      })
    }
  }

  OpenPaymentTermModal() {
    this.OnloadPaymentTerms();
    this.PaymentTermModel.show();
  }

  PaymentTermReset() {
    this.OnloadPaymentTerms();
    this.PaymentTermModel.hide();
  }

  PaymentTermtabclick(event) {
    if (event == "PaymentTermList-link") {
      this.OnloadPaymentTerms();
    }
  }

  //#endregion Payment Section Edn

  //#region Country Section Start

  public onLoadCoutry() {
    this.GetCountryList();
    this.CountryForm = this.FormBuilder.group({
      countryId: [0],
      countryName: ['', Validators.required],
      countryCode: ['', Validators.required],
    })
  };

  get counform() { return this.CountryForm.controls }

  tabclick(event) {
    if (event == "CountryList-link") {
      this.onLoadCoutry();
    }
  }

  OpenCountryModal() {
    this.CountryModal.show();
    document.getElementById("CountryList-link").click();
  }

  CountryReset() {
    this.onLoadCoutry();
    this.Countrysubmit = false;
    this.CountryModal.hide();

  }

  public AddCoutry(CountryForm: any) {
    this.Countrysubmit = true;
    if (CountryForm.invalid) {
      return;
    }
    this.Countrysubmit = false;
    this.CountryService.AddCountry(CountryForm.value).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        Toast.fire({
          type: 'success',
          title: responce.message,
        });
        this.onLoadCoutry();
        document.getElementById("CountryList-link").click();
      }

    })

  }

  public GetCountryList() {
    this.CountryService.GetCountryList().subscribe((responce: any) => {
      let result = responce.data;
      this.CoutryList = result;
    })
  }

  public GetCountry(i: any) {
    this.CountryService.GetCountry(i).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        this.CountryForm.patchValue({
          countryId: result.countryId,
          countryName: result.countryName,
          countryCode: result.countryCode
        })
      }
      document.getElementById("CountryForm-link").click();
    })
  }

  public DeleteCountry(i: any) {
    if (i != 0) {
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
          this.CountryService.DeleteCountry(i).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadCoutry();
            }
          });
        }
      })
    }
  }

  //#endregion Country Section End

  //#region Location Section Start

  onLoadLocation() {
    this.LocationForm = this.FormBuilder.group({
      WarehouseId: [0],
      WarehouseName: ['', Validators.required],
      Warehousecode: [, Validators.required],
      IsActive: [true],
    })
    this.getLocationList();
    this.Locationsubmit = false;
  }

  get locaform() { return this.LocationForm.controls }

  OpenLocationModal() {
    this.LocationModal.show();
  }

  getLocationList() {
    this.WarehouseService.GetLocationList().subscribe((responce: any) => {
      if (responce.status) {
        this.LocationList = responce.data;
      }
    });
  }

  AddLocation(LocationForm: any) {
    this.Locationsubmit = true;
    if (LocationForm.invalid) {
      return
    }
    this.Locationsubmit = false;
    this.WarehouseService.AddLocation(LocationForm.value).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        this.onLoadLocation();
        Toast.fire({
          type: 'success',
          title: responce.data,
        });
        document.getElementById("locationList-link").click();
      }

    })
  }

  LocationReset() {
    this.LocationModal.hide();
    this.onLoadLocation();
  }

  GetLocation(LocationId: any) {
    this.WarehouseService.GetLocation(LocationId).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        this.LocationForm.patchValue({
          WarehouseId: result.warehouseId,
          WarehouseName: result.warehouseName,
          Warehousecode: result.warehousecode,
          IsActive: result.isActive
        })
        document.getElementById("locationFor-link").click();
      }
    })
  }

  LocationChange(event, LocationId) {
    this.WarehouseService.UpdateLocationStatus(LocationId, event).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        Toast.fire({
          type: 'success',
          title: responce.message,
        });
        this.onLoadLocation();
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        });
      }

    })
  }

  DeleteLocation(i: any) {
    if (i != 0) {
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
          this.WarehouseService.DeleteLocation(i).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadLocation();
            }
          });
        }
      })
    }
  }

  locationtabclick(event) {
    if (event == "locationList-link") {
      this.onLoadLocation();
    }

  }
  //#endregion Location Section End

  //#region Taxcode Section Start
  onLoadTaxCode() {
    this.TaxcodeForm = this.FormBuilder.group({
      taxId: [0],
      code: ['', Validators.required],
      amount: [, Validators.required],
    })
    this.GetTaxCodeList();
  }

  OpentaxcodeModal() {
    this.TaxcodeModal.show();
  }

  GetTaxCodeList() {
    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.TaxcodeList = responce.data;
        }
      }
    });
  }

  get taxform() { return this.TaxcodeForm.controls }

  AddTaxcode(TaxcodeForm: any) {
    this.Taxcodesubmit = true;
    if (TaxcodeForm.invalid) {
      return
    }
    this.Taxcodesubmit = false;
    if (TaxcodeForm.value.taxId == 0) {
      this.TaxCodeService.AddTaxCode(TaxcodeForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadTaxCode();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("taxcodeList-link").click();
        }

      })
    }
    else {
      this.TaxCodeService.UpdateTaxCode(TaxcodeForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadTaxCode();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("taxcodeList-link").click();
        }

      })
    }
  }

  GetTaxcode(i: any) {
    this.TaxCodeService.GetTaxCode(i).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        if (result != null) {
          this.TaxcodeForm.patchValue({
            taxId: result.taxId,
            code: result.code,
            amount: result.amount,
          })
          document.getElementById("taxcodeFor-link").click();
        }
      }
    })
  }

  DeleteTaxcode(Id: any) {
    if (Id != 0) {
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
          this.TaxCodeService.DeleteTaxCode(Id).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadTaxCode();
            }
          });
        }
      })
    }
  }

  taxcodetabclick(event) {
    if (event == "taxcodeList-link") {
      this.onLoadTaxCode();
    }

  }

  TaxcodeReset() {
    this.TaxcodeModal.hide();
    this.onLoadTaxCode();
  }

  //#endregion Taxcode Section End


  //#region ShipmentTerm Section Start

  onLoadShipmentTerm() {
    this.ShipmentTermForm = this.FormBuilder.group({
      shipmentTermId: [0],
      code: ['', Validators.required],
      description: ['']

    })
    this.GetShipmentTermList();
    this.ShipmentTermsubmit = false;
  }

  get shiptermform() { return this.ShipmentTermForm.controls }

  AddShipmentTerm(ShipmentTermForm: any) {
    this.ShipmentTermsubmit = true;
    if (ShipmentTermForm.invalid) {
      return
    }
    this.ShipmentTermsubmit = false;
    if (ShipmentTermForm.value.shipmentTermId == 0) {
      this.ShipmentTermService.AddShipmentTerm(ShipmentTermForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadShipmentTerm();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("ShipmentTermList-link").click();
        }

      })
    }
    else {
      this.ShipmentTermService.UpdateShipmentTerm(ShipmentTermForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadShipmentTerm();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("ShipmentTermList-link").click();
        }

      })
    }
  }

  GetShipmentTermList() {
    this.ShipmentTermService.GetShipmentTermList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.ShipmentTermList = responce.data;
        }
      }
    });
  }

  GetShipmentTerm(i: any) {
    this.ShipmentTermService.GetShipmentTerm(i).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        if (result != null) {
          this.ShipmentTermForm.patchValue({
            shipmentTermId: result.shipmentTermId,
            code: result.code,
            description: result.description,
          })
          document.getElementById("ShipmentTermFor-link").click();
        }
      }
    })
  }

  DeleteShipmentTerm(Id: any) {
    if (Id != 0) {
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
          this.ShipmentTermService.DeleteShipmentTerm(Id).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadShipmentTerm();
            }
          });
        }
      })
    }
  }

  OpenShipmentTermModal() {
    this.onLoadShipmentTerm();
    this.ShipmentTermsubmit = false;
    this.ShipmentTermModal.show();
  }

  ShipmentTermReset() {
    this.ShipmentTermModal.hide();
    this.ShipmentTermsubmit = false;
    this.onLoadShipmentTerm();
  }

  ShipmentTermtabclick(event) {
    if (event == "ShipmentTermList-link") {
      this.ShipmentTermsubmit = false;
      this.onLoadShipmentTerm();
    }
  }

  //#endregion ShipmentTerm Section End

  //#region ShipmentMethod Section Start

  onLoadShipmentMethod() {
    this.ShipmentMethodForm = this.FormBuilder.group({
      shipmentMethodId: [0],
      code: ['', Validators.required],
      description: ['']

    })
    this.GetShipmentMethodList();
    this.ShipmentMethodsubmit = false;
  }

  OpenShipmentMethodModal() {
    this.onLoadShipmentMethod();
    this.ShipmentMethodsubmit = false;
    this.ShipmentMethodModal.show();
  }

  GetShipmentMethodList() {
    this.ShipmentMethodService.GetShipmentMethodList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.ShipmentMethodList = responce.data;
        }
      }
    });
  }

  AddShipmentMethod(ShipmentMethodForm: any) {
    this.ShipmentMethodsubmit = true;
    if (ShipmentMethodForm.invalid) {
      return
    }
    this.ShipmentMethodsubmit = false;
    if (ShipmentMethodForm.value.shipmentMethodId == 0) {
      this.ShipmentMethodService.AddShipmentMethod(ShipmentMethodForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadShipmentMethod();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("ShipmentMethodList-link").click();
        }

      })
    }
    else {
      this.ShipmentMethodService.UpdateShipmentMethod(ShipmentMethodForm.value).subscribe((responce: any) => {
        let result = responce.data;
        if (responce.status) {
          this.onLoadShipmentMethod();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("ShipmentMethodList-link").click();
        }

      })
    }
  }

  GetShipmentMethod(i: any) {
    this.ShipmentMethodService.GetShipmentMethodById(i).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        if (result != null) {
          this.ShipmentMethodForm.patchValue({
            shipmentMethodId: result.shipmentMethodId,
            code: result.code,
            description: result.description,
          })
          document.getElementById("ShipmentMethodFor-link").click();
        }
      }
    })
  }

  DeleteShipmentMethod(Id: any) {
    if (Id != 0) {
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
          this.ShipmentMethodService.DeleteShipmentMethod(Id).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadShipmentMethod();
            }
          });
        }
      })
    }
  }

  ShipmentMethodtabclick(event) {
    if (event == "ShipmentMethodList-link") {
      this.onLoadShipmentMethod();
    }
  }

  get smf() { return this.ShipmentMethodForm.controls }

  ShipmentMethodReset() {
    this.ShipmentMethodModal.hide();
    this.onLoadShipmentMethod();
  }

  //#endregion ShipmentMethod Section End

//#region Sales Order type Start

onLoadSalesOrderTypeList() {
  this.SalesOrderTypeForm = this.FormBuilder.group({
    SalesOrderTypeId: [0],
    TypeName: ['', Validators.required],
    Description: [''],
    IsActive: [true],
  })
  this.getSalesOrderTypeList();
  this.SalesOrderTypesubmit = false;
}

get SalesOrderTypeform() { return this.SalesOrderTypeForm.controls }

OpenSalesOrderTypeModal() {
  this.SalesOrderTypeModel.show();
}

getSalesOrderTypeList() {
  this.SalesOrderTypeService.GetSalesOrderTypeList().subscribe((responce: any) => {
    if (responce.status) {
      this.SalesOrderTypeList = responce.data;
    }
  });
}

AddSalesOrderType(SalesOrderTypeForm: any) {
  this.SalesOrderTypesubmit = true;
  if (SalesOrderTypeForm.invalid) {
    return
  }
  const self = this;
  self.saving = true;
  this.SalesOrderTypesubmit = false;
  this.SalesOrderTypeService.AddSalesOrderType(SalesOrderTypeForm.value)
    .pipe(finalize(() => self.saving = false))
    .subscribe((responce: any) => {
      if (responce.status) {
        this.onLoadSalesOrderTypeList();
        Toast.fire({
          type: 'success',
          title: responce.message,
        });
        document.getElementById("SalesOrderTypeList-link").click();
      }

    })
}

SalesOrderTypeReset() {
  this.SalesOrderTypeModel.hide();
  this.onLoadSalesOrderTypeList();
}

GetSalesOrderType(SalesOrderTypeId: any) {
  this.SalesOrderTypeService.GetSalesOrderTypeById(SalesOrderTypeId).subscribe((responce: any) => {
    let result = responce.data;
    if (responce.status) {
      this.SalesOrderTypeForm.patchValue({
        SalesOrderTypeId:result.salesOrderTypeId, 
        TypeName: result.typeName,
        Description:result.description,
        IsActive: result.isActive
      })
      document.getElementById("SalesOrderTypeFor-link").click();
    }
  })
}
public SalesOrderTypeChange(event, SalesOrderTypeId) {
  if (SalesOrderTypeId != 0) {
    this.SalesOrderTypeService.SalesOrderTypeStatusChange(SalesOrderTypeId, event).subscribe((responce: any) => {
      if (responce.status) {
        this.onLoadSalesOrderTypeList();
        Toast.fire({
          type: 'success',
          title: responce.message,
        })
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        })
      }
    });
  }
}

DeleteSalesOrderType(SalesOrderTypeId: any) {
  if (SalesOrderTypeId != 0) {
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
        this.SalesOrderTypeService.DeleteSalesOrderType(SalesOrderTypeId).subscribe((responce: any) => {
          if (responce.status) {
            Swal.fire(
              'Deleted!',
              responce.message,
              'success'
            )
            this.getSalesOrderTypeList()
            this.onLoadSalesOrderTypeList();
          }
        });
      }
    })
  }
}

SalesOrderTypetabclick(event) {
  if (event == "SalesOrderTypeList-link") {
    this.onLoadSalesOrderTypeList();
  }

}

//#endregion Sales Order list End

  //#region Additional Charge Start

  onLoadAdditionalChargeList() {
    this.AdditionalChargeForm = this.FormBuilder.group({
      AdditionalChargeId: [0],
      Name: ['', Validators.required],
      UnitPriceType: ['Fixed'],
      UnitPrice: ['', Validators.required],
      Description: [''],
      IsActive: [true],
    })
    this.getAdditionalChargeList();
    this.AdditionalChargesubmit = false;
  }

  get AdditionalChargeform() { return this.AdditionalChargeForm.controls }

  OpenAdditionalChargeModal() {
    this.AdditionalChargeModel.show();
    document.getElementById("AdditionalChargeList-link").click();
  }

  getAdditionalChargeList() {
    this.AdditionalChargeService.GetAdditionalChargeList().subscribe((responce: any) => {
      if (responce.status) {
        this.AdditionalChargeList = responce.data;
      }
    });
  }

  AddAdditionalCharge(AdditionalChargeForm: any) {
    this.AdditionalChargesubmit = true;
    if (AdditionalChargeForm.invalid) {
      return
    }
    const self = this;
    self.saving = true;
    this.AdditionalChargeService.AddAdditionalCharge(AdditionalChargeForm.value)
      .pipe(finalize(() => self.saving = false))
      .subscribe((responce: any) => {
        if (responce.status) {
          this.onLoadAdditionalChargeList();
          Toast.fire({
            type: 'success',
            title: responce.message,
          });
          document.getElementById("AdditionalChargeList-link").click();
        }

      })
  }

  AdditionalChargeReset() {
    this.AdditionalChargeModel.hide();
    this.onLoadAdditionalChargeList();
  }

  GetAdditionalCharge(AdditionalChargeId: any) {
    this.AdditionalChargeService.GetAdditionalChargeById(AdditionalChargeId).subscribe((responce: any) => {
      let result = responce.data;
      if (responce.status) {
        this.AdditionalChargeForm.patchValue({
          AdditionalChargeId:result.additionalChargeId, 
          Name: result.name,
          UnitPriceType:result.unitPriceType,
          UnitPrice:result.unitPrice,
          Description:result.description,
          IsActive: result.isActive
        })
        document.getElementById("AdditionalChargeFor-link").click();
      }
    })
  }
  public AdditionalChargeChange(event, AdditionalChargeId) {
    if (AdditionalChargeId != 0) {
      this.AdditionalChargeService.AdditionalChargeStatusChange(AdditionalChargeId, event).subscribe((responce: any) => {
        if (responce.status) {
          this.onLoadAdditionalChargeList();
          Toast.fire({
            type: 'success',
            title: responce.message,
          })
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      });
    }
  }

  DeleteAdditionalCharge(AdditionalChargeId: any) {
    if (AdditionalChargeId != 0) {
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
          this.AdditionalChargeService.DeleteAdditionalCharge(AdditionalChargeId).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                'Deleted!',
                responce.message,
                'success'
              )
              this.onLoadAdditionalChargeList()
            }
          });
        }
      })
    }
  }

  AdditionalChargetabclick(event) {
    if (event == "AdditionalChargeList-link") {
      this.onLoadAdditionalChargeList();
    }

  }

  //#endregion Additional Charge End
  allowalpha(event: any) {
    const pattern = /[a-z\\A-Z\ \a-z\\A-Z]/;
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

  allownumberwithdot(event: any) {
    const pattern = /[0-9\\.]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  avoidkeyPress(e) {
    if ((e.keyCode == 110 || e.keyCode == 190) && e.target.value.indexOf(".") != -1) {
      e.preventDefault();
      return;
    }
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
      // Allow: Ctrl+A, Command+A
      (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
      // Allow: home, end, left, right, down, up
      (e.keyCode >= 35 && e.keyCode <= 40)) {
      // let it happen, don't do anything
      return;
    }
    else if (e.keyCode == 190) {
      if (e.shiftKey) {
        e.preventDefault();
      }
      else {
        return;
      }
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
      e.preventDefault();
    }
  }

}
