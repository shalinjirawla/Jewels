import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { formatDate } from '@angular/common';
import { FormControl, FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { CustomerService } from '../../../Services/Customer-Services/customer.service';
import { ProductService } from '../../../Services/Products-Services/product.service';
import { TenantsServicesService } from './../../../Services/TenantsServices/tenants-services.service';
import { SalesOrderTypeService, CurrencyService, TaxCodeService, AdditionalChargeService, CreditTermsService, ShipmentMethodService } from '../../../Services/Masters-Services/general-setup.service';
import { SalesOrderService } from './../../../Services/Sales-Order-Services/sales-order.service';
import { SalesOrderDetailsVM, SalesOrderItemsVM, SalesOrderMergeVM, SalesOrdersVM, AdditionalChargeForAll, AdditionalChargeForProduct } from './../../../Models/SalesOrderModel/SalesOrderModel';
import { finalize } from 'rxjs/operators';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss']
})
export class SalesOrderComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  @ViewChild('largeModal1', { static: false }) public largeModal1: ModalDirective;
  constructor(private formBuilder: FormBuilder, private customerservice: CustomerService,
    private TenantsServicesService: TenantsServicesService,
    private SalesOrderTypeService: SalesOrderTypeService,
    private CurrencyService: CurrencyService,
    private CreditTermsService: CreditTermsService,
    private ShipmentMethodService: ShipmentMethodService,
    private AdditionalChargeService: AdditionalChargeService,
    private TaxCodeService: TaxCodeService,
    private SalesOrderService: SalesOrderService,
    private ProductService: ProductService, ) { }
  //Form List stat
  saving: boolean = false;
  applysaving: boolean = false;
  SalesOrderDetailsForm: FormGroup;
  SalesOrderTypeForm: FormGroup;
  AdditionalChargeFormForAll: FormGroup;
  AdditionalChargeFormForProduct: FormGroup;
  OrderItemsForm: FormGroup;
  //Form List end

  //btn submiited start
  SalesOrderTabSubmit: boolean = false;
  SalesOrderTabSubmitsecond: boolean = false;
  firsttab: boolean = true;
  secondtab: boolean = false;
  thirdtab: boolean = false;
  SalesOrderTypeTabSubmit: boolean = false;
  SalesOrdeItemTabSubmit: boolean = false;
  AdditionalChargeSubmit: boolean = false;
  //btn submitted end

  SaleTitle: string = "Add New Sales Order";
  SavebtnTitle: string = "Save & Next";
  CurrentDate: Date;
  EstimatedDeliveryDate: Date;
  AddressTypeTiltle: string = "SHIPPING ADDRESS";
  EmployeeModelLists: any[] = [];
  TenantUserList: any[] = [];
  CustomerList: any[] = [];
  SalesOrderTypeList: any[] = [];
  CurrencyList: any[] = [];
  CreditTermsList: any[] = [];
  ShipmentMethodList: any[] = [];
  SalesOrderRefList: any[] = [];
  AdditionalChargeList: any[] = [];
  TaxcodeList: any[] = [];
  ProductList: any[] = [];
  CustomerBillingAddress: any;
  CustomerShipingAddress: any;
  DiscountTypeList: any[] = [];

  SalesOrdersVM: SalesOrdersVM = new SalesOrdersVM();
  SalesOrderDetailsVM: SalesOrderDetailsVM = new SalesOrderDetailsVM();
  SalesOrderItemsVM: SalesOrderItemsVM = new SalesOrderItemsVM();
  SalesOrderItemsVMList: SalesOrderItemsVM[] = [];
  AdditionalChargeForAllModel: AdditionalChargeForAll = new AdditionalChargeForAll();
  AdditionalChargeForProductModel: AdditionalChargeForProduct = new AdditionalChargeForProduct();
  IsProductSalesOrderItemAdded: boolean = false;
  IsProductLoad: boolean = false;
  AdditionalChargeActive: string = "All";

  //bind salesOrder list var
  loadSalesOrderList: boolean = false;
  EmpltySalesOrderList: boolean = false;
  SalesOrderList: any[] = [];
  SalesOrderAdditionalchargeForAll: any[] = [];
  SalesOrderAdditionalchargeForProduct: any[] = [];
  ngOnInit() {
    this.CurrentDate = new Date();
    this.oninit();
    this.GetSalesOrderList()
  }
  public oninit() {
    this.onLoad();
    this.BindData();
    this.onLoadAdditionalChargeForAll();
    this.OnLoadAdditionalChargeForProduct();
  }
  public onLoad() {
    this.SalesOrderDetailsForm = this.formBuilder.group({
      SalesOrdersId: ['0'],
      SalesOrderNumber: [''],
      DateOrdered: ['', Validators.required],
      EstimatedDeliveryDate: ['', Validators.required],
      CustomerId: ['', Validators.required],
      CustomerPurchesOrderNumber: [''],
      Remarks: [''],
    });
    this.SalesOrderTypeForm = this.formBuilder.group({
      SalesOrderTypeId: ['', Validators.required],
      CreditTermId: [''],
      ShipmentMethodId: [''],
      SalesOrderRepId: [''],
      CurrencyId: [''],
    });

    this.OrderItemsForm = this.formBuilder.group({
      SalesOrderDetailsId: ['0'],
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
    self.customerservice.GetCustomerList().subscribe((responce: any) => {
      if (responce.body.data != null) {
        self.CustomerList = responce.body.data;
      }
    });
    self.SalesOrderTypeService.GetActiveSalesOrderTypeList().subscribe((responce: any) => {
      if (responce.status) {
        self.SalesOrderTypeList = responce.data;
      }
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
    self.SalesOrderRefList = [
      { 'salesOrderRef': 1, 'name': 'Admin' },
      { 'salesOrderRef': 2, 'name': 'SalesMan' },
      { 'salesOrderRef': 3, 'name': 'Purchaser' },
      { 'salesOrderRef': 4, 'name': 'Operator' },
      { 'salesOrderRef': 5, 'name': 'Manager' },
      { 'salesOrderRef': 6, 'name': 'Fulfillment' },
    ];
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
  public GetSalesOrderList() {
    const self = this;
    self.loadSalesOrderList = true;
    self.SalesOrderService.GetSalesOrderList()
      .pipe(finalize(() => { self.loadSalesOrderList = false }))
      .subscribe((responce: any) => {
        if (responce.status) {
          self.SalesOrderList = responce.data;
          if (self.SalesOrderList.length == 0)
            self.EmpltySalesOrderList = true;
          else
            self.EmpltySalesOrderList = false;
        }
      }, (errror) => {
        Swal.fire('error', errror.message, 'error');
      })
  }
  public SetFixedToDot(amount: number): any {
    if (amount != null)
      return Number(amount).toFixed(2);
  }
  public OpenSalesOrderModel() {
    this.CurrentDate = new Date();
    this.SalesOrderTabSubmit = false;
    this.SalesOrderTabSubmitsecond = false;
    this.SalesOrderTypeTabSubmit = false;
    this.largeModal.show();
    this.SaleTitle = "Add New Sales Order";
    this.CustomerChange(0);
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
    this.SalesOrdeItemTabSubmit = false;
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
    this.TotalCalcalate();
  }
  public TabChange(event: any) {
  }
  public SaveSalesOrder(SalesOrderDetailsForm: any, SalesOrderTypeForm: any, OrderItemsForm: any) {
    this.SalesOrderTabSubmit = true;
    if (this.SalesOrderDetailsForm.invalid) {
      document.getElementById("TabOrderDetail-link").click();
      return;
    }
    this.SalesOrderTabSubmitsecond = true;
    if (this.SalesOrderTypeForm.invalid) {
      document.getElementById("SaleOrderType-link").click();
      return;
    }

    if (document.getElementById('TabOrderDetail-link').className == "nav-link active") {
      document.getElementById("SaleOrderType-link").click();
      return
    }
    else if (document.getElementById('SaleOrderType-link').className == 'nav-link active') {
      this.SalesOrderTabSubmitsecond = true;
      if (this.SalesOrderTypeForm.invalid) {
        document.getElementById("SaleOrderType-link").click();
        return;
      } else {
        document.getElementById("SaleOrderItem-link").click();
        return;
      }
    } else if (document.getElementById('SaleOrderItem-link').className == 'nav-link active') { }
    const self = this;
    this.SalesOrdeItemTabSubmit = true;
    if (OrderItemsForm.invalid) {
      return;
    }
    self.saving = true;
    self.SalesOrderDetailsVM.AdditionalChargeForProduct = [];
    self.SalesOrderDetailsVM.AdditionalChargeForAll = [];
    self.SalesOrderItemsVMList = [];
    var SaleOrdeDetails = SalesOrderDetailsForm.value;
    var SalesOrderType = SalesOrderTypeForm.value;
    var SalesOrderItems = OrderItemsForm.value;
    // SalesOrder
    self.SalesOrdersVM.SalesOrdersId = Number(SaleOrdeDetails.SalesOrdersId);
    self.SalesOrdersVM.SalesOrderNumber = SaleOrdeDetails.SalesOrderNumber;
    self.SalesOrdersVM.DateOrdered = SaleOrdeDetails.DateOrdered;
    self.SalesOrdersVM.EstimatedDeliveryDate = SaleOrdeDetails.EstimatedDeliveryDate;
    self.SalesOrdersVM.CustomerId = Number(SaleOrdeDetails.CustomerId);
    self.SalesOrdersVM.CustomerPurchesOrderNumber = SaleOrdeDetails.CustomerPurchesOrderNumber;
    self.SalesOrdersVM.Remarks = SaleOrdeDetails.Remarks;
    self.SalesOrdersVM.SalesOrderTypeId = Number(SalesOrderType.SalesOrderTypeId);
    self.SalesOrdersVM.CreditTermId = Number(SalesOrderType.CreditTermId);
    self.SalesOrdersVM.ShipmentMethodId = Number(SalesOrderType.ShipmentMethodId);
    self.SalesOrdersVM.CurrencyId = Number(SalesOrderType.CurrencyId);
    self.SalesOrdersVM.SalesOrderRepId = Number(SalesOrderType.SalesOrderRepId);

    //Sales Order Details
    self.SalesOrderDetailsVM.SalesOrderDetailsId = Number(SalesOrderItems.SalesOrderDetailsId);
    self.SalesOrderDetailsVM.SalesOrdersId = Number(SaleOrdeDetails.SalesOrdersId);
    // self.SalesOrderDetailsVM.AdditionalCharge = JSON.stringify(self.AdditionalChargeForAll.value);
    self.SalesOrderDetailsVM.TotalQTY = Number(SalesOrderItems.TotalQTY);
    self.SalesOrderDetailsVM.Total = Number(SalesOrderItems.Total);
    self.SalesOrderDetailsVM.FinalTotal = Number(SalesOrderItems.FinalTotal);
    self.SalesOrderDetailsVM.TaxInclude = SalesOrderItems.TaxInclude;
    self.SalesOrderDetailsVM.FinalTaxTotal = Number(SalesOrderItems.FinalTaxTotal);
    self.SalesOrderDetailsVM.AdditionalChargeAmount = Number(SalesOrderItems.AdditionalChargeAmount);
    self.SalesOrderDetailsVM.IsAdditionalChargeApply = SalesOrderItems.IsAdditionalChargeApply;
    self.SalesOrderDetailsVM.IsAdditionalChargeApplyType = SalesOrderItems.IsAdditionalChargeApplyType;

    //Additionla charge for All and product
    if (self.SalesOrderDetailsVM.IsAdditionalChargeApplyType != '') {
      if (self.SalesOrderDetailsVM.IsAdditionalChargeApplyType == 'All') {
        self.AdditionalChargeForAll.value.forEach(element => {
          self.AdditionalChargeForAllModel.SalesOrdersId = Number(element.OrderItemsId);
          self.AdditionalChargeForAllModel.AdditionalForAllId = Number(element.AdditionalForAllId);
          self.AdditionalChargeForAllModel.AdditionalChargeId = Number(element.AdditionalChargeId);
          self.AdditionalChargeForAllModel.TaxId = Number(element.TaxId);
          self.SalesOrderDetailsVM.AdditionalChargeForAll.push(self.AdditionalChargeForAllModel);
          self.AdditionalChargeForAllModel = new AdditionalChargeForAll();
        });
        self.SalesOrderDetailsVM.AdditionalChargeForProduct = [];
      } else if (self.SalesOrderDetailsVM.IsAdditionalChargeApplyType == 'Product') {
        self.AdditionalChargeForProduct.value.forEach(element => {
          self.AdditionalChargeForProductModel.SalesOrdersId = Number(element.OrderItemsId);
          self.AdditionalChargeForProductModel.AdditionalChargeForProductId = Number(element.AdditionalForProductId);
          self.AdditionalChargeForProductModel.ProductId = Number(element.ProductId);
          self.AdditionalChargeForProductModel.AdditionalChargeId = Number(element.AdditionalChargeId);
          self.AdditionalChargeForProductModel.IsTaxble = element.IsTaxble;
          self.AdditionalChargeForProductModel.TaxId = Number(element.TaxId);
          self.SalesOrderDetailsVM.AdditionalChargeForProduct.push(self.AdditionalChargeForProductModel);
          self.AdditionalChargeForProductModel = new AdditionalChargeForProduct();
        });
        self.SalesOrderDetailsVM.AdditionalChargeForAll = [];
      } else {
        self.SalesOrderDetailsVM.AdditionalChargeForProduct = [];
        self.SalesOrderDetailsVM.AdditionalChargeForAll = [];
      }
    }
    //Sales Order Items list
    OrderItemsForm.value.OrderItems.forEach(element => {
      self.SalesOrderItemsVM.OrderItemsId = element.OrderItemsId;
      self.SalesOrderItemsVM.ProductId = Number(element.ProductId);
      self.SalesOrderItemsVM.UnitPrice = Number(element.UnitPrice);
      self.SalesOrderItemsVM.Unit = Number(element.Unit);
      self.SalesOrderItemsVM.QTY = Number(element.QTY);
      self.SalesOrderItemsVM.DiscountType = Number(element.DiscountType);
      self.SalesOrderItemsVM.Discount = Number(element.Discount);
      self.SalesOrderItemsVM.TaxId = Number(element.TaxId);
      self.SalesOrderItemsVM.IsTaxble = element.IsTaxble;
      self.SalesOrderItemsVM.TaxTotal = Number(element.TaxTotal);
      self.SalesOrderItemsVM.Total = Number(element.Total);
      self.SalesOrderItemsVMList.push(self.SalesOrderItemsVM);
      self.SalesOrderItemsVM = new SalesOrderItemsVM();
    });
    const input = {
      'SalesOrdersVM': self.SalesOrdersVM,
      'SalesOrderDetailsVM': self.SalesOrderDetailsVM,
      'SalesOrderItemsList': self.SalesOrderItemsVMList
    }


    self.SalesOrderService.AddEditSalesOrder(input)
      .pipe(finalize(() => { self.saving = false }))
      .subscribe((responce: any) => {
        Swal.fire('Success', responce.message, 'success');
        this.Close();
        this.GetSalesOrderList();
      }, (error) => {
        Swal.fire('Error', error.message, 'error');
      })



  }
  public CustomerChange(CustomerId: number) {
    if (CustomerId != 0) {
      const self = this;
      self.customerservice.GetCustomerAddress(CustomerId).subscribe((responce: any) => {
        if (responce.status) {
          if (responce.body.data.billingAddress != null && responce.body.data.billingAddress != undefined)
            self.CustomerBillingAddress = responce.body.data.billingAddress;
          if (responce.body.data.shippingAddress != null && responce.body.data.shippingAddress != undefined)
            self.CustomerShipingAddress = responce.body.data.shippingAddress;
        }
      });
    } else {
      this.CustomerBillingAddress = null;
      this.CustomerShipingAddress = null;
    }
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
    this.SalesOrderDetailsVM.AdditionalChargeForProduct = [];
    this.SalesOrderDetailsVM.AdditionalChargeForAll = [];
    this.SalesOrderItemsVMList = [];
  }
  public OpenAdditionlChargeModel() {
    this.largeModal1.show();
    this.AdditionalChargeSubmit = false;
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
    // var IsExist = arrobj.value.filter(x => x.AdditionalChargeId == SelectedAdditionalCharge);
    // if (IsExist != null && IsExist != undefined && IsExist.length > 1) {
    //   Amount = '';
    //   SelectedAdditionalCharge = '';
    //   Swal.fire('Warning', 'This Additional Charge already Selected..', 'info');
    // } else {
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
  public GetSalesOrder(SalesOrderId: number) {
    if (SalesOrderId != 0) {
      const self = this;
      self.SalesOrderService.GetSalesOrder(SalesOrderId).subscribe((responce: any) => {
        if (responce != null && responce.status) {
          debugger
          var SalesOrder = responce.data.salesOrdersVM;
          var SalesOrderType = responce.data.salesOrderDetailsVM;
          var SalesOrderItems = responce.data.salesOrderItemsList;
          self.SalesOrderDetailsForm.patchValue({
            SalesOrdersId: SalesOrder.salesOrdersId,
            SalesOrderNumber: SalesOrder.salesOrderNumber,
            DateOrdered: new Date(SalesOrder.dateOrdered),
            EstimatedDeliveryDate: new Date(SalesOrder.estimatedDeliveryDate),
            CustomerId: SalesOrder.customerId,
            CustomerPurchesOrderNumber: SalesOrder.customerPurchesOrderNumber,
            Remarks: SalesOrder.remarks,
          })
          this.CustomerChange(SalesOrder.customerId);
          this.CurrentDate = new Date(self.SalesOrderDetailsForm.value.DateOrdered);
          this.EstimatedDeliveryDate = new Date(self.SalesOrderDetailsForm.value.EstimatedDeliveryDate);
          this.SalesOrderTypeForm.patchValue({
            SalesOrderTypeId: SalesOrder.salesOrderTypeId,
            CreditTermId: SalesOrder.creditTermId,
            ShipmentMethodId: SalesOrder.shipmentMethodId,
            SalesOrderRepId: SalesOrder.salesOrderRepId,
            CurrencyId: SalesOrder.currencyId,
          });

          this.OrderItemsForm.patchValue({
            SalesOrderDetailsId: SalesOrderType.salesOrderDetailsId,
            TotalQTY: SalesOrderType.totalQTY,
            Total: SalesOrderType.total,
            FinalTotal: SalesOrderType.finalTotal,
            TaxInclude: SalesOrderType.taxInclude,
            FinalTaxTotal: SalesOrderType.finalTaxTotal,
            AdditionalChargeAmount: SalesOrderType.additionalChargeAmount,
            IsAdditionalChargeApply: SalesOrderType.isAdditionalChargeApply,
            IsAdditionalChargeApplyType: SalesOrderType.isAdditionalChargeApplyType,
          });
          var index = 0;
          SalesOrderItems.forEach(element => {
            this.BindSalesOrderItem(element, index);
            index++;
          });
          if (SalesOrderType.isAdditionalChargeApply) {
            if (SalesOrderType.isAdditionalChargeApplyType == 'All') {
              self.SalesOrderAdditionalchargeForAll = SalesOrderType.additionalChargeForAll;
              if (self.SalesOrderAdditionalchargeForAll != null && self.SalesOrderAdditionalchargeForAll.length > 0) {
                let index = 0;
                self.SalesOrderAdditionalchargeForAll.forEach(element => {
                  this.BindAddionalChargeForAll(element, index);
                  index++;
                });
              }
            }
            else if (SalesOrderType.isAdditionalChargeApplyType == 'Product') {
              self.SalesOrderAdditionalchargeForProduct = SalesOrderType.additionalChargeForProduct;
              if (self.SalesOrderAdditionalchargeForProduct != null && self.SalesOrderAdditionalchargeForProduct.length > 0) {
                let index = 0;
                self.SalesOrderAdditionalchargeForProduct.forEach(element => {
                //  this.BindAddionalChargeForProduct(element, index);
                  index++;
                });
              }
            }
          }
          self.OpenSalesOrderModel();
          self.SaleTitle = "Edit Sales Order";
        } else
          Swal.fire('error', responce.message, 'error');
      }, (error) => {
        console.log(error.message);
      });
    }
  }
  public BindSalesOrderItem(SalesOrderItems: any, index?) {
    let arrobj = this.OrderItemsForm.get('OrderItems') as FormArray;
    if (index > 0)
      this.AddOrderItems();
    arrobj.controls[index].patchValue({
      OrderItemsId: SalesOrderItems.orderItemsId,
      ProductId: SalesOrderItems.productId,
      Unit: SalesOrderItems.unit,
      UnitPrice: SalesOrderItems.unitPrice,
      QTY: SalesOrderItems.qty,
      DiscountType: SalesOrderItems.discountType,
      Discount: SalesOrderItems.discount,
      IsTaxble: SalesOrderItems.isTaxble,
      TaxId: SalesOrderItems.taxId,
      TaxTotal: SalesOrderItems.taxTotal,
      Total: SalesOrderItems.total,
    });

  }
  public BindAddionalChargeForAll(all: any, index) {
    let arrobj = this.AdditionalChargeFormForAll.get('AdditionalChargeForAll') as FormArray;
    if (index > 0)
      this.AddAdditionalChargeForAll();
    arrobj.controls[index].patchValue({
      OrderItemsId: all.salesOrdersId,
      AdditionalForAllId: all.additionalForAllId,
      AdditionalChargeId:all.additionalChargeId,
      Amount:0,
      TaxId: all.taxId,
    });
  }
  public BindAddionalChargeForProduct(product: any, index) {
    let arrobj = this.AdditionalChargeFormForProduct.get('AdditionalChargeForProduct') as FormArray;
    if (index > 0)
      this.AddAdditionalChargeForProduct(product, index);
    arrobj.controls[index].patchValue({
      OrderItemsId: [''],
      AdditionalForProductId: [''],
      ProductId: ['', Validators.required],
      AdditionalChargeId: ['', Validators.required],
      Amount: ['', Validators.required],
      IsTaxble: [false],
      TaxId: ['']
    });
  }
  public DeleteSalesOrder(SalesOrderId: number) {
    if (SalesOrderId != 0) {
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
          self.SalesOrderService.DeleteSalesOrder(SalesOrderId)
            .subscribe((responce: any) => {
              if (responce.status) {
                Swal.fire(
                  responce.message,
                  '',
                  'success'
                )
                this.GetSalesOrderList();
              }
            })
        }
      })
    }
  }

  //#region 
  get f() { return this.SalesOrderDetailsForm.controls; }
  get f1() { return this.SalesOrderTypeForm.controls; }
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
