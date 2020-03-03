import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import {Router} from '@angular/router'
import Swal from 'sweetalert2';
import { MustMatch } from '../../_helpers/must-match';
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
  public loading: boolean=false;
  constructor(private FormBuilder: FormBuilder,
    private TenantsServicesService: TenantsServicesService,
    private router :Router,

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
      Password: ['', Validators.compose([Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{6,}')])],
      ConfirmPassword: ['', Validators.required],
    },
      {
        validator: MustMatch('Password', 'ConfirmPassword')
      });
  }
  public SaveTenants(TenantsForm: any) {
    this.FormSubmitted = true;
    if (this.TenantsForm.invalid) {
      return;
    }
    this.loading = true;
    this.TenantsServicesService.SaveTenants(TenantsForm.value).subscribe((responce: any) => {
      this.loading = false;
      if (responce != null && responce.status) {
        this.Responce = responce;
        Swal.fire({
          type: 'success',
          title: responce.message,
          timer: 5000,
          showConfirmButton:true,
        })
        this.router.navigateByUrl('/login');
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
