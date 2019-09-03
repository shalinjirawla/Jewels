import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { BrandModel } from '../../../Models/ProductModels/ProductsModel';
import { ProductBrandService } from '../../../Services/Products-Services/product-brand.service';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})
@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private FormBuilder: FormBuilder, private ProductBrandService: ProductBrandService) { }

  //Title For Model Start..
  ModelTitleString: string = "Add New Product Brand";
  //Title For Model End...

  //Form List for Categories Start..
  BrandForm: FormGroup;
  //Form List for Categories End//
  BrandListEmplty: boolean = true;
  BrandList: BrandModel[] = [];
  FormSubmitted: boolean = false;
  responce: any;
  ngOnInit() {
    this.OnLoad();
    this.GetProductBranbdList();
  }
  public OnLoad() {
    this.BrandForm = this.FormBuilder.group({
      BrandId: [0],
      BrandName: ['', Validators.required],
      Description: [''],
    });
    this.FormSubmitted = false;
  }

  public GetProductBranbdList()
  {
    this.ProductBrandService.GetProductBrandList().subscribe((responce:any)=>{
      if(responce.status){
      this.BrandList=responce.data
      debugger
      this.BrandListEmplty=false;
      }
    });
    this.BrandListEmplty=true;
  }
  public AddBrand(BrandForm: FormControl) {
    this.FormSubmitted = true;
    if (this.BrandForm.invalid) {
      return;
    }
    this.ProductBrandService.SaveBrand(this.BrandForm.value).subscribe((responce: any) => {
      if (responce.status) {
        this.GetProductBranbdList();
        this.ResetForm();
        this.largeModal.hide();
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
  public ResetForm() {
    this.largeModal.hide();
    this.OnLoad();
  }
  public BrandEdit(brandId: any) {
    if (brandId != 0) {
      this.ProductBrandService.GetProductBrand(brandId).subscribe((responce: any) => {
        debugger
        if (responce.status && responce.data != null) {
          let Data = responce.data;
          this.largeModal.show();
          this.BrandForm.patchValue({
            BrandId: Data.brandId,
            BrandName: Data.brandName,
            Description: Data.description,
          });
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      });
    }
  }
  public BrandDelete(brandId: any) {
    if (brandId != 0) {
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
          this.ProductBrandService.DeleteProductBrand(brandId).subscribe((responce: any) => {
            this.GetProductBranbdList();
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
  get f() { return this.BrandForm.controls; }
}
