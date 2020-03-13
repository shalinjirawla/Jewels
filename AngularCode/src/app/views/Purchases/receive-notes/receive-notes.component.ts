import { Component, OnInit, ViewChild, NgModule } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormArray, Validators, NgModel } from '@angular/forms';
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service';
import { ProductService } from '../../../Services/Products-Services/product.service';
import { PurchaseOrderService } from '../../../Services/Purchase-Order-Services/purchase-order-services.services';
import { CurrencyService, PaymentTermService, ShipmentTermService, WarehouseService, ShipmentMethodService } from '../../../Services/Masters-Services/general-setup.service';
import { finalize } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { ReceiveNotesServicesService } from './../../../Services/Purchase-Order-Services/receive-notes-services.service';
import { ReceiveNotesItemsModel, ReceiveNotesModel } from './../../../Models/PurchaseOrderModel/ReceiveNotes';
import { error, element } from 'protractor';
@Component({
  selector: 'app-receive-notes',
  templateUrl: './receive-notes.component.html',
  styleUrls: ['./receive-notes.component.scss']
})
export class ReceiveNotesComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private formBuilder: FormBuilder,
    private SupplierServicesService: SupplierServicesService,
    private CurrencyService: CurrencyService,
    private ShipmentTermService: ShipmentTermService,
    private ShipmentMethodService: ShipmentMethodService,
    private ProductService: ProductService,
    private PurchaseOrderService: PurchaseOrderService,
    private PaymentTermService: PaymentTermService,
    private ReceiveNotesServicesService: ReceiveNotesServicesService,
    private WarehouseService: WarehouseService) { }
  //Form List stat
  saving: boolean = false;
  ReceiveNotesForm: FormGroup;
  AddItemsForm: FormGroup;
  PurchaseOrderItemsList: FormGroup;
  ProductItemsList: FormGroup;
  CurrentDate: any;
  SupplierSelected: boolean = false;
  EmpltyReceiveNotesList: boolean = false;
  loadReceiveNotesList: boolean = false;
  IsLoadData: boolean = true;
  //Form List end
  //title start
  ReceiveNoteTitle = "Add New Receive Notes.";
  ReceiveNotebtnTitle: string = "Save & Next";
  ReveiveSubmit: boolean = false;
  ReveiveItemsSubmit: boolean = false;
  //title end
  //list of data start
  SupplierList: any[] = [];
  CurrencyList: any[] = [];
  ShipmentMethodList: any[] = [];
  ShipmentTermsList: any[] = [];
  AdditionalChargeList: any[] = [];
  PaymentTermsList: any[] = [];
  PurchaseOrderProductList: any[] = [];
  OriginalPurchaseOrderProductList: any[] = [];
  PurchaseOrderList: any[] = [];
  SupplierBillingAddress: any;
  SupplierShipingAddress: any;
  SelectedPurchaseOrderName: string;
  SelectedProductList: any[] = [];
  WarehouseList: any[] = [];
  IsValidProductList: boolean = false;
  SelectedAll: boolean = false;
  ReceiveNotesItemsModel: ReceiveNotesItemsModel = new ReceiveNotesItemsModel();
  ReceiveNotesModel: ReceiveNotesModel = new ReceiveNotesModel();
  ReceiveNotesModelList: any[] = [];
  ReceiveNotesList: any[] = [];
  //list of data end
  ngOnInit() {
    this.onLoad();
    this.CurrentDate = new Date();
    this.BindData();
    this.GetReceiveNotesList();
  }
  public onLoad() {
    this.ReceiveNotesForm = this.formBuilder.group({
      ReceiveNotesId: ['0'],
      ReceiveNotesNumber: [''],
      ReceiveDate: [''],
      SupplierId: ['', Validators.required],
      Remarks: [''],
      Status: [''],
    });
    this.OnLoadAddItemsForm();
  }
  public GetReceiveNotesList() {
    const self = this;
    self.loadReceiveNotesList = true;
    self.ReceiveNotesServicesService.GetReceiveNotesList()
      .pipe(finalize(() => { self.loadReceiveNotesList = false }))
      .subscribe((responce: any) => {
        if (responce != null && responce.status) {
          if (responce.data != null && responce.data.length > 0) {
            self.ReceiveNotesList = responce.data;
            self.EmpltyReceiveNotesList = false;
          } else {
            self.EmpltyReceiveNotesList = true;
            self.ReceiveNotesList = [];
          }
        }
      });
  }
  public OnLoadAddItemsForm() {
    this.AddItemsForm = this.formBuilder.group({
      ReceiveNotesId: ['0'],
      PurchaseOrderId: ['', Validators.required],
      IsselectedAll: [false],
      PurchaseOrderItemList: this.formBuilder.array([this.formBuilder.group({
        ProductPurchaseOrderId: [''],
        ReceiveNoteItemId: ['0'],
        Code: [''],
        ProductId: ['0'],
        ProductName: [''],
        ProductCode: [''],
        QTY: [''],
        UOM: [''],
        Price: [''],
        Isselected: [false],
        WarehouseId: ['', Validators.required],
        ProductQTY: [null, Validators.required],
        UniquIndex: ['0'],
      })]),

    });
  }
  public BindData() {
    const self = this;
    self.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        self.CurrencyList = responce.data;
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
    self.ShipmentTermService.GetShipmentTermList().subscribe((responce: any) => {
      if (responce.status) {
        self.ShipmentTermsList = responce.data;
      }
    });
    self.SupplierServicesService.GetSuppliersList().subscribe((responce: any) => {
      if (responce.status && responce.data != null) {
        self.SupplierList = responce.data;
      }
    });
    self.WarehouseService.GetLocationList().subscribe((responce: any) => {
      if (responce.status && responce.data != null) {
        self.WarehouseList = responce.data;
      }
    });
  }
  public AddProductOrderItems(product: any) {
    this.PurchaseOrderItemList.push(this.formBuilder.group({
      ProductPurchaseOrderId: product.purchaseOrderId,
      ReceiveNoteItemId: product.receiveNoteItemId,
      Isselectedall: false,
      Code: this.SelectedPurchaseOrderName,
      ProductId: product.productId,
      ProductName: product.productName,
      ProductCode: product.productCode,
      QTY: product.qty,
      UOM: product.uom,
      Price: product.price,
      Isselected: false,
      WarehouseId: ['', Validators.required],
      ProductQTY: [null, Validators.required],
      UniquIndex: product.uniquIndex,
    }));
  }
  public SupplierChange(SupplierId: number) {
    if (SupplierId != 0) {
      const self = this;
      self.SupplierServicesService.GetSuppliersAddress(SupplierId).subscribe((responce: any) => {
        if (responce.status) {
          self.SupplierSelected = true;
          if (responce.data.billingAddress != null && responce.data.billingAddress != undefined)
            self.SupplierBillingAddress = responce.data.billingAddress;
          else
            this.SupplierBillingAddress = null;
          if (responce.data.shippingAddress != null && responce.data.shippingAddress != undefined)
            self.SupplierShipingAddress = responce.data.shippingAddress;
          else
            this.SupplierShipingAddress = null;
        } else
          self.SupplierSelected = false;
        self.PurchaseOrderProductList = [];
        self.SelectedPurchaseOrderName = null;
      });
      this.BindPurchaseOrderList(SupplierId);
    } else {
      this.SupplierBillingAddress = null;
      this.SupplierShipingAddress = null;
      this.SupplierSelected = false;
      this.PurchaseOrderProductList = [];
      this.SelectedPurchaseOrderName = null;
    }
    this.OnLoadAddItemsForm();
    this.IsValidProductList = false;
  }
  public RemoveAllSelectedCheckbox() {
    var element = <HTMLInputElement>document.getElementById("SeletceAll");
    element.checked = false;
    this.SelectedAll = false;
  }
  public BindPurchaseOrderList(SupplierId: number) {
    if (SupplierId != 0) {
      const self = this;
      self.PurchaseOrderService.GetPurchaseOrderlistIdBySupplier(SupplierId)
        .subscribe((responce: any) => {
          if (responce.status) {
            self.PurchaseOrderList = responce.data;

          }
        })
    }
  }
  public CheckIsValidProductList() {
    let product = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
    if (product != null && product != undefined) {
      if (product.value.ProductId != 0)
        this.IsValidProductList = true;
      else
        this.IsValidProductList = false;
    }
  }
  public PurhcaseOrderChange(PurchaseOrderId: number) {
    if (PurchaseOrderId != 0) {
      const self = this;
      self.PurchaseOrderService.GetProductIdByPurchaseOrder(PurchaseOrderId)
        .subscribe((responce: any) => {
          if (responce.status) {
            self.PurchaseOrderProductList = responce.data;
            self.OriginalPurchaseOrderProductList = responce.data;
            this.IsExistProductInProductList();
            var obj = self.PurchaseOrderList.filter(x => x.purchaseOrderId == PurchaseOrderId);
            if (obj != null && obj.length > 0) {
              this.SelectedPurchaseOrderName = obj[0].code;
            } else {
              this.SelectedPurchaseOrderName = null;
            }
          }
        })

    } else {
      this.SelectedPurchaseOrderName = null;
    }
  }

  public OpenReceiveNoteModel() {
    this.largeModal.show();
    this.IsLoadData = false;
  }
  public Close() {
    this.largeModal.hide();
    this.ResetForm();
    this.SupplierSelected = false;
  }
  public MoveToAllProductItems(productList: any) {
    if (productList != null && productList.length > 0) {
      let index = 0;
      let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
      this.OriginalPurchaseOrderProductList.forEach(element => {
        let isexist = productlist.value.filter(x => x.UniquIndex == element.uniquIndex);
        if (isexist.length == 0) {
          this.MoveToSelectedProductItems(element, index);
        }
        index++;
      });
      this.IsExistProductInProductList();
    }
  }
  public MoveToSelectedProductItems(product: any, index: any) {
    if (product != null) {
      let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
      if (productlist != null && productlist.length > 0) {
        if (productlist.value[0].ProductId != 0)
          this.AddProductOrderItems(product)
        else
          productlist.controls[productlist.length - 1].patchValue({
            ProductPurchaseOrderId: product.purchaseOrderId,
            ReceiveNoteItemId: '',
            Isselectedall: false,
            Code: this.SelectedPurchaseOrderName,
            ProductId: product.productId,
            ProductName: product.productName,
            ProductCode: product.productCode,
            QTY: product.qty,
            UOM: product.uom,
            Price: product.price,
            Isselected: false,
            WarehouseId: '',
            ProductQTY: null,
            UniquIndex: product.uniquIndex,
          });
        this.CheckIsValidProductList();
        this.PurchaseOrderProductList.splice(index, 1);
      }
      else {
        this.OnLoadAddItemsForm();
      }
    }
  }
  public IsExistProductInProductList() {
    let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
    if (productlist != null && productlist != undefined) {
      this.PurchaseOrderProductList = [];
      this.OriginalPurchaseOrderProductList.forEach(originalelement => {
        var isexist = productlist.value.filter(x => x.UniquIndex == originalelement.uniquIndex);
        if (isexist.length == 0) {
          this.PurchaseOrderProductList.push(originalelement);
        }
      });
    }
  }
  public ResetForm() {
    this.IsValidProductList = false;
    this.onLoad();
    document.getElementById('Tabinfo-link').click();
    this.SelectedPurchaseOrderName = null;
    this.ReveiveSubmit = false;
  }
  public SelectAllItemForRemove(event: any) {
    let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
    if (productlist != null && productlist != undefined) {
      let index = 0
      productlist.value.forEach(product => {
        productlist.controls[index].patchValue({
          Isselected: event,
        });
        index++;
      });
      this.AddItemsForm.patchValue({
        IsselectedAll: event
      })
    }
  }
  public RemoveSelectedProduct() {
    let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
    if (productlist != null && productlist != undefined) {
      let index = 0
      productlist.value.forEach(product => {
        if (product.Isselected)
          this.PurchaseOrderItemList.removeAt(index);
        if (this.PurchaseOrderItemList.length == 1 && this.AddItemsForm.value.IsselectedAll) {
          productlist.controls[0].patchValue({
            ProductPurchaseOrderId: product.purchaseOrderId,
            Isselectedall: false,
            Code: '',
            ProductId: '',
            ProductName: '',
            ProductCode: '',
            QTY: '',
            UOM: '',
            Price: '',
            Isselected: false,
            WarehouseId: '0',
            ProductQTY: '0',
            UniquIndex: '',
          })
          this.IsValidProductList = false;
        }
        index++;
      });

      this.IsExistProductInProductList();
      this.RemoveAllSelectedCheckbox();
    }
  }
  public CheckIsValidQTY(currentvalue: number, QTYvalue: number, index) {
    currentvalue = Number(currentvalue);
    QTYvalue = Number(QTYvalue);
    if (currentvalue > QTYvalue) {
      let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
      productlist.controls[index].patchValue({
        ProductQTY: QTYvalue,
      })
    }

  }
  public SaveReceiveNotes(ReceiveNotesForm: any, AddItemsForm: any) {
    this.ReveiveSubmit = true;
    if (ReceiveNotesForm.invalid) {
      document.getElementById('Tabinfo-link').click();
      return;
    }
    const self = this;
    if (document.getElementById('Tabinfo-link').className == 'nav-link active') {
      document.getElementById('Tabitems-link').click();
      return;
    }
    self.ReveiveItemsSubmit = true;
    debugger
    if (AddItemsForm.invalid) {
      if (AddItemsForm.value.PurchaseOrderItemList != null && AddItemsForm.value.PurchaseOrderItemList[0].ProductId == 0) {
        Swal.fire('warning', "Please Add atleast one product", 'warning');
        return;
      }
      Swal.fire('warning', "Please Fill Reqiured Fields", 'warning');
      return;
    }
    self.ReceiveNotesModelList = [];
    self.ReceiveNotesModel = new ReceiveNotesModel();
    self.ReceiveNotesModel.ReceiveNoteId = Number(ReceiveNotesForm.value.ReceiveNotesId);
    self.ReceiveNotesModel.ReceiveNoteNumber = ReceiveNotesForm.value.ReceiveNotesNumber;
    self.ReceiveNotesModel.ReceiveDate = self.CurrentDate;
    self.ReceiveNotesModel.SupplierId = Number(ReceiveNotesForm.value.SupplierId);
    self.ReceiveNotesModel.Remarks = ReceiveNotesForm.value.Remarks;
    if (AddItemsForm.value != null && AddItemsForm.value.PurchaseOrderItemList != null && AddItemsForm.value.PurchaseOrderItemList.length > 0) {
      AddItemsForm.value.PurchaseOrderItemList.forEach(element => {
        if (element.ProductId != null && element.ProductId != undefined && element.ProductId != '') {
          self.ReceiveNotesItemsModel.ReceiveNoteItemId = Number(AddItemsForm.value.ReceiveNoteItemId ? AddItemsForm.value.ReceiveNoteItemId : 0);
          self.ReceiveNotesItemsModel.ReceiveNoteId = Number(AddItemsForm.value.ReceiveNotesId);
          self.ReceiveNotesItemsModel.PurchaseOrdersId = Number(element.ProductPurchaseOrderId);
          self.ReceiveNotesItemsModel.ProductId = Number(element.ProductId);
          self.ReceiveNotesItemsModel.WarehouseId = Number(element.WarehouseId);
          self.ReceiveNotesItemsModel.ProductQTY = Number(element.ProductQTY);
          self.ReceiveNotesModelList.push(self.ReceiveNotesItemsModel);
          self.ReceiveNotesItemsModel = new ReceiveNotesItemsModel();
        }
      });
    }
    self.ReceiveNotesModel.ProductList = self.ReceiveNotesModelList;
    self.ReceiveNotesServicesService.AddReceiveNotes(self.ReceiveNotesModel)
      .pipe(finalize(() => { self.ReveiveItemsSubmit = false }))
      .subscribe((responce: any) => {
        if (responce != null && responce.status) {
          this.GetReceiveNotesList();
          this.ResetForm();
          this.Close();
          Swal.fire('Success', responce.message, 'success');
        }
      }, (error) => {
        Swal.fire('Error', error, 'error');
      });

  }
  public GetReceiveNotesDetails(ReceiveNotesId: number) {
    if (ReceiveNotesId != 0) {
      const self = this;
      self.ReceiveNotesServicesService.GetReceiveNotes(ReceiveNotesId).subscribe((responce: any) => {
        if (responce != null && responce.status) {
          var ReceiveNotesModel = responce.data;
          var ReceiveNotesModelList = responce.data.productList;
          this.ReceiveNotesForm.patchValue({
            ReceiveNotesId: ReceiveNotesModel.receiveNoteId,
            ReceiveNotesNumber: ReceiveNotesModel.receiveNoteNumber,
            ReceiveDate: new Date(ReceiveNotesModel.receiveDate),
            SupplierId: ReceiveNotesModel.supplierId,
            Remarks: ReceiveNotesModel.remarks,
          });
          this.OpenReceiveNoteModel();
          this.IsLoadData = true;
          this.SupplierChange(ReceiveNotesModel.supplierId);
          if (ReceiveNotesModelList != null && ReceiveNotesModelList.length > 0) {
            this.AddItemsForm.patchValue({
              ReceiveNotesId: ReceiveNotesModelList[0].receiveNoteId,
              PurchaseOrderId: ReceiveNotesModelList[0].purchaseOrdersId
            });
            setTimeout(() => {
              this.PurhcaseOrderChange(ReceiveNotesModelList[0].purchaseOrdersId);
              setTimeout(() => {
                // this.MoveToAllProductItems(ReceiveNotesModelList);
                // this.IsLoadData = false;
                let productlist = this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
                if (productlist != null && productlist.length > 0) {
                  let index = 0;
                  this.PurhcaseOrderChange(ReceiveNotesModelList[0].purchaseOrdersId);
                  ReceiveNotesModelList.forEach(element => {
                    var product = this.OriginalPurchaseOrderProductList.filter(x => x.productId == element.productId);
                    if (productlist.value[0].ProductId != 0)
                      this.EditAddProductOrderItems(product, element.receiveNoteItemId, element.warehouseId, element.productQTY)
                    else
                      productlist.controls[productlist.length - 1].patchValue({
                        ProductPurchaseOrderId: element.purchaseOrdersId,
                        ReceiveNoteItemId: element.receiveNoteItemId,
                        Isselectedall: false,
                        Code: this.SelectedPurchaseOrderName,
                        ProductId: product[0].productId,
                        ProductName: product[0].productName,
                        ProductCode: product[0].productCode,
                        QTY: product[0].qty,
                        UOM: product[0].uom,
                        Price: product[0].price,
                        Isselected: false,
                        WarehouseId: element.warehouseId,
                        ProductQTY: element.productQTY,
                        UniquIndex: product[0].uniquIndex,
                      });
                    this.CheckIsValidProductList();
                    this.PurchaseOrderProductList.splice(index, 1);
                    index++;
                  });
                  this.IsLoadData = false;

                }
                else {
                  this.OnLoadAddItemsForm();
                }
              }, 500);
            }, 1000);
          }
          this.ReceiveNoteTitle = "Edit Receive Notes";
        }
      })
    }
  }
  public EditAddProductOrderItems(product: any, ReceiveNoteItemId: any, WarehouseId: number, ProductQTY: number) {
    var obj = product[0];
    this.PurchaseOrderItemList.push(this.formBuilder.group({
      ProductPurchaseOrderId: obj.purchaseOrderId,
      ReceiveNoteItemId: ReceiveNoteItemId,
      Isselectedall: false,
      Code: this.SelectedPurchaseOrderName,
      ProductId: obj.productId,
      ProductName: obj.productName,
      ProductCode: obj.productCode,
      QTY: obj.qty,
      UOM: obj.uom,
      Price: obj.price,
      Isselected: false,
      WarehouseId: [WarehouseId, Validators.required],
      ProductQTY: [ProductQTY, Validators.required],
      UniquIndex: obj.uniquIndex,
    }));
  }
  public DeleteReceiveNotes(ReceiveNotesId: number) {
    if (ReceiveNotesId != 0) {
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
          self.ReceiveNotesServicesService.DeleteReceiveNotes(ReceiveNotesId)
            .subscribe((responce: any) => {
              if (responce.status) {
                Swal.fire('Success', responce.message, 'info');
                this.GetReceiveNotesList();
              }
            })
        }
      })
    }
  }

  get PurchaseOrderItemList() {
    return this.AddItemsForm.get('PurchaseOrderItemList') as FormArray;
  }
  get ProductItemList() {
    return this.AddItemsForm.get('ProductItemsList') as FormArray;
  }
  get f() { return this.ReceiveNotesForm.controls }
  get f1() { return this.AddItemsForm.controls }
  get f2() { return this.PurchaseOrderItemList.controls }
  //#region 

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
