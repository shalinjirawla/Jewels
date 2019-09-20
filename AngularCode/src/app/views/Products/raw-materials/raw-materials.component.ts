import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import { RawMaterailsService } from '../../../Services/RawMaterails-Services/raw-materails.service'
import { TaxCodeService } from '../../../Services/Masters-Services/general-setup.service'
import { SupplierServicesService } from '../../../Services/SuppliersServices/supplier-services.service'
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import Swal from 'sweetalert2'

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
  ModelTitleString: string = "Add Raw Material";
  RawMaterialForm: FormGroup;
  RawMaterialForm2: FormGroup;
  UnitofMeasure: FormGroup;
  UploadPhotoForm: FormGroup;
  FormSubmitted: boolean = false;
  FormSubmitted2: boolean = false;
  FormSubmitted3: boolean = false;
  RawMaterailsempty: boolean = true;
  ProductCategorieList: any[];
  ProductBrandList: any[];
  WarehouseList: any[];
  TaxCodeList: any[];
  SupplierList: any[];
  FtMetricUnitList: any[];
  KgMetricUnitList: any[];
  RawMaterailsList: any[];
  UOMList: any[];
  PictureList: any = { "picture": [] };
  PictureLenghtcount: boolean = false;
  base64textString: any;
  Base64File: any;
  ouMatric: string = "";
  inMatric: string = "";

  key: string = 'Companyname'; //set default
  reverse: boolean = false;
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

  }
  openmodal() {
    this.OnLoad();
    this.FormSubmitted = false;
    this.FormSubmitted2 = false;
    this.FormSubmitted3 = false;
    this.largeModal.show();
    document.getElementById("RawMaterialFormtab-link").click();
  }
  public OnLoad() {
    this.GetRawMaterailsList();
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
      rawMaterailId: [0],
      productId: [0],
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
      let pictureList = {};
      return obj3;
    }

    final["pictureList"] = this.PictureList;
    
    if (final["rmId"] == 0) {
      this.RawMaterailsService.SaveRawMaterails(final).subscribe((responce: any) => {
        if (responce.status) {
          Swal.fire(
            "",
            responce.message,
            'success'
          )
          this.ResetForm();
        }
        else {
          Swal.fire(
            "",
            responce.message,
            'success'
          )
        }
      })
    }
    else {
      this.RawMaterailsService.UpdateRawMaterails(final["rmId"], final).subscribe((responce: any) => {
        if (responce.status) {
          Swal.fire(
            responce.message,
            "",
            'success'
          )
          this.ResetForm();
        }
        else {
          Swal.fire(
            responce.message,
            "",
            'success'
          )
        }
      })
    }


  }

  DeleteRawMaterails(Id: any) {
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
          this.RawMaterailsService.DeleteRawMaterails(Id).subscribe((responce: any) => {
            if (responce.status) {
              Swal.fire(
                responce.message,
                '',
                'success'
              )
              this.OnLoad();
            }
          });
        }
      })
    }
  }

  GetRawMaterailsList() {
    this.RawMaterailsService.GetRawMaterailsList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.RawMaterailsList = responce.data;
        }
        if (this.RawMaterailsList.length != 0) {
          this.RawMaterailsempty = false;
        }
      }
    })
  }

  GetRawMaterail(Id: any) {
    this.RawMaterailsService.GetRawMaterails(Id).subscribe((responce: any) => {
      if (responce.status) {
        let result = responce.data;
        if (result != null) {

          this.RawMaterialForm.patchValue({
            rmId: result.rmId,
            rmName: result.rmName,
            itemcode: result.itemcode,
            alternativeRMName: result.alternativeRMName,
            productCategorieId: result.productCategorieId != null ? result.productCategorieId : 0,
            brandId: result.brandId != null ? result.brandId : 0,
            warehouseId: result.warehouseId != null ? result.warehouseId : 0,
            taxCodeId: result.taxCodeId != null ? result.taxCodeId : 0,
            supplierId: result.supplierId != null ? result.supplierId : 0,
            purchase_Price: result.purchase_Price,
            selling_Price: result.selling_Price,
          })

          this.RawMaterialForm2.patchValue({
            iStockOnHand: result.iStockOnHand,
            iCostPrice: result.iCostPrice,
            iLandedCost: result.iLandedCost,
            reorder_Quantity: result.reorder_Quantity,
            minimumu_Order_Quantity: result.minimumu_Order_Quantity,
            description: result.description,
            stockItem: result.stockItem,
            taxable: result.taxable,
          })

          this.UnitofMeasure.patchValue({
            uomId: result.uomId != null ? result.uomId : 0,
            outer_Weight: result.outer_Weight,
            inner_Weight: result.inner_Weight,
            outer_Weight_metric_Units: result.outer_Weight_metric_Units != null ? result.outer_Weight_metric_Units : 0,
            inner_Weight_metric_Units: result.inner_Weight_metric_Units != null ? result.inner_Weight_metric_Units : 0,
            oD_Width: result.oD_Width,
            oD_Height: result.oD_Height,
            oD_length: result.oD_length,
            oD_metric_Units: result.oD_metric_Units != null ? result.oD_metric_Units : 0,
            oD_CBM: result.oD_CBM == "0.00" ? "" : result.oD_CBM,
            iD_Width: result.iD_Width,
            iD_Height: result.iD_Height,
            iD_length: result.iD_length,
            iD_metric_Units: result.iD_metric_Units != null ? result.iD_metric_Units : 0,
            iD_CBM: result.iD_CBM == "0.00" ? "" : result.iD_CBM,
          })
          this.PictureList = result.pictureList;

          this.largeModal.show();
          document.getElementById("RawMaterialFormtab-link").click();
        }
      }
    })
  }

  AddPicture(base64textString) {
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

      this.UploadPhotoForm = this.FormBuilder.group({
        pictuterId: [0],
        pictuterString: [""],
        defaultPicture: false,
        rawMaterailId: [0],
        productId: [0],
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
          this.UploadPhotoForm = this.FormBuilder.group({
            pictuterId: [0],
            pictuterString: [""],
            defaultPicture: false,
            rawMaterailId: [0],
            productId: [0],
          })
          this.PictureList.picture = [];
        }
      }
    })
  }

  OuterCBM(UnitofMeasure) {
    let flag = true;
    this.outerUOM(UnitofMeasure.value.oD_metric_Units, flag)
    let ouWidth = UnitofMeasure.value.oD_Width != "" ? parseFloat(UnitofMeasure.value.oD_Width) : 0;
    let ouHeight = UnitofMeasure.value.oD_Height != "" ? parseFloat(UnitofMeasure.value.oD_Height) : 0;
    let ouLength = UnitofMeasure.value.oD_length != "" ? parseFloat(UnitofMeasure.value.oD_length) : 0;
    if (ouWidth != 0 && ouHeight != 0 && ouLength != 0 && this.ouMatric != "") {
      let matric = this.ouMatric;
      let oucbm = "";
      if (matric == "cm") {
        oucbm = (ouWidth * ouHeight * ouLength / 1000000).toString();
      }
      else if (matric == "mm") {
        oucbm = (ouWidth * ouHeight * ouLength / 1000000000).toString();
      }
      else if (matric == "m") {
        oucbm = (ouWidth * ouHeight * ouLength).toString();
      }
      else if (matric == "in") {
        oucbm = (ouWidth * ouHeight * ouLength / 31023.37).toString();
      }
      else if (matric == "ft") {
        oucbm = (ouWidth * ouHeight * ouLength / 35.315).toString();
      }
      this.UnitofMeasure.patchValue({
        oD_CBM: oucbm,
      })
    }
    else {
      this.UnitofMeasure.patchValue({
        oD_CBM: "",
      })
    }
  }

  outerUOM(event, flag) {
    if (event != 0) {
      this.FtMetricUnitList.map((result: any, index) => {
        if (result.metric_UnitsId == event) {
          this.ouMatric = result.metric_UnitsName;
          if (!flag) {
            this.OuterCBM(this.UnitofMeasure);
          }
        }
      })
    }
    else {
      this.ouMatric = "";
      if (!flag) {
        this.OuterCBM(this.UnitofMeasure);
      }
    }
  }

  InnerCBM(UnitofMeasure) {
    let flag = true;
    this.InnerUOM(UnitofMeasure.value.iD_metric_Units, flag)
    let inWidth = UnitofMeasure.value.iD_Width != "" ? parseFloat(UnitofMeasure.value.iD_Width) : 0;
    let inHeight = UnitofMeasure.value.iD_Height != "" ? parseFloat(UnitofMeasure.value.iD_Height) : 0;
    let inLength = UnitofMeasure.value.iD_length != "" ? parseFloat(UnitofMeasure.value.iD_length) : 0;
    if (inWidth != 0 && inHeight != 0 && inLength != 0 && this.inMatric != "") {
      let matric = this.inMatric;
      let incbm = "";
      if (matric == "cm") {
        incbm = (inWidth * inHeight * inLength / 1000000).toString();
      }
      else if (matric == "mm") {
        incbm = (inWidth * inHeight * inLength / 1000000000).toString();
      }
      else if (matric == "m") {
        incbm = (inWidth * inHeight * inLength).toString();
      }
      else if (matric == "in") {
        incbm = (inWidth * inHeight * inLength / 31023.37).toString();
      }
      else if (matric == "ft") {
        incbm = (inWidth * inHeight * inLength / 35.315).toString();
      }
      this.UnitofMeasure.patchValue({
        iD_CBM: incbm,
      })
    }
    else {
      this.UnitofMeasure.patchValue({
        iD_CBM: "",
      })
    }
  }

  InnerUOM(event, flag) {
    if (event != 0) {
      this.FtMetricUnitList.map((result: any, index) => {
        if (result.metric_UnitsId == event) {
          this.inMatric = result.metric_UnitsName;
          if (!flag) {
            this.InnerCBM(this.UnitofMeasure);
          }
        }
      })
    }
    else {
      this.inMatric = "";
      if (!flag) {
        this.InnerCBM(this.UnitofMeasure);
      }
    }
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
