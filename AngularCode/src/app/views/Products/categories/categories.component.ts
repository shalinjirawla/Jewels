import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CategoriesModel } from '../../../Models/ProductModels/ProductsModel';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import Swal from 'sweetalert2'
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})
@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private FormBuilder: FormBuilder, private ProductCategoriesService: ProductCategoriesService) { }

  //Title For Model Start..
  ModelTitleString: string = "Add New Product Categories";
  //Title For Model End...

  //Form List for Categories Start..
  CategoriesForm: FormGroup;
  //Form List for Categories End//

  // Form Submiited btn 
  FormSubmitted: boolean = false;
  //

  Rsponces: any;
  CategoriesList: CategoriesModel[];
  CategoriesListEmplty: boolean = true;
  ngOnInit() {
    this.OnLoad();
  }
  public OnLoad() {
    this.CategoriesForm = this.FormBuilder.group({
      CategoriesId: [0],
      CategoriesName: ['', Validators.required],
      DisplayOrder: [''],
      Code: [''],
      Description: [''],
    });
    this.FormSubmitted=false;
    this.GetCategoriesList();
  }
  public GetCategoriesList() {
    this.ProductCategoriesService.GetProductCategoriesList().subscribe((responce: any) => {
      if (responce != null) {
        this.CategoriesListEmplty = true;
        return this.CategoriesList = responce.data;
      }
    });
    this.CategoriesListEmplty = false;
  }
  public AddCategories(CategoriesForm: FormControl) {
    this.FormSubmitted = true;
    if (CategoriesForm.invalid) {
      return;
    }
    this.ProductCategoriesService.SaveProductCategories(CategoriesForm.value).subscribe((responce: any) => {
      if (responce.status) {
        this.GetCategoriesList();
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
  public CategorieEdit(CategorieId: any) {
    if (CategorieId != 0) {
      this.ProductCategoriesService.GetProductCategories(CategorieId).subscribe((responce: any) => {
        debugger
        if (responce.status && responce.data != null) {
          let Data = responce.data;
          this.largeModal.show();
          this.CategoriesForm.patchValue({
            CategoriesId: Data.categoriesId,
            CategoriesName: Data.categoriesName,
            DisplayOrder: Data.displayOrder,
            Code: Data.code,
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
  public CategorieDelete(CategorieId: number) {
    if (CategorieId != 0) {
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
          this.ProductCategoriesService.DeleteCategorie(CategorieId).subscribe((responce: any) => {
            this.GetCategoriesList();
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
  public ResetForm() {
    this.largeModal.hide();
    this.OnLoad();
  }
  get f() { return this.CategoriesForm.controls; }
}
