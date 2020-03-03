import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, NgModel } from '@angular/forms';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import { WarehouseService ,TaxCodeService} from '../../../Services/Masters-Services/general-setup.service';
import Swal from 'sweetalert2';
import { RawMaterailsService } from '../../../Services/RawMaterails-Services/raw-materails.service'
import { ProductService } from '../../../Services/Products-Services/product.service';
import { finalize } from 'rxjs/operators';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  @ViewChild('VariantImagesModel', { static: false }) public VariantImagesModel: ModalDirective;
  constructor(private FormBuilder: FormBuilder,
    private ProductBrandService: ProductBrandService,
    private ProductCategoriesService: ProductCategoriesService,
    private SupplierServicesService: SupplierServicesService,
    private WarehouseService: WarehouseService,
    private RawMaterailsService: RawMaterailsService,
    private ProductService: ProductService,
    private TaxCodeService: TaxCodeService,
  ) { }

  saving: boolean = false;
  FormSubmitted: boolean = false;
  //Title For Model Start..
  ModelTitleString: string = "Add New Product";
  //Title For Model End...
  //Form List for Product Start..
  ProductForm: FormGroup;
  ProductVariantForm: FormGroup;
  AddCustomerForm: FormGroup;
  CategoriesList: any[];
  DefaultSupplierList: any[];
  VariantoptionValueLabellist: any = [];
  VariantoptionValueLabellistEmplty: boolean = true;
  BrandList: any[];
  IsBatchOrSerialSelect: boolean = false;
  IsRawMaterailSelect: boolean = false;
  isProductVarintsSelect: boolean = false;
  ProductNoDataFound: boolean = true;
  VariantOptionType: boolean = false;

  //Form List for Product End//
  games: any
  key: string = 'name';
  reverse: boolean = false;
  filter: NgModel;


  //suppier start
  Productlaod: boolean = false;

  ProductList: any[] = [];
  DefaultTax: any[] = [];
  VarinatOptions: any[] = [];
  WareHouseLocationList: any[] = [];
  RawMaterailsList: any[];
  ProductIsVariantLoaded: boolean = false;
  Base64File: any;
  ProductVarinat: any = {};
  //suppier end
  sort(key) {
    this.key = key;
    this.reverse = !this.reverse;
  }
  p: number = 1;
  listofCount: number = 5;
  public ListOfData(Count: number) {
    this.listofCount = Count;
  }
  ngOnInit() {
    this.OnLoad();
    this.GetDropDownList();
    this.GetProductList();
  }
  public ShowModel() {
    this.ModelTitleString = "Add New Product";
    $('#ProductVariant-link').parent().hide();
    this.largeModal.show();
  }
  public GetDropDownList() {
    this.ProductCategoriesService.GetProductCategoriesList().subscribe((responce: any) => {
      if (responce.status) {
        this.CategoriesList = responce.data;
      }
    });
    this.ProductBrandService.GetProductBrandList().subscribe((responce: any) => {
      if (responce.status) {
        this.BrandList = responce.data;
      }
    });
    this.SupplierServicesService.GetDafaultSuppliers().subscribe((responce: any) => {
      this.DefaultSupplierList = responce.data;

    });
    this.WarehouseService.GetLocationList().subscribe((responce: any) => {
      if (responce.status) {
        this.WareHouseLocationList = responce.data;
      }
    });
    this.RawMaterailsService.GetRawMaterailsList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.RawMaterailsList = responce.data;
        }
      }
    })
    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.DefaultTax = responce.data;
        }
      }
    });
    // this.DefaultTax = [
    //   { 'DefaultTaxId': 1, 'Tax': 'GST(7%)' },
    //   { 'DefaultTaxId': 2, 'Tax': 'UST(8%)' }]
    this.VarinatOptions = [
      { 'VarinatOptionId': 0, 'VarinatOptionLabel': 'Size' },
      { 'VarinatOptionId': 1, 'VarinatOptionLabel': 'Color' }]
  }
  public GetProductList() {
    const self = this;
    self.Productlaod = true;
    self.ProductService.GetProductList()
      .pipe(finalize(() => { self.Productlaod = false }))
      .subscribe((responce: any) => {
        if (responce != null && responce.status) {
          if (responce.data != null && responce.data.length > 0)
            this.ProductNoDataFound = false;
          else
            this.ProductNoDataFound = true;
          self.ProductList = responce.data;
        }
      });
  }
  public OnLoad() {
    this.ProductForm = this.FormBuilder.group({
      ProductId: [0],
      Name: ['', Validators.required],
      Sku: [''],
      CategorieId: [''],
      BrandId: [''],
      BatchItem: [false],
      Stockitem: [false],
      Taxable: [false],
      SerialNumber: [false],
      IsRawMaterail: [false],
      RawMaterial_points: this.FormBuilder.array([this.FormBuilder.group({
        RawMaterialId: '',
        UMO: '1',
        QTY: ''
      })]),
      IsProductVariants: [false],
    });
    this.ProductVariantForm = this.FormBuilder.group({
      ProductId: ['0'],
      ProductVariantId: ['0'],
      VariantOptionsType: ['0'],
      VariantOptions: ['', Validators.required],
      VariantOptionLabel: [''],
      VariantOptionLabelsList: [''],
      VariantOptionValue_points: this.FormBuilder.array([this.FormBuilder.group({
        ProductId: ['0'],
        ProductVariantId: ['0'],
        VariantOptionsType: [''],
        VariantOptionLabel: [''],
        Variants: [''],
        VariantslabelId: [''],
        Variantslabel: [''],
        Sku: [''],
        ReorderQuantity: [''],
        PurchasePrice: [''],
        SellingPrice: [''],
        Action: [true],
        Image: [''],
        VariMinmOrderQuantity: [''],
        VariantDesc: [''],
        DefaultSupplierId: [''],
        DefaultTaxId: [''],
        DefaultWarehouseId: [''],
        UnitsOfMeasurement: [''],
        InitialStockHand: [''],
        InitialStockPrice: [''],
        InitialHandCost: [''],
      })]),

    });
  }
  public AddRawMaterial() {
    this.RawMaterialPoints.push(this.FormBuilder.group({ RawMaterialId: '', UMO: '', QTY: '' }));
  }

  public DeleteRawMaterial(index) {
    this.RawMaterialPoints.removeAt(index);
  }
  get RawMaterialPoints() {
    return this.ProductForm.get('RawMaterial_points') as FormArray;
  }
  public DeleteVariantOptionValueVariantLabel(index) {
    this.VariantOptionValue.removeAt(index);
    this.VariantoptionValueLabellist.splice(index, 1);
  }
  get VariantOptionValue() {
    return this.ProductVariantForm.get('VariantOptionValue_points') as FormArray;
  }
  get VariantOptionValueMoreItem() {
    return this.ProductVariantForm.controls.MoreItem.get('MoreItem') as FormArray;
  }
  public AddProductVariant() {
    this.VariantOptionValue.push(this.FormBuilder.group({
      ProductId: [0],
      ProductVariantId: [0],
      VariantOptionsType: [''],
      VariantOptionLabel: [''],
      Variants: ['', Validators.required],
      VariantslabelId: [''],
      Variantslabel: [''],
      Sku: [''],
      ReorderQuantity: [''],
      PurchasePrice: [''],
      SellingPrice: [''],
      Action: [true],
      Image: [''],
      VariMinmOrderQuantity: [''],
      VariantDesc: [''],
      DefaultSupplierId: [''],
      DefaultTaxId: [''],
      DefaultWarehouseId: [''],
      UnitsOfMeasurement: [''],
      InitialStockHand: [''],
      InitialStockPrice: [''],
      InitialHandCost: [''],
    }));
  }

  public ChangeVariantOptionValue(Text: string) {
    if (Text != null && Text != undefined && Text != '') {
      let obj = { label: Text, id: this.VariantoptionValueLabellist.length + 1, VariantType: this.VariantOptionType }
      let Id = this.VariantoptionValueLabellist.length + 1;
      this.VariantoptionValueLabellist.push(obj);
      this.ProductVariantForm.patchValue({
        VariantOptionLabel: '',
      })
      let arrobj = this.ProductVariantForm.get('VariantOptionValue_points') as FormArray;
      if (Id > 1) {
        this.AddProductVariant();
      }
      arrobj.controls[Id - 1].patchValue({
        Variants: Id,
        Action: false,
      });

    } else {
      Swal.fire({
        type: 'error',
        title: "Please Enter Variant Option Values..",
      })
    }
  }

  public ChangeBatchORSerialNo(Id: any, Status: boolean) {
    if (Id == 'BatchItem') {
      if (this.ProductForm.controls.SerialNumber.value) {
        this.ProductForm.patchValue({
          SerialNumber: false,
        });
        this.IsBatchOrSerialSelect = false;
      }
    } else {
      if (Status) {
        this.ProductForm.patchValue({
          BatchItem: false,
          IsRawMaterail: false,
        });
        this.IsBatchOrSerialSelect = true;
        this.IsRawMaterailSelect = false;
      } else {
        this.IsBatchOrSerialSelect = false;
      }

    }
  }

  public HasRawMaterial(Status: boolean) {
    if (Status) {
      this.IsRawMaterailSelect = true;
    } else { this.IsRawMaterailSelect = false; }
  }
  public HasProductVariants(Status: boolean) {

    if (Status) {
      this.isProductVarintsSelect = true;
      $('#ProductVariant-link').parent().show();

    } else {
      this.isProductVarintsSelect = false;
      $('#ProductVariant-link').parent().hide();
    }
  }
  public VariantOptionChange(value: boolean) {
    if (value)
      this.VariantOptionType = false;
    else
      this.VariantOptionType = true;
  }
  public MoreInfoVariant(pointIndex: number) {

    if (this.ProductForm.value.Name != null && this.ProductForm.value.Name != undefined && this.ProductForm.value.Name != "") {
      this.ProductIsVariantLoaded = true;
      this.VariantOptionValue.value.forEach(element => {
        element.Action = false;
      });
      let arrobj = this.ProductVariantForm.get('VariantOptionValue_points') as FormArray;
      arrobj.controls[pointIndex].patchValue({
        Action: true,
      });
    } else {
      Swal.fire(
        'Warning',
        'Please Add Product Name',
        'warning'
      );
    }
  }
  handleFileSelect(evt, index) {
    var files = evt.target.files;
    var file = files[0];
    if (files && file) {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        // this.Base64File = reader.result;
        let arrobj = this.ProductVariantForm.get('VariantOptionValue_points') as FormArray;
        arrobj.controls[index].patchValue({
          Image: reader.result,
        });
      };
    }
  }

  public VariantOptionSelectImage(selectedimages) {

  }
  public AddProduct(ProductForm: any, ProductVariantForm: any) {
    const self = this;
    //self.saving = true;

    self.FormSubmitted = true;
    if (ProductForm.invalid) {
      self.saving = false;
      return;
    }
    if (!ProductForm.value.IsRawMaterail)
      ProductForm.value.RawMaterial_points = null;
    else
      ProductForm.value.RawMaterial_points = JSON.stringify(this.RawMaterialPoints.value[0]);
    ProductVariantForm.value.VariantOptionLabelsList = this.VariantoptionValueLabellist;
    if (!ProductForm.value.IsProductVariants)
      ProductVariantForm.value = null;
    else {
      if (ProductVariantForm.value != null && ProductVariantForm.value != undefined) {
        if (ProductVariantForm.value.VariantOptionValue_points != null &&
          ProductVariantForm.value.VariantOptionValue_points.length > 0) {
          ProductVariantForm.value.VariantOptionValue_points.forEach(element => {
            element.DefaultSupplierId = Number(element.DefaultSupplierId);
            element.DefaultTaxId = Number(element.DefaultTaxId);
            element.DefaultWarehouseId = Number(element.DefaultWarehouseId);
            if (this.VariantoptionValueLabellist != null && this.VariantoptionValueLabellist.length > 0) {
              this.VariantoptionValueLabellist.forEach(label => {
                if (element.Variants == label.id) {
                  element.VariantslabelId = label.id;
                  element.Variantslabel = label.label;
                }
              });
            }
          });
        }
      }
      this.ProductVarinat =
      {
        'ProductId': Number(ProductVariantForm.value.ProductId),
        'VariantOptionsType': ProductVariantForm.value.VariantOptionsType,
        'VariantOptionLabel': ProductVariantForm.value.VariantOptions,
        'ProductVariantListVMs': ProductVariantForm.value.VariantOptionValue_points
      };
    }
    const FinalSave = {
      ProductData: ProductForm.value,
      ProductVarinatData: this.ProductVarinat
    };
    this.ProductService.SaveProduct(FinalSave)
      .pipe(finalize(() => { this.saving = false }))
      .subscribe((responce: any) => {
        this.closeModel();
        Swal.fire('Success', responce.message, 'success');
        this.GetProductList();
      }, (error) => {
        Swal.fire(
          'Error',
          error.message,
          'error'
        );
      });
  }

  public closeModel() {
    $('#Product-link').removeClass("active");
    $('#ProductVariant').removeClass("active");
    $('#ProductVariant-link').removeClass("active");
    $('#Product-link').parent().removeClass("active");
    $('#ProductVariant').parent().removeClass("active");
    $('#Product').addClass("active");
    $('#Product-link').addClass("active");
    this.VariantoptionValueLabellist = [];
    this.largeModal.hide();
    this.OnLoad();
    this.IsRawMaterailSelect = false;
    this.ProductIsVariantLoaded = false;
    this.FormSubmitted = false;
  }
  public EditProduct(ProductId: number) {
    if (ProductId != 0) {
      const self = this;
      self.ProductService.GetProduct(ProductId)
        .subscribe((responce: any) => {
          if (responce != null && responce.status) {
            self.BindProductData(responce.data);
            self.ModelTitleString = "Edit Product";
          }
        })
    }
  }
  public BindProductData(data: any) {
    const self = this;

    let productData = data.productData;
    let productVarinatData = data.productVarinatData;
    let RawMaterial = JSON.parse(productData.rawMaterial_points);
    self.ProductForm.patchValue({
      ProductId: productData.productId,
      Name: productData.name,
      Sku: productData.sku,
      CategorieId: productData.categorieId,
      BrandId: productData.brandId,
      BatchItem: productData.batchItem,
      Stockitem: productData.stockitem,
      Taxable: productData.taxable,
      SerialNumber: productData.serialNumber,
      IsRawMaterail: productData.isRawMaterail,

      IsProductVariants: productData.isProductVariants,
    });
    if (productData.isRawMaterail) {
      let arrobj = this.ProductForm.get('RawMaterial_points') as FormArray;
      arrobj.controls[0].patchValue({
        RawMaterialId: RawMaterial.RawMaterialId,
        UMO: RawMaterial.UMO,
        QTY: RawMaterial.QTY
      });
      self.IsRawMaterailSelect = true;
    }
    else
      self.IsRawMaterailSelect = false;

    self.largeModal.show();
    if (productData.isProductVariants) {
      this.isProductVarintsSelect = true;
      $('#ProductVariant-link').parent().show();
      this.ProductVariantForm.patchValue({
        ProductId: productVarinatData.productId,
        VariantOptionsType: productVarinatData.variantOptionsType,
        VariantOptions: productVarinatData.variantOptionLabel,
        VariantOptionLabelsList: [''],
      });

      if (productVarinatData.productVariantListVMs != null && productVarinatData.productVariantListVMs.length > 0) {
        productVarinatData.productVariantListVMs.forEach(element => {
          let obj = { label: element.variantslabel, id: element.variantslabelId, VariantType: this.VariantOptionType }
          this.VariantoptionValueLabellist.push(obj);
          let Id = this.VariantoptionValueLabellist.length;
          if (Id == 1)
            this.ProductVariantForm.patchValue({
              VariantOptionsType: element.variantOptionsType,
              VariantOptions: element.variantOptionslabel,
            })
          let arrobj = this.ProductVariantForm.get('VariantOptionValue_points') as FormArray;
          if (Id > 1) {
            this.AddProductVariant();
          }
          arrobj.controls[Id - 1].patchValue({
            ProductId: element.productId,
            ProductVariantId: element.productVariantId,
            VariantOptionsType: element.variantOptionsType,
            VariantOptionLabel: element.variantOptionslabel,
            Variants: element.variantslabelId,
            VariantslabelId: element.variantslabelId,
            Variantslabel: element.variantslabel,
            Sku: element.sku,
            ReorderQuantity: element.reorderQuantity,
            PurchasePrice: element.purchasePrice,
            SellingPrice: element.sellingPrice,
            Action: false,
            Image: element.image,
            VariMinmOrderQuantity: element.variMinmOrderQuantity,
            VariantDesc: element.variantDesc,
            DefaultSupplierId: element.defaultSupplierId?element.defaultSupplierId:'',
            DefaultTaxId: element.defaultTaxId?element.defaultTaxId:'',
            DefaultWarehouseId: element.defaultWarehouseId? element.defaultWarehouseId:'',
            UnitsOfMeasurement: element.unitsOfMeasurement,
            InitialStockHand: element.initialStockHand,
            InitialStockPrice: element.initialStockPrice,
            InitialHandCost: element.initialHandCost,
          });

        });
      }
    } else {
      $('#ProductVariant-link').parent().hide();
    }
  }
  public DeleteProduct(ProductId: number) {
    if (ProductId != 0) {
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
          self.ProductService.DeleteProduct(ProductId)
            .subscribe((responce: any) => {
              if (responce.status) {
                Swal.fire(
                  responce.message,
                  '',
                  'success'
                )
                this.GetProductList();
              }
            })
        }
      })
    }
  }
  get f() { return this.ProductForm.controls; }
}
