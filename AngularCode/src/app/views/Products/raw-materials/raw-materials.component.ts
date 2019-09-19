import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
//import * as $ from 'jquery';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import { RawMaterailsService } from '../../../Services/RawMaterails-Services/raw-materails.service'
import { TaxCodeService } from '../../../Services/Masters-Services/general-setup.service'
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service'
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

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
    private SupplierServicesService: SupplierServicesService,
  ) { }


  RawMaterialList: boolean = true;
  ModelTitleString: string = "Add New Product Raw Material";
  RawMaterialForm: FormGroup;
  RawMaterialForm2: FormGroup;
  UnitofMeasure: FormGroup;
  UploadPhotoForm: FormGroup;
  FormSubmitted: boolean = false;
  FormSubmitted2: boolean = false;
  FormSubmitted3: boolean = false;
  ProductCategorieList: any[];
  ProductBrandList: any[];
  WarehouseList: any[];
  TaxCodeList: any[];
  SupplierList: any[];
  FtMetricUnitList: any[];
  KgMetricUnitList: any[];
  UOMList: any[];
  PictureList: any = { "picture": [] };
  PictureLenghtcount: boolean = false;
  base64textString: any;
  Base64File: any;


  ngOnInit() {
    this.OnLoad();
  }
  openmodal() {
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
      alternativeRMName: [''],
      productCategorieId: [0],
      brandId: [0],
      warehouseId: [0],
      taxCodeId: [0],
      supplierId: [0],
      purchase_Price: [""],
      selling_Price: [""],

    });
    this.RawMaterialForm2 = this.FormBuilder.group({
      iStockOnHand: [""],
      iCostPrice: [""],
      iLandedCost: [""],
      reorder_Quantity: [""],
      minimumu_Order_Quantity: [""],
      description: [''],
      stockItem: [false],
      taxable: [false],
    });
    this.UnitofMeasure = this.FormBuilder.group({
      uomId: [0],
      outer_Weight: [""],
      inner_Weight: [""],
      outer_Weight_metric_Units: [0],
      inner_Weight_metric_Units: [0],
      oD_Width: [""],
      oD_Height: [""],
      oD_length: [""],
      oD_metric_Units: [0],
      oD_CBM: [""],
      iD_Width: [""],
      iD_Height: [""],
      iD_length: [""],
      iD_metric_Units: [0],
      iD_CBM: [""],
    })
    this.UploadPhotoForm = this.FormBuilder.group({
      pictuterId: [0],
      pictuterString: [""],
      defaultPicture: false,
      rawMaterailId:[0],
      productId:[0],
    })

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
        this.UOMList = responce.data;
      }
    })

    this.RawMaterailsService.GetFtMetricUnitList().subscribe((responce: any) => {
      if (responce.status) {
        this.FtMetricUnitList = responce.data;
      }
    })
    this.RawMaterailsService.GetKgMetricUnitList().subscribe((responce: any) => {
      if (responce.status) {
        this.KgMetricUnitList = responce.data;
      }
    })

    this.SupplierServicesService.GetSuppliersList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null && responce.data != undefined && responce.data.length > 0) {
          this.SupplierList = responce.data;
        }
      }
    });

    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        this.TaxCodeList = responce.data;
      }
    })

  }
  public AddRawMaterial(RawMaterialForm: FormControl, RawMaterialForm2: FormControl, UnitofMeasure: FormControl) {
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
    else if (document.getElementById("RawMaterialForm2tab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm3tab-link").click();
      return;
    }
    this.FormSubmitted2 = true;
    if (RawMaterialForm2.invalid) {
      document.getElementById("RawMaterialForm2tab-link").click();
      return;
    }
    this.FormSubmitted2 = false;
    if (document.getElementById("RawMaterialFormtab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm2tab-link").click();
      return;
    }
    else if (document.getElementById("RawMaterialForm2tab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm3tab-link").click();
      return;
    }
    this.FormSubmitted3 = true;
    if (UnitofMeasure.invalid) {
      return;
    }
    if (document.getElementById("RawMaterialFormtab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm2tab-link").click();
      return;
    }
    else if (document.getElementById("RawMaterialForm2tab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm3tab-link").click();
      return;
    }
    else if (document.getElementById("RawMaterialForm3tab-link").className == "nav-link active") {
      document.getElementById("RawMaterialForm4tab-link").click();
      return;
    }
    this.FormSubmitted3 = false;
    let RawMaterialFormData = RawMaterialForm.value;
    let final = merge_options(RawMaterialForm.value, RawMaterialForm2.value, UnitofMeasure.value);
    function merge_options(obj1, obj2, obj4) {
      let obj3 = {};
      for (var attrname in obj1) { obj3[attrname] = obj1[attrname]; }
      for (var attrname in obj2) { obj3[attrname] = obj2[attrname]; }
      for (var attrname in obj4) { obj3[attrname] = obj4[attrname]; }
      return obj3;
    }
    final.pictureList = this.PictureList;
    debugger
    this.RawMaterailsService.SaveRawMaterails(final).subscribe((responce: any) => {
      if (responce.status) {
        this.FtMetricUnitList = responce.data;
      }
    })


  }

  AddPicture(base64textString) {
    debugger
    if (base64textString != null && base64textString != "") {
      if (this.UploadPhotoForm.value.pictuterId == 0) {
        if (this.PictureList.picture.length == 0) {
          this.UploadPhotoForm.value.defaultPicture = true;
        }
        this.UploadPhotoForm.value.pictuterString = base64textString;
        this.UploadPhotoForm.value.pictuterId = Math.random().toString(36).substr(2, 9);
        this.PictureList.picture.push(this.UploadPhotoForm.value)
      }

      if (this.PictureList.picture.length >= 0) {
        this.PictureLenghtcount = false;
      }

      this.UploadPhotoForm.patchValue({
        pictuterId: [0],
        pictuterString: [""],
        defaultPicture: false,
      })

    }

  }

  SetDeafultPicture(pictuterId: any) {
    this.PictureList.picture.map((result: any, index) => {
      if (result.defaultPicture) {
        this.PictureList.picture[index].defaultPicture = false;
      }
      if (pictuterId == result.pictuterId) {
        this.PictureList.picture[index].defaultPicture = true;
      }
    })
  }

  DeletePicture(pictuterId: any) {
    let a = this.PictureList.picture.map((result: any, index) => {
      if (pictuterId == result.pictuterId) {
        var elementPos = this.PictureList.picture.map(function (x) { return x.pictuterId; }).indexOf(result.pictuterId);
        this.PictureList.picture.splice(elementPos, 1);
        let setDefaultPicture = false;
        let a = this.PictureList.picture.map((result: any, index) => {
          if (result.defaultPicture) {
            setDefaultPicture = true;
          }

        })
        if (!setDefaultPicture) {
          if (this.PictureList.picture.length != 0) {
            this.PictureList.picture[0].defaultPicture = true;
          }
        }
        if (this.PictureList.picture.length == 0) {
          this.FormSubmitted = false;
          this.FormSubmitted2 = false;
          this.OnLoad();
          this.PictureList.picture = [];
        }
      }
    })
  }

  handleFileSelect(evt) {
    var files = evt.target.files;
    var file = files[0];
    if (files && file) {
      var a = this.getBase64(file);
    }
  }

  getBase64(file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.Base64File = reader.result;
      this.AddPicture(this.Base64File);
    };

  }


  public ResetForm() {
    this.FormSubmitted = false;
    this.FormSubmitted2 = false;
    this.OnLoad();
    this.PictureList.picture = [];
    this.largeModal.hide();
  }
  get f() { return this.RawMaterialForm.controls; }

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
