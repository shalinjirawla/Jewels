import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
//import * as $ from 'jquery';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import { RawMaterailsService } from '../../../Services/RawMaterails-Services/raw-materails.service'
import { TaxCodeService } from '../../../Services/Masters-Services/general-setup.service'

@Component({
  selector: 'app-raw-materials',
  templateUrl: './raw-materials.component.html',
  styleUrls: ['./raw-materials.component.scss',
  ]
})

export class RawMaterialsComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private FormBuilder: FormBuilder,
    private ProductBrandService: ProductBrandService,
    private ProductCategoriesService: ProductCategoriesService,
    private RawMaterailsService: RawMaterailsService,
    private TaxCodeService: TaxCodeService,
  ) { }


  RawMaterialList: boolean = true;
  ModelTitleString: string = "Add New Product Raw Material";
  RawMaterialForm: FormGroup;
  RawMaterialForm2: FormGroup;
  FormSubmitted: boolean = false;
  FormSubmitted2: boolean = false;
  ProductCategorieList: any[];
  ProductBrandList: any[];
  WarehouseList: any[];
  TaxCodeList: any[];
  SupplierLsi: any[];
  UOMList:any[];


  ngOnInit() {
    this.OnLoad();
  }
  openmodal(){
    this.OnLoad();
    this.FormSubmitted = false;
    this.FormSubmitted2 = false;
    this.largeModal.show();
  }
  public OnLoad() {
    this.RawMaterialList = false;
    this.RawMaterialForm = this.FormBuilder.group({
      rmId: [0],
      rmName: ['', Validators.required],
      itemcode: [''],
      alternativeRMName:[''],
      productCategorieId: [0],
      brandId: [0],
      warehouseId: [0],
      taxCodeId: [0],
      supplierId: [0],
      purchase_Price: [],
      selling_Price: [],

    });
    this.RawMaterialForm2 = this.FormBuilder.group({
      iStockOnHand: [],
      iCostPrice: [],
      iLandedCost: [],
      reorder_Quantity: [],
      minimumu_Order_Quantity: [],
      description: [''],
      uomId:[],
    });
 
    //get Categories and Brand values..
    this.ProductCategoriesService.GetProductCategoriesList().subscribe((responce: any) => {
      if (responce.status) {
        this.ProductCategorieList = responce.data;
      }
    });

    this.ProductBrandService.GetProductBrandList().subscribe((responce: any) => {
      if (responce.status) {
        this.ProductBrandList = responce.data;
      }
    });

    this.RawMaterailsService.GetLocationList().subscribe((responce: any) => {
      if (responce.status) {
        this.WarehouseList = responce.data;
      }
    })

    this.RawMaterailsService.GetUOMList().subscribe((responce: any) => {
      if (responce.status) {
        debugger
        this.UOMList = responce.data;
      }
    })

    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        this.TaxCodeList = responce.data;
      }
    })

  }
  public AddRawMaterial(RawMaterialForm: FormControl) {
    debugger
    this.FormSubmitted = true;
    if (RawMaterialForm.invalid) {
      document.getElementById("RawMaterialFormtab-link").click();
      return;
    }
    this.FormSubmitted = false;
    if (document.getElementById("RawMaterialFormtab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm2tab-link").click();
      return;
    }
    this.FormSubmitted2=true;
    if (this.RawMaterialForm2.invalid) {
      document.getElementById("RawMaterialForm2tab-link").click();
      return;
    }
    this.FormSubmitted2=false;
  }

  public ResetForm() {
    this.FormSubmitted = false;
    this.FormSubmitted2=false;
    this.OnLoad();
    this.largeModal.hide();
  }
  get f() { return this.RawMaterialForm.controls; }

  ischeckedornot(event) {
    debugger
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
