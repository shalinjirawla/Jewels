import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
import { element } from 'protractor';
import { TenantsServicesService } from '../../../Services/TenantsServices/tenants-services.service';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})
@Component({
  selector: 'app-tenants',
  templateUrl: './tenants.component.html',
  styleUrls: ['./tenants.component.scss']
})
export class TenantsComponent implements OnInit {

  constructor(private FormBuilder: FormBuilder,
    private TenantsServicesService: TenantsServicesService
  ) { }
  TenantsForm: FormGroup;
  FormSubmitted: boolean = false;
  Responce: any;

  ngOnInit() {
    this.Onload();
  }
  public Onload() {
    this.TenantsForm = this.FormBuilder.group({
      TenantId: [0],
      TenantName: ['', Validators.required],
      EmailId: ['', Validators.compose([Validators.required, Validators.email])],
    });
  }
  public SaveTenants(TenantsForm: FormControl) {
    this.FormSubmitted = true;
    if (this.TenantsForm.invalid) {
      return;
    }
    this.TenantsServicesService.SaveTenants(TenantsForm.value).subscribe((responce: any) => {
      if (responce != null && responce.status) {
        this.Responce = responce;
        Swal.fire({
          type: 'success',
          title: responce.message,
          timer: 2000
        })
      }else{
        Swal.fire({
          type: 'error',
          title: responce.message,
          timer: 2000
        })
      }
    });
  }
  get f() {
    return this.TenantsForm.controls;
  }
}
