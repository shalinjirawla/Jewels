import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators,FormArray } from '@angular/forms';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import {HasProductRawMaterial,ProductModel} from '../../../Models/ProductModels/ProductsModel';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private FormBuilder: FormBuilder,
    private ProductBrandService: ProductBrandService,
    private ProductCategoriesService: ProductCategoriesService
  ) { }
  
  //Title For Model Start..
  ModelTitleString: string = "Add New Product";
  //Title For Model End...
  //Form List for Product Start..
  ProductForm: FormGroup;
  ProductVariantForm:FormGroup;
  CategoriesList: any[];
  BrandList: any[];
  IsBatchOrSerialSelect:boolean=false;
  IsRawMaterailSelect:boolean=false;
  isProductVarintsSelect:boolean=false;
  //Form List for Product End//
  games: any
  key: string = 'name';
  reverse: boolean = false;
  sort(key) {
    this.key = key;
    this.reverse = !this.reverse;
  }
  p: number = 1;
  ngOnInit() {
    this.OnLoad();
    this.GetDropDownList();
   
  }
  public ShowModel()
  {
    $('#ProductVariant-link').parent().hide();
    this.largeModal.show();
  }
  public GetDropDownList() {
    this.ProductCategoriesService.GetProductCategoriesList().subscribe((responce: any) => {
      if (responce.status) {
        this.CategoriesList = responce.data;
      }
    });
    this.ProductBrandService.GetProductBrandList().subscribe((responce:any)=>{
      if(responce.status)
      {
        this.BrandList=responce.data;
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
      BatchItem:[false],
      Stockitem:[false],
      Taxable:[false],
      SerialNumber:[false],
      IsRawMaterail:[false],
      RawMaterial_points: this.FormBuilder.array([this.FormBuilder.group({
        RawMaterialId:'',
        UMO:'',
        QTY:''
      })]),
      IsProductVariants:[''],
    });
    this.ProductVariantForm=this.FormBuilder.group({
        VariantOption:['',Validators.required],
        VariantOptionValue:this.FormBuilder.array([this.FormBuilder.group({
          label:'',
        })]),
    });
  }
 public AddRawMaterial() {
    this.RawMaterialPoints.push(this.FormBuilder.group({RawMaterialId:'',UMO:'',QTY:''}));
  }

 public DeleteRawMaterial(index) {
    this.RawMaterialPoints.removeAt(index);
  }
  get RawMaterialPoints() {
    return this.ProductForm.get('RawMaterial_points') as FormArray;
  }
  get ProductVariant()
  {
    return this.ProductVariantForm.get('VariantOptionValue') as FormArray;
  }
  public AddProductVariant() {
   // this.VariantOptionValue.push(this.FormBuilder.group({label:'',}));
  }

 public DeleteProductVariant(index) {
    //this.VariantOptionValue.removeAt(index);
  }
  public ChangeBatchORSerialNo(Id:any,Status:boolean)
  {
    if(Id=='BatchItem')
    {
        if(this.ProductForm.controls.SerialNumber.value){
          this.ProductForm.patchValue({
            SerialNumber:false,
          });
          this.IsBatchOrSerialSelect=false;
        }
    }else {
      if(Status){
        this.ProductForm.patchValue({
          BatchItem:false,
          IsRawMaterail:false,
        });
        this.IsBatchOrSerialSelect=true;
        this.IsRawMaterailSelect=false;
      }else{
        this.IsBatchOrSerialSelect=false;
      }
        
    }
  }
  public HasRawMaterial(Status:boolean){
    if(Status)
    {
      this.IsRawMaterailSelect=true;
    }else{this.IsRawMaterailSelect=false;}
  }
  public HasProductVariants(Status:boolean){
    if(Status){
      this.isProductVarintsSelect=true;
      $('#ProductVariant-link').parent().show();
      debugger
    }else{this.isProductVarintsSelect=false;
       $('#ProductVariant-link').parent().hide();}
  }
}
