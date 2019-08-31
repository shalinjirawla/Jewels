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
  BrandListEmplty: boolean = false;
  BrandList: BrandModel[] = [];
  FormSubmitted: boolean = false;
  responce: any;
  ngOnInit() {
    this.OnLoad();
  }
  public OnLoad() {
    this.BrandForm = this.FormBuilder.group({
      BrandId: [0],
      BrandName: ['', Validators.required],
      Description: [''],
    });
    this.FormSubmitted = false;
  }

  public AddBrand(BrandForm: FormControl) {
    this.FormSubmitted = true;
    if (this.BrandForm.invalid) {
      return;
    }
    this.ProductBrandService.SaveBrand(this.BrandForm.value).subscribe((responce: any) => {
      return this.responce = responce;
    });
  }
  public ResetForm() {
    this.largeModal.hide();
    this.OnLoad();
  }
  public BrandEdit(brandId: any) {

  }
  public BrandDelete(brandId: any) {

  }
  get f() { return this.BrandForm.controls; }
}
