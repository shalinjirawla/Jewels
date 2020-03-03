import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CategoriesModel } from '../../../Models/ProductModels/ProductsModel';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import Swal from 'sweetalert2'
import { TaxCodeService } from '../../../Services/Masters-Services/general-setup.service';
import { ServicesService } from './../../../Services/Products-Services/services.service';
import { finalize } from 'rxjs/operators';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.scss']
})
export class ServiceComponent implements OnInit {

  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private FormBuilder: FormBuilder,
    private ProductCategoriesService: ProductCategoriesService,
    private TaxCodeService: TaxCodeService,
    private ServicesService: ServicesService, ) { }

  //Title For Model Start..
  ModelTitleString: string = "Add New Service";
  ServiceForm: FormGroup;
  FormSubmitted: boolean = false;
  Rsponces: any;
  ServiceList: CategoriesModel[];
  IsTaxbled: boolean = false;
  TaxList: any[] = [];
  saving: boolean = false;
  ProductServiceslaod: boolean = false;
  ProductServiceNoDataFound: boolean = false;
  ngOnInit() {
    this.OnLoad();
    this.GetProductServiceList();
  }

  public OnLoad() {
    this.ServiceForm = this.FormBuilder.group({
      ServiceId: [0],
      Name: ['', Validators.required],
      SKU: [''],
      Taxble: [''],
      TaxId: [''],
      PurchasePrice: [''],
      SellingPrice: [''],
      MinmOrderQuantity: [''],
      Description: [''],
    });

  }
  public GetProductServiceList() {
    this.TaxCodeService.GetTaxCodeList().subscribe((responce: any) => {
      if (responce.status) {
        if (responce.data != null) {
          this.ProductServiceNoDataFound=true;
          this.TaxList = responce.data;
        }else
        this.ProductServiceNoDataFound=false;
      }
    });
    const self = this;
    self.ProductServiceslaod = true;
    this.ServicesService.GetServiceList()
      .pipe(finalize(() => { self.ProductServiceslaod = false }))
      .subscribe((responce: any) => {
        if (responce != null && responce.status)
          this.ServiceList = responce.data;
      });
  }
  public ShowModel() {
    this.largeModal.show();
  }
  public Close() {
    this.largeModal.hide();
    this.FormSubmitted = false;
  }
  public IsTexaledChange(event: any) {
    if (event)
      this.IsTaxbled = true;
    else
      this.IsTaxbled = false;
  }
  public SaveService(ServiceForm: any) {
    this.FormSubmitted = true;
    if (ServiceForm.invalid) {
      return;
    }
    const self = this;
    self.saving = true;
    self.ServicesService.SaveService(ServiceForm.value)
      .pipe(finalize(() => { self.saving = false }))
      .subscribe((responce: any) => {
        Swal.fire('Success', responce.message, 'success');
        this.Close();
        this.OnLoad();
        this.GetProductServiceList();
      }, (error) => {
        Swal.fire('error', error.message, 'error');
      })
  }
  public GetProductService(serviceId: number) {
    if (serviceId != 0) {
      const self = this;
      self.ServicesService.GetService(serviceId).subscribe((responce: any) => {
        if (responce.status) {
          let service = responce.data;
          self.ServiceForm.patchValue({
            ServiceId: service.serviceId,
            Name: service.name,
            SKU: service.SKU,
            Taxble: service.taxble,
            TaxId: service.taxId,
            PurchasePrice: service.purchasePrice,
            SellingPrice: service.sellingPrice,
            MinmOrderQuantity: service.minmOrderQuantity,
            Description: service.description,
          });
          if (service.taxble)
            self.IsTaxbled = true;
          else
            self.IsTaxbled = false;
          self.ShowModel();
          self.ModelTitleString = "Edit Product Service";
        }
      });
    }
  }
  public DeleteService(ProductServiceId:number){
    if (ProductServiceId != 0) {
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
          self.ServicesService.DeleteService(ProductServiceId)
            .subscribe((responce: any) => {
              if (responce.status) {
                Swal.fire(
                  responce.message,
                  '',
                  'success'
                )
                this.GetProductServiceList();
              }
            })
        }
      })
    }
  }
  get f() { return this.ServiceForm.controls; }
}
