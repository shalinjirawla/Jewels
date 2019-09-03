import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
//import * as $ from 'jquery';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';

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
  ) { }
  RawMaterialList: boolean = true;
  ModelTitleString: string = "Add New Product Raw Material";
  RawMaterialForm: FormGroup;
  FormSubmitted: boolean = false;
  ProductCategorieList: any[];
  ProductBrandList: any[];
  ngOnInit() {
    this.OnLoad();
  }
  public OnLoad() {
    this.RawMaterialList = false;
    this.RawMaterialForm = this.FormBuilder.group({
      RMId: [0],
      NameOrItem: ['', Validators.required],
      ItemCode: [''],
      CategorieId: [''],
      CategorieName: [''],
      BrandId: [''],
      BrandName: [''],
      StockItem:[''],
      Taxable:[''],
      BarCode:[''],
      Description: ['']
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

  }
  public AddRawMaterial(RawMaterialForm: FormControl) {
    this.FormSubmitted = true;
    if (RawMaterialForm.invalid) {
      return;
    }

  }

  public ResetForm() {
    this.FormSubmitted = false;
    this.OnLoad();
    this.largeModal.hide();
  }
  get f() { return this.RawMaterialForm.controls; }
}
