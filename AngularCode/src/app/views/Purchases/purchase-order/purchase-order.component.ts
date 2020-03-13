import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {  FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service';
import { ProductService } from '../../../Services/Products-Services/product.service';
import { TenantsServicesService } from './../../../Services/TenantsServices/tenants-services.service';
import {  CurrencyService, PaymentTermService, TaxCodeService, AdditionalChargeService, CreditTermsService, ShipmentMethodService } from '../../../Services/Masters-Services/general-setup.service';
import { PurchaseOrderService } from './../../../Services/Purchase-Order-Services/purchase-order-services.services';
import { PurchaseOrderDetailsVM, PurchaseOrderItemsVM,  PurchaseOrdersVM, AdditionalChargeForAll, AdditionalChargeForProduct } from './../../../Models/PurchaseOrderModel/PurchaseOrderModel';
import { finalize } from 'rxjs/operators';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-purchase-order',
  templateUrl: './purchase-order.component.html',
  styleUrls: ['./purchase-order.component.scss']
})
export class PurchaseOrderComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  @ViewChild('largeModal1', { static: false }) public largeModal1: ModalDirective;
  constructor(private formBuilder: FormBuilder,
    private SupplierServicesService: SupplierServicesService,
    private TenantsServicesService: TenantsServicesService,
    private PurchaseOrderService: PurchaseOrderService,
    private CurrencyService: CurrencyService,
    private CreditTermsService: CreditTermsService,
    private ShipmentMethodService: ShipmentMethodService,
    private AdditionalChargeService: AdditionalChargeService,
    private TaxCodeService: TaxCodeService,
    private ProductService: ProductService,
    private PaymentTermService: PaymentTermService, ) { }
  //Form List stat
  saving: boolean = false;
  applysaving: boolean = false;
  PurchaseOrderDetailsForm: FormGroup;
  PurchaseOrderTypeForm: FormGroup;
  AdditionalChargeFormForAll: FormGroup;
  AdditionalChargeFormForProduct: FormGroup;
  OrderItemsForm: FormGroup;
  //Form List end
  //btn submiited start
  PurchaseOrderTabSubmit: boolean = false;
  PurchaseOrderTabSubmitsecond: boolean = false;
  firsttab: boolean = true;
  secondtab: boolean = false;
  thirdtab: boolean = false;
  PurchaseOrderTypeTabSubmit: boolean = false;
  PurchaseOrdeItemTabSubmit: boolean = false;
  AdditionalChargeSubmit: boolean = false;
  //btn submitted end

  PurchaseTitle: string = "Add New Sales Order";
  PurchasebtnTitle: string = "Save & Next";
  CurrentDate: Date;
  EstimatedDeliveryDate: Date;
  AddressTypeTiltle: string = "SHIPPING ADDRESS";
  EmployeeModelLists: any[] = [];
  TenantUserList: any[] = [];
  SupplierList: any[] = [];
  PurchaseOrderTypeList: any[] = [];
  CurrencyList: any[] = [];
  CreditTermsList: any[] = [];
  PaymentTermsList: any[] = [];
  ShipmentMethodList: any[] = [];
  AdditionalChargeList: any[] = [];
  TaxcodeList: any[] = [];
  ProductList: any[] = [];
  SupplierBillingAddress: any;
  SupplierShipingAddress: any;
  DiscountTypeList: any[] = [];

  PurchaseOrdersVM: PurchaseOrdersVM = new PurchaseOrdersVM();
  PurchaseOrderDetailsVM: PurchaseOrderDetailsVM = new PurchaseOrderDetailsVM();
  PurchaseOrderItemsVM: PurchaseOrderItemsVM = new PurchaseOrderItemsVM();
  PurchaseOrderItemsVMList: PurchaseOrderItemsVM[] = [];
  AdditionalChargeForAllModel: AdditionalChargeForAll = new AdditionalChargeForAll();
  AdditionalChargeForProductModel: AdditionalChargeForProduct = new AdditionalChargeForProduct();
  IsProductSalesOrderItemAdded: boolean = false;
  IsProductLoad: boolean = false;
  AdditionalChargeActive: string = "All";

  //bind PurchaseOrder list var
  loadSalesOrderList: boolean = false;
  EmpltyPurchaseOrderList: boolean = false;
  PurchasesOrderList: any[] = [];
  PurchaseOrderAdditionalchargeForAll: any[] = [];
  PurchaseOrderAdditionalchargeForProduct: any[] = [];
  ngOnInit() {
    this.CurrentDate = new Date();
    this.oninit();
    this.GetPurchaseOrderList()
  }
  public oninit() {
    this.onLoad();
    this.BindData();
    this.onLoadAdditionalChargeForAll();
    this.OnLoadAdditionalChargeForProduct();
  }
  public onLoad() {
    this.PurchaseOrderDetailsForm = this.formBuilder.group({
      PurchaseOrdersId: ['0'],
      PurchaseOrderNumber: [''],
      DateOrdered: [new Date(), Validators.required],
      EstimatedDeliveryDate: ['', Validators.required],
      SupplierId: ['', Validators.required],
      ReferenceNumber: [''],
      Remarks: [''],
      PaymentTermId: [''],
      CreditTermId: [''],
      ShipmentMethodId: [''],
      ExchangeRate: ['', Validators.required],
      CurrencyId: ['', Validators.required],
    });
    this.OrderItemsForm = this.formBuilder.group({
      PurchaseOrderDetailsId: ['0'],
      OrderItems: this.formBuilder.array([this.formBuilder.group({
        OrderItemsId: [0],
        ProductId: ['', Validators.required],
        Unit: ['1'],
        UnitPrice: ['', Validators.required],
        QTY: ['', Validators.required],
        DiscountType: ['1'],
        Discount: ['0'],
        IsTaxble: [false],
        TaxId: [''],
        TaxTotal: ['0.00'],
        Total: ['0.00']
      })]),
      TotalQTY: ['0'],
      Total: ['0.00'],
      FinalTotal: ['0.00'],
      TaxInclude: [false],
      FinalTaxTotal: ['0.00'],
      AdditionalChargeAmount: ['0.00'],
      IsAdditionalChargeApply: [false],
      IsAdditionalChargeApplyType: ['']
    });
  }
  public onLoadAdditionalChargeForAll() {
    this.AdditionalChargeFormForAll = this.formBuilder.group({
      AdditionalChargeForAll: this.formBuilder.array([this.formBuilder.group({
        OrderItemsId: [0],
        AdditionalForAllId: [0],
        AdditionalChargeId: ['', Validators.required],
        Amount: ['', Validators.required],
        TaxId: ['']
      })])
    });
  }
  public OnLoadAdditionalChargeForProduct() {
    this.AdditionalChargeFormForProduct = this.formBuilder.group({
      AdditionalChargeForProduct: this.formBuilder.array([this.formBuilder.group({
        OrderItemsId: [''],
        AdditionalForProductId: [''],
        ProductId: ['', Validators.required],
        AdditionalChargeId: ['', Validators.required],
        Amount: ['', Validators.required],
        IsTaxble: [false],
        TaxId: ['']
      })])
    });
  }

  public BindData() {
    const self = this;
    self.TenantsServicesService.GetTenantUserList()
      .subscribe((responce: any) => {
        self.TenantUserList = responce.data;
      });

    self.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        self.CurrencyList = responce.data;
      }
    });
    self.CreditTermsService.GetCreditTermsist().subscribe((responce: any) => {
      if (responce.status) {
        self.CreditTermsList = responce.data;
      }
    });
    self.ShipmentMethodService.GetShipmentMethodList().subscribe((responce: any) => {
      if (responce.status) {
        self.ShipmentMethodList = responce.data;
      }
    });
    self.PaymentTermService.GetPaymentTermList().subscribe((responce: any) => {
      if (responce.status) {
        self.PaymentTermsList = responce.data;
      }
    });
    self.SupplierServicesService.GetSuppliersList().subscribe((responce: any) => {
      if (responce.status && responce.data != null) {
        self.SupplierList = responce.data;
      }
    });
    self.DiscountTypeList = [
      { 'discountTypeId': 1, 'name': '$', 'value': 'Fixed' },
      { 'discountTypeId': 2, 'name': '%', 'value': 'Percentage' },

    ];
    self.AdditionalChargeService.GetAdditionalChargeList().subscribe((responce: any) => {
      if (responce.status) {
        self.AdditionalChargeList = responce.data;
      }
    });
    self.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.TaxcodeList = responce.data;
        }
      }
    });
    self.ProductService.GetProductList()
      .subscribe((responce: any) => {
        if (responce != null && responce.status) {
          self.ProductList = responce.data;
        }
      });
  }

  public SupplierChange(SupplierId: number) {
    if (SupplierId != 0) {
      const self = this;
      self.SupplierServicesService.GetSuppliersAddress(SupplierId).subscribe((responce: any) => {
        if (responce.status) {
          if (responce.data.billingAddress != null && responce.data.billingAddress != undefined)
            self.SupplierBillingAddress = responce.data.billingAddress;
          else
            this.SupplierBillingAddress = null;
          if (responce.data.shippingAddress != null && responce.data.shippingAddress != undefined)
            self.SupplierShipingAddress = responce.data.shippingAddress;
          else
            this.SupplierShipingAddress = null;
        }
      });
    } else {
      this.SupplierBillingAddress = null;
      this.SupplierShipingAddress = null;
    }
  }
  public GetPurchaseOrderList() {
    const self = this;
    self.loadSalesOrderList = true;
    self.PurchaseOrderService.GetPurchaseOrderList()
      .pipe(finalize(() => { self.loadSalesOrderList = false }))
      .subscribe((responce: any) => {
        if (responce.status) {
          self.PurchasesOrderList = responce.data;
          if (self.PurchasesOrderList.length == 0)
            self.EmpltyPurchaseOrderList = true;
          else
            self.EmpltyPurchaseOrderList = false;
        }
      }, (errror) => {
        Swal.fire('error', errror.message, 'error');
      })
  }
  
  public OpenPurchaseOrderModel() {
    this.CurrentDate = new Date();
    this.PurchaseOrderTabSubmit = false;
    this.PurchaseOrderTabSubmitsecond = false;
    this.PurchaseOrderTypeTabSubmit = false;
    this.largeModal.show();
    this.PurchaseTitle = "Add New Purchase Order";
    this.SupplierChange(0);
  }
  get AdditionalChargeForProduct() {
    return this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
  }
  get AdditionalChargeForAll() {
    return this.AdditionalChargeFormForAll.get('AdditionalChargeForAll') as FormArray;
  }
  public CheckIsTaxble(event: any, index) {

    var IsTaxble = event.children[event.selectedIndex].title;
    if (IsTaxble != null && IsTaxble != undefined && IsTaxble != '') {
      if (IsTaxble == 'false')
        IsTaxble = false;
      else
        IsTaxble = true;
      let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
      arrobj.controls[index].patchValue({
        IsTaxble: IsTaxble,
      });
    }
  }
  public AddAdditionalChargeForAll() {
    this.AdditionalChargeSubmit = false;
    let arrylendth = this.AdditionalChargeForAll.length;
    this.AdditionalChargeForAll.push(this.formBuilder.group({
      OrderItemsId: [0],
      AdditionalForAllId: [arrylendth],
      AdditionalChargeId: ['', Validators.required],
      Amount: ['', Validators.required],
      TaxId: ['']
    }));
  }
  public AddAdditionalChargeForProduct(product: any, index: any) {
    let arrylendth = this.AdditionalChargeForProduct.length;
    var ProductExist = this.AdditionalChargeForProduct.value.filter(x => x.ProductId == product.ProductId);
    if (ProductExist.length == 0) {
      if (index != 1) {
        this.AdditionalChargeForProduct.push(this.formBuilder.group({
          OrderItemsId: product.OrderItemsId,
          AdditionalForProductId: [arrylendth],
          ProductId: product.ProductId,
          AdditionalChargeId: ['', Validators.required],
          Amount: ['', Validators.required],
          IsTaxble: product.IsTaxble,
          TaxId: ['']
        }));
      } else {
        let arrproductobj = this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
        arrproductobj.controls[0].patchValue({
          OrderItemsId: product.OrderItemsId,
          ProductId: product.ProductId,
          IsTaxble: product.IsTaxble,
        });
      }
    }
  }
  public DeleteAdditionalChargeForAll(index) {
    this.AdditionalChargeForAll.removeAt(index);
  }
  public ResetAdditionalChargeForProduct() {
    var IsAdditionalChargeApply = this.OrderItemsForm.value.IsAdditionalChargeApply;
    var IsAdditionalChargeApplyType = this.OrderItemsForm.value.IsAdditionalChargeApplyType;
    if (IsAdditionalChargeApply) {
      if (IsAdditionalChargeApplyType == 'All') {
        this.OnLoadAdditionalChargeForProduct();
      }
    } else {
      this.OnLoadAdditionalChargeForProduct();
    }
    this.AdditionalChargeActive = "All";
  }
  public ResetAdditionalChargeForAll() {
    var IsAdditionalChargeApply = this.OrderItemsForm.value.IsAdditionalChargeApply;
    var IsAdditionalChargeApplyType = this.OrderItemsForm.value.IsAdditionalChargeApplyType;
    if (IsAdditionalChargeApply) {
      if (IsAdditionalChargeApplyType == 'Product') {
        this.onLoadAdditionalChargeForAll();
      }
    } else {
      this.onLoadAdditionalChargeForAll();
    }
    this.AdditionalChargeActive = "Product";
    if (this.OrderItemsForm.invalid) {
      this.IsProductSalesOrderItemAdded = false;
    } else {
      this.IsProductSalesOrderItemAdded = true;
      this.IsProductLoad = true;

      setTimeout(() => {
        let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
        if (arrobj.length > 1) {
          let index = 0;
          arrobj.value.forEach(element => {
            index++;
            this.AddAdditionalChargeForProduct(element, index);
          });
        } else {
          let arrproductobj = this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
          arrproductobj.controls[0].patchValue({
            OrderItemsId: arrobj.value[0].OrderItemsId,
            ProductId: arrobj.value[0].ProductId,
            IsTaxble: arrobj.value[0].IsTaxble,
          });
        }
        this.IsProductLoad = false;
      }, 1000);

    }
  }
  get OrderItems() {
    return this.OrderItemsForm.get('OrderItems') as FormArray;
  }
  public AddOrderItems() {
    this.PurchaseOrdeItemTabSubmit = false;
    let arrylendth = this.OrderItems.length;
    this.OrderItems.push(this.formBuilder.group({
      OrderItemsId: [arrylendth],
      ProductId: ['', Validators.required],
      Unit: ['1'],
      UnitPrice: ['', Validators.required],
      QTY: ['', Validators.required],
      DiscountType: ['1'],
      Discount: ['0'],
      IsTaxble: [false],
      TaxId: [''],
      TaxTotal: ['0.00'],
      Total: ['0.00']
    }));
  }
  public DeleteOrderItems(index) {
    this.OrderItems.removeAt(index);
    this.AdditionalChargeForProduct.removeAt(index);
    this.TotalCalcalate();
  }
  public TabChange(event: any) {
  }
  public SavePurchaseOrder(PurchaseOrderDetailsForm: any, OrderItemsForm: any) {
    this.PurchaseOrderTabSubmit = true;
    if (this.PurchaseOrderDetailsForm.invalid) {
      document.getElementById("TabOrderDetail-link").click();
      return;
    }
    if (document.getElementById('TabOrderDetail-link').className == "nav-link active") {
      document.getElementById("SaleOrderItem-link").click();
      return
    }

    const self = this;
    this.PurchaseOrdeItemTabSubmit = true;
    if (OrderItemsForm.invalid) {
      return;
    }
    self.saving = true;
    self.PurchaseOrderDetailsVM.AdditionalChargeForProduct = [];
    self.PurchaseOrderDetailsVM.AdditionalChargeForAll = [];
    self.PurchaseOrderItemsVMList = [];
    var PurchaseOrdeDetails = PurchaseOrderDetailsForm.value;
    var PurchaseOrderItems = OrderItemsForm.value;
    // PurchaseOrder
    self.PurchaseOrdersVM.PurchaseOrdersId = Number(PurchaseOrdeDetails.PurchaseOrdersId);
    self.PurchaseOrdersVM.PurchaseOrderNumber = PurchaseOrdeDetails.PurchaseOrderNumber;
    self.PurchaseOrdersVM.DateOrdered = PurchaseOrdeDetails.DateOrdered;
    self.PurchaseOrdersVM.EstimatedDeliveryDate = PurchaseOrdeDetails.EstimatedDeliveryDate;
    self.PurchaseOrdersVM.SupplierId = Number(PurchaseOrdeDetails.SupplierId);
    self.PurchaseOrdersVM.ReferenceNumber = PurchaseOrdeDetails.ReferenceNumber;
    self.PurchaseOrdersVM.Remarks = PurchaseOrdeDetails.Remarks;
    self.PurchaseOrdersVM.CreditTermId = Number(PurchaseOrdeDetails.CreditTermId);
    self.PurchaseOrdersVM.ShipmentMethodId = Number(PurchaseOrdeDetails.ShipmentMethodId);
    self.PurchaseOrdersVM.CurrencyId = Number(PurchaseOrdeDetails.CurrencyId);
    self.PurchaseOrdersVM.PaymentTermId = Number(PurchaseOrdeDetails.PaymentTermId);
    self.PurchaseOrdersVM.ExchangeRate = Number(PurchaseOrdeDetails.ExchangeRate);

    //Sales Order Details
    self.PurchaseOrderDetailsVM.PurchaseOrderDetailsId = Number(PurchaseOrderItems.PurchaseOrderDetailsId);
    self.PurchaseOrderDetailsVM.PurchaseOrdersId = Number(PurchaseOrdeDetails.PurchaseOrdersId);
    self.PurchaseOrderDetailsVM.TotalQTY = Number(PurchaseOrderItems.TotalQTY);
    self.PurchaseOrderDetailsVM.Total = Number(PurchaseOrderItems.Total);
    self.PurchaseOrderDetailsVM.FinalTotal = Number(PurchaseOrderItems.FinalTotal);
    self.PurchaseOrderDetailsVM.TaxInclude = PurchaseOrderItems.TaxInclude;
    self.PurchaseOrderDetailsVM.FinalTaxTotal = Number(PurchaseOrderItems.FinalTaxTotal);
    self.PurchaseOrderDetailsVM.AdditionalChargeAmount = Number(PurchaseOrderItems.AdditionalChargeAmount);
    self.PurchaseOrderDetailsVM.IsAdditionalChargeApply = PurchaseOrderItems.IsAdditionalChargeApply;
    self.PurchaseOrderDetailsVM.IsAdditionalChargeApplyType = PurchaseOrderItems.IsAdditionalChargeApplyType;

    //Additionla charge for All and product
    if (self.PurchaseOrderDetailsVM.IsAdditionalChargeApplyType != '') {
      if (self.PurchaseOrderDetailsVM.IsAdditionalChargeApplyType == 'All') {
        self.AdditionalChargeForAll.value.forEach(element => {
          self.AdditionalChargeForAllModel.PurchaseOrdersId = Number(PurchaseOrdeDetails.PurchaseOrdersId);
          self.AdditionalChargeForAllModel.AdditionalForAllId = Number(element.AdditionalForAllId);
          self.AdditionalChargeForAllModel.AdditionalChargeId = Number(element.AdditionalChargeId);
          self.AdditionalChargeForAllModel.TaxId = Number(element.TaxId);
          self.PurchaseOrderDetailsVM.AdditionalChargeForAll.push(self.AdditionalChargeForAllModel);
          self.AdditionalChargeForAllModel = new AdditionalChargeForAll();
        });
        self.PurchaseOrderDetailsVM.AdditionalChargeForProduct = [];
      } else if (self.PurchaseOrderDetailsVM.IsAdditionalChargeApplyType == 'Product') {
        self.AdditionalChargeForProduct.value.forEach(element => {
          self.AdditionalChargeForProductModel.PurchaseOrdersId = Number(PurchaseOrdeDetails.PurchaseOrdersId);
          self.AdditionalChargeForProductModel.AdditionalChargeForProductId = Number(element.AdditionalForProductId);
          self.AdditionalChargeForProductModel.ProductId = Number(element.ProductId);
          self.AdditionalChargeForProductModel.AdditionalChargeId = Number(element.AdditionalChargeId);
          self.AdditionalChargeForProductModel.IsTaxble = element.IsTaxble;
          self.AdditionalChargeForProductModel.TaxId = Number(element.TaxId);
          self.PurchaseOrderDetailsVM.AdditionalChargeForProduct.push(self.AdditionalChargeForProductModel);
          self.AdditionalChargeForProductModel = new AdditionalChargeForProduct();
        });
        self.PurchaseOrderDetailsVM.AdditionalChargeForAll = [];
      } else {
        self.PurchaseOrderDetailsVM.AdditionalChargeForProduct = [];
        self.PurchaseOrderDetailsVM.AdditionalChargeForAll = [];
      }
    }
    //Sales Order Items list
    OrderItemsForm.value.OrderItems.forEach(element => {
      self.PurchaseOrderItemsVM.OrderItemsId = element.OrderItemsId;
      self.PurchaseOrderItemsVM.ProductId = Number(element.ProductId);
      self.PurchaseOrderItemsVM.UnitPrice = Number(element.UnitPrice);
      self.PurchaseOrderItemsVM.Unit = Number(element.Unit);
      self.PurchaseOrderItemsVM.QTY = Number(element.QTY);
      self.PurchaseOrderItemsVM.DiscountType = Number(element.DiscountType);
      self.PurchaseOrderItemsVM.Discount = Number(element.Discount);
      self.PurchaseOrderItemsVM.TaxId = Number(element.TaxId);
      self.PurchaseOrderItemsVM.IsTaxble = element.IsTaxble;
      self.PurchaseOrderItemsVM.TaxTotal = Number(element.TaxTotal);
      self.PurchaseOrderItemsVM.Total = Number(element.Total);
      self.PurchaseOrderItemsVMList.push(self.PurchaseOrderItemsVM);
      self.PurchaseOrderItemsVM = new PurchaseOrderItemsVM();
    });
    const input = {
      'PurchaseOrdersVM': self.PurchaseOrdersVM,
      'PurchaseOrderDetailsVM': self.PurchaseOrderDetailsVM,
      'PurchaseOrderItemsVM': self.PurchaseOrderItemsVMList
    }
    self.PurchaseOrderService.AddEditPurchaseOrder(input)
      .pipe(finalize(() => { self.saving = false }))
      .subscribe((responce: any) => {
        Swal.fire('Success', responce.message, 'success');
        this.Close();
        this.GetPurchaseOrderList();
      }, (error) => {
        Swal.fire('Error', error.message, 'error');
      });
  }
  //#region order sales
  public Close() {
    this.largeModal.hide();
    this.ResetForm();
    this.oninit();
  }
  public ResetForm() {
    this.onLoad();
    document.getElementById("TabOrderDetail-link").click();
    this.PurchaseOrderDetailsVM.AdditionalChargeForProduct = [];
    this.PurchaseOrderDetailsVM.AdditionalChargeForAll = [];
    this.PurchaseOrderItemsVMList = [];
  }
  public OpenAdditionlChargeModel() {
    this.largeModal1.show();
    this.AdditionalChargeSubmit = false;
    //document.getElementById('ForAll-link').click();
  }
  public OpenAdditionlChargeModelandBindProduct(AdditionalChargeApplyType: string) {
    this.largeModal1.show();
    this.AdditionalChargeSubmit = false;
    if (AdditionalChargeApplyType == "Product") {
      this.ResetAdditionalChargeForAll();
    }
    //document.getElementById('ForAll-link').click();
  }
  public CloseAdditionlChargeModel() {
    this.largeModal1.hide();
  }

  public ChangeTaxInclude(event: boolean, FinalTaxTotal: number, FinalTotal: number) {
    if (FinalTaxTotal != null && FinalTaxTotal != undefined && FinalTotal != null && FinalTotal != undefined) {
      FinalTaxTotal = Number(FinalTaxTotal);
      FinalTotal = Number(FinalTotal);
      if (event)
        FinalTotal = FinalTotal + FinalTaxTotal;
      else
        FinalTotal = FinalTotal - FinalTaxTotal;
      this.OrderItemsForm.patchValue({
        TaxInclude: event,
        FinalTotal: FinalTotal.toFixed(2),
      });
    }
  }
  public CalculateItemOrderData(UnitPrice: number, QTY: number, DiscountType: any,
    Discount: number, index: any, taxId: number, Taxtotal) {
    UnitPrice = this.ConvertToNumber(UnitPrice);
    QTY = this.ConvertToNumber(QTY);
    DiscountType = this.ConvertToNumber(DiscountType);
    Discount = this.ConvertToNumber(Discount);
    if (UnitPrice != null && UnitPrice != undefined) {
      let Total = UnitPrice * QTY;
      if (Discount > 0) {
        if (DiscountType == 1)//fixed
          Total = Total - Discount;
        else//percentage
        {
          let per = Total * Discount / 100;
          Total = Total - per;
        }
      }
      let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
      arrobj.controls[index].patchValue({
        Total: Total.toFixed(2),
      });
      this.TotalCalcalate();
      if (taxId != null && taxId != undefined)
        this.ChangeTaxValue(taxId, index, Total);
    }

  }
  public TotalCalcalate() {
    let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
    var CalQTY = 0;
    var CalTotal = 0;
    var CalFinalTotal = 0;
    var CalTaxTotal = 0;
    var AdditionalChargeAmount = Number(this.OrderItemsForm.value.AdditionalChargeAmount);
    arrobj.controls.forEach(element => {
      CalQTY += Number(element.value.QTY);
      CalTotal += Number(element.value.Total);
      CalTaxTotal += Number(element.value.TaxTotal);
    });
    CalFinalTotal = CalTotal + AdditionalChargeAmount;
    this.OrderItemsForm.patchValue({
      TotalQTY: CalQTY,
      Total: CalTotal.toFixed(2),
      FinalTotal: CalFinalTotal.toFixed(2),
      FinalTaxTotal: CalTaxTotal.toFixed(2),
    });
    if (!this.OrderItemsForm.value.TaxInclude)
      CalTaxTotal = 0;
    this.ChangeTaxInclude(this.OrderItemsForm.value.TaxInclude, CalTaxTotal, CalFinalTotal);
  }
  public ConvertToNumber(value: any): any {
    if (value != null && value != undefined && value != '')
      return Number(value);
    else
      return 0;
  }
  public ChangeTaxValue(taxId: any, index, Taxtotal) {
    if (taxId != null && taxId != undefined && taxId != '') {
      taxId = Number(taxId);
      Taxtotal = Number(Taxtotal);
      this.TaxcodeList.forEach(element => {
        if (element.taxId == taxId) {
          let caltaxper = Taxtotal * element.amount / 100;
          let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
          arrobj.controls[index].patchValue({
            TaxTotal: caltaxper.toFixed(2),
          });
        }
      });

    }
    else {
      let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
      arrobj.controls[index].patchValue({
        TaxTotal: 0.00,
      });
    }
    this.TotalCalcalate();
  }
  //#endregion 
  public ChangeAddAdditionalChargeForAll(event: any, index) {
    var SelectedAdditionalCharge = event.value;
    let arrobj = this.AdditionalChargeFormForAll.get('AdditionalChargeForAll') as FormArray;
    var IsExist = arrobj.value.filter(x => x.AdditionalChargeId == SelectedAdditionalCharge);
    if (IsExist != null && IsExist != undefined && IsExist.length > 1) {
      Amount = '';
      SelectedAdditionalCharge = '';
      Swal.fire('Warning', 'This Additional Charge already Selected..', 'info');
    } else {
      var Amount = event.children[event.selectedIndex].title;
      if (Amount != null && Amount != undefined && Amount != '') {
        Amount = Number(Amount);
      } else {
        Amount = 0;
      }
    }
    arrobj.controls[index].patchValue({
      AdditionalChargeId: SelectedAdditionalCharge,
      Amount: Amount,
    });
  }
  public ChangeAddAdditionalChargeForProduct(event: any, index) {
    var SelectedAdditionalCharge = event.value;
    let arrobj = this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
    var Amount = event.children[event.selectedIndex].title;
    if (Amount != null && Amount != undefined && Amount != '') {
      Amount = Number(Amount);
    } else {
      Amount = 0;
    }
    // }
    arrobj.controls[index].patchValue({
      AdditionalChargeId: SelectedAdditionalCharge,
      Amount: Amount,
    });
  }
  public ApplyAdditionalChargeForAll(AdditionalChargeForAll: any) {
    this.AdditionalChargeSubmit = true;
    if (AdditionalChargeForAll.invalid) {
      return;
    }
    const self = this;
    AdditionalChargeForAll = AdditionalChargeForAll.value;
    if (AdditionalChargeForAll != null && AdditionalChargeForAll.length > 0) {
      var TaxAmount = 0;
      var AdditionalChargeAmount = 0;
      AdditionalChargeForAll.forEach(element => {
        let Taxobj = this.TaxcodeList.filter(x => x.taxId === Number(element.TaxId));
        if (Taxobj != null && Taxobj != undefined && Taxobj.length > 0) {
          TaxAmount += Number(element.Amount) * Taxobj[0].amount / 100;
          AdditionalChargeAmount += Number(element.Amount) + TaxAmount;
        }
        else {
          AdditionalChargeAmount += Number(element.Amount);
        }
      });
      var OriginalFinalTotalAmount = Number(this.OrderItemsForm.value.FinalTotal);
      var OriginalAdditionalCharge = Number(this.OrderItemsForm.value.AdditionalChargeAmount);
      var IsAdditionalChargeApply = this.OrderItemsForm.value.IsAdditionalChargeApply;
      if (IsAdditionalChargeApply)
        OriginalFinalTotalAmount = OriginalFinalTotalAmount - OriginalAdditionalCharge
      AdditionalChargeAmount = AdditionalChargeAmount + TaxAmount;
      OriginalFinalTotalAmount = OriginalFinalTotalAmount + AdditionalChargeAmount;
      this.OrderItemsForm.patchValue({
        AdditionalChargeAmount: AdditionalChargeAmount.toFixed(2),
        IsAdditionalChargeApply: true,
        FinalTotal: OriginalFinalTotalAmount.toFixed(2),
        IsAdditionalChargeApplyType: this.AdditionalChargeActive,
      });
      this.CloseAdditionlChargeModel();
    }
  }
  public ApplyAdditionalChargeForProduct(AdditionalChargeForProduct: any) {
    this.AdditionalChargeSubmit = true;
    if (AdditionalChargeForProduct.invalid) {
      return;
    }
    const self = this;
    AdditionalChargeForProduct = AdditionalChargeForProduct.value;
    if (AdditionalChargeForProduct != null && AdditionalChargeForProduct.length > 0) {
      var TaxAmount = 0;
      var AdditionalChargeAmount = 0;
      AdditionalChargeForProduct.forEach(element => {
        let Taxobj = this.TaxcodeList.filter(x => x.taxId === Number(element.TaxId));
        if (Taxobj != null && Taxobj != undefined && Taxobj.length > 0 && element.IsTaxble) {
          TaxAmount += Number(element.Amount) * Taxobj[0].amount / 100;
          AdditionalChargeAmount += Number(element.Amount) + TaxAmount;
        }
        else {
          AdditionalChargeAmount += Number(element.Amount);
        }
      });
      var OriginalFinalTotalAmount = Number(this.OrderItemsForm.value.FinalTotal);
      var OriginalAdditionalCharge = Number(this.OrderItemsForm.value.AdditionalChargeAmount);
      var IsAdditionalChargeApply = this.OrderItemsForm.value.IsAdditionalChargeApply;
      if (IsAdditionalChargeApply)
        OriginalFinalTotalAmount = OriginalFinalTotalAmount - OriginalAdditionalCharge
      // AdditionalChargeAmount = AdditionalChargeAmount + TaxAmount;
      OriginalFinalTotalAmount = OriginalFinalTotalAmount + AdditionalChargeAmount;
      this.OrderItemsForm.patchValue({
        AdditionalChargeAmount: AdditionalChargeAmount.toFixed(2),
        IsAdditionalChargeApply: true,
        FinalTotal: OriginalFinalTotalAmount.toFixed(2),
        IsAdditionalChargeApplyType: this.AdditionalChargeActive,
      });
      this.CloseAdditionlChargeModel();
    }
  }
  public ApplyAdditionalCharge(AdditionalChargeForAll: any, AdditionalChargeForProduct: any) {
    if (this.AdditionalChargeActive == 'All') {
      this.ApplyAdditionalChargeForAll(AdditionalChargeForAll);
    }
    else if (this.AdditionalChargeActive == 'Product') {
      this.ApplyAdditionalChargeForProduct(AdditionalChargeForProduct);
    }
    this.AdditionalChargeSubmit = true;
    if (AdditionalChargeForAll.invalid) {
      return;
    }

  }
  public GetPurchaseOrder(PurchaseOrderId: number) {
    if (PurchaseOrderId != 0) {
      const self = this;
      self.PurchaseOrderService.GetPurchaseOrder(PurchaseOrderId).subscribe((responce: any) => {
        if (responce != null && responce.status) {
          var PurchaseOrder = responce.data.purchaseOrdersVM;
          var PurchaseOrdeDetails = responce.data.purchaseOrderDetailsVM;
          var PurchaseOrderItems = responce.data.purchaseOrderItemsVM;
          self.PurchaseOrderDetailsForm.patchValue({
            PurchaseOrdersId: PurchaseOrder.purchaseOrdersId,
            PurchaseOrderNumber: PurchaseOrder.purchaseOrderNumber,
            DateOrdered: new Date(PurchaseOrder.dateOrdered),
            EstimatedDeliveryDate: new Date(PurchaseOrder.estimatedDeliveryDate),
            SupplierId: PurchaseOrder.supplierId,
            ReferenceNumber: PurchaseOrder.referenceNumber,
            Remarks: PurchaseOrder.remarks,
            PaymentTermId: PurchaseOrder.paymentTermId?PurchaseOrder.paymentTermId:'',
            CreditTermId: PurchaseOrder.creditTermId?PurchaseOrder.creditTermId:'',
            ShipmentMethodId: PurchaseOrder.shipmentMethodId? PurchaseOrder.shipmentMethodId:'',
            ExchangeRate: PurchaseOrder.exchangeRate,
            CurrencyId: PurchaseOrder.currencyId?PurchaseOrder.currencyId:'',
          })
          this.SupplierChange(PurchaseOrder.supplierId);
          this.CurrentDate = PurchaseOrder.dateOrdered;
          this.EstimatedDeliveryDate = new Date(self.PurchaseOrderDetailsForm.value.EstimatedDeliveryDate);
          this.OrderItemsForm.patchValue({
            PurchaseOrderDetailsId: PurchaseOrdeDetails.purchaseOrderDetailsId,
            TotalQTY: PurchaseOrdeDetails.totalQTY,
            Total: PurchaseOrdeDetails.total,
            FinalTotal: PurchaseOrdeDetails.finalTotal,
            TaxInclude: PurchaseOrdeDetails.taxInclude,
            FinalTaxTotal: PurchaseOrdeDetails.finalTaxTotal,
            AdditionalChargeAmount: PurchaseOrdeDetails.additionalChargeAmount,
            IsAdditionalChargeApply: PurchaseOrdeDetails.isAdditionalChargeApply,
            IsAdditionalChargeApplyType: PurchaseOrdeDetails.isAdditionalChargeApplyType,
          });
          var index = 0;
          PurchaseOrderItems.forEach(element => {
            this.BindPurchaseOrderItem(element, index);
            index++;
          });
          debugger
          if (PurchaseOrdeDetails.isAdditionalChargeApply) {
            if (PurchaseOrdeDetails.isAdditionalChargeApplyType == 'All') {
              self.PurchaseOrderAdditionalchargeForAll = PurchaseOrdeDetails.additionalChargeForAll;
              if (self.PurchaseOrderAdditionalchargeForAll != null && self.PurchaseOrderAdditionalchargeForAll.length > 0) {
                let indexAll = 0;
                self.PurchaseOrderAdditionalchargeForAll.forEach(element => {
                  this.BindAddionalChargeForAll(element, indexAll);
                  indexAll++;
                });
                this.AdditionalChargeActive == 'All'
              }
            }
            else if (PurchaseOrdeDetails.isAdditionalChargeApplyType == 'Product') {
              self.PurchaseOrderAdditionalchargeForProduct = PurchaseOrdeDetails.additionalChargeForProduct;
              if (self.PurchaseOrderAdditionalchargeForProduct != null && self.PurchaseOrderAdditionalchargeForProduct.length > 0) {
                let indexProduct = 0;
                self.PurchaseOrderAdditionalchargeForProduct.forEach(element => {
                  this.BindAddionalChargeForProduct(element, indexProduct);
                  indexProduct++;
                });
                this.IsProductSalesOrderItemAdded = true;
                this.AdditionalChargeActive == 'Product'
              }
            }
          }
          self.OpenPurchaseOrderModel();
          self.PurchaseTitle = "Edit Sales Order";
        } else
          Swal.fire('error', responce.message, 'error');
      }, (error) => {
        console.log(error.message);
      });
    }
  }
  public BindPurchaseOrderItem(PurchaseOrderItems: any, index?) {
    let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
    if (index > 0)
      this.AddOrderItems();
    arrobj.controls[index].patchValue({
      OrderItemsId: PurchaseOrderItems.orderItemsId,
      ProductId: PurchaseOrderItems.productId,
      Unit: PurchaseOrderItems.unit,
      UnitPrice: PurchaseOrderItems.unitPrice,
      QTY: PurchaseOrderItems.qty,
      DiscountType: PurchaseOrderItems.discountType,
      Discount: PurchaseOrderItems.discount,
      IsTaxble: PurchaseOrderItems.isTaxble,
      TaxId: PurchaseOrderItems.taxId,
      TaxTotal: PurchaseOrderItems.taxTotal,
      Total: PurchaseOrderItems.total,
    });

  }
  public BindAddionalChargeForAll(all: any, index) {
    let arrobj = this.AdditionalChargeFormForAll.get('AdditionalChargeForAll') as FormArray;
    if (index > 0)
      this.AddAdditionalChargeForAll();
    arrobj.controls[index].patchValue({
      OrderItemsId: all.salesOrdersId,
      AdditionalForAllId: all.additionalForAllId,
      AdditionalChargeId: all.additionalChargeId,
      Amount: this.BindChargeAmount(all.additionalChargeId),
      TaxId: all.taxId,
    });
  }
  public BindChargeAmount(chargeId: number): any {
    if (chargeId != 0) {
      var obj = this.AdditionalChargeList.filter(x => x.additionalChargeId == chargeId);
      if (obj != null)
        return obj[0].unitPrice;
      else
        return 0;
    } else
      return 0;
  }
  public BindAddionalChargeForProduct(product: any, index) {
    let arrobj = this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
    if (index > 0)
      this.BindaddAdditionalchargeProduct(product);
    arrobj.controls[index].patchValue({
      OrderItemsId: product.salesOrdersId,
      AdditionalForProductId: product.additionalChargeForProductId,
      ProductId: product.productId,
      AdditionalChargeId: product.additionalChargeId,
      Amount: this.BindChargeAmount(product.additionalChargeId),
      IsTaxble: product.isTaxble,
      TaxId: product.taxId,
    });
  }
  public BindaddAdditionalchargeProduct(product: any) {
    let arrylendth = this.AdditionalChargeForProduct.length;
    this.AdditionalChargeForProduct.push(this.formBuilder.group({
      OrderItemsId: product.OrderItemsId,
      AdditionalForProductId: [arrylendth],
      ProductId: product.ProductId,
      AdditionalChargeId: ['', Validators.required],
      Amount: ['', Validators.required],
      IsTaxble: product.IsTaxble,
      TaxId: ['']
    }));
  }
  public DeletePurchaseOrder(PurchaseOrderId: number) {
    if (PurchaseOrderId != 0) {
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
          const self = this;
          self.PurchaseOrderService.DeletePurchaseOrder(PurchaseOrderId)
            .subscribe((responce: any) => {
              if (responce.status) {
                Swal.fire('Success',responce.message,'info');
                this.GetPurchaseOrderList();
              }
            })
        }
      })
    }
  }

  //#region 
  get f() { return this.PurchaseOrderDetailsForm.controls; }
  get f1() { return this.PurchaseOrderTypeForm.controls; }
  get f2() { return this.OrderItemsForm.controls; }
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
  //#endregion
}
