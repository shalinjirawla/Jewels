import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MustMatch } from '../_helpers/must-match';
import { ApplicationUserService } from '../../Services/ApplicationUser/application-user.service';
import { TenantsServicesService } from '../../Services/TenantsServices/tenants-services.service';
import { Router } from '@angular/router'
import Swal from 'sweetalert2'
import { from } from 'rxjs';
import { element } from 'protractor';
import { setTimeout } from 'timers';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})

@Component({
  selector: 'app-dashboard',
  templateUrl: 'register.component.html',

})
export class RegisterComponent implements OnInit {
  private loading: boolean = false;
  constructor(private FormBuilder: FormBuilder,
    private ApplicationUserService: ApplicationUserService,
    private TenantsServicesService: TenantsServicesService,
    private router: Router
  ) { }
  RegisterForm: FormGroup;
  FormSubmitted: boolean = false;
  Responce: any;
  UrlTenantId: number;
  UrlUserId:string;
  ngOnInit() {
    this.getQueryParameter();
  }

  public getQueryParameter(): number {
    const url = window.location.href;
    this.Onload();
    if (url.includes('?')) {
debugger
      let httpParams = url.split('?')[1];
      let data=httpParams.split("&")
      if(data!=null && data!=undefined && data.length>1){
      let TenantId= parseInt(data[0].split("=")[1]);
      let UserId=data[1].split("=")[1];
      if (TenantId != null && TenantId != 0 &&  UserId!=null &&  UserId!='') {
        this.GetRegisterData(TenantId,UserId);
      }
      return TenantId;
    }else{
      this.router.navigateByUrl('404');
    }

    } else {
      this.router.navigateByUrl('404');
    }

  }

  public Onload() {
    this.RegisterForm = this.FormBuilder.group({
      TenantId: [0],
      TenantName: ['', Validators.required],
      EmailId: ['', Validators.compose([Validators.required, Validators.email])],
      BusinessRegisterNumber: [''],
      TaxRegisterNumber: [''],
      IsActive: [true],
      Logo: [''],
      IsInTrialPeriod: [''],
      SubscriptionEndDateUtc: ['']
      });

  }
  public SaveRegsiter(RegisterForm: FormControl) {
    this.FormSubmitted = true;
    if (this.RegisterForm.invalid) {
      return;
    }
    this.loading = true;
    this.ApplicationUserService.SaveRegister(RegisterForm.value).subscribe((responce: any) => {
      this.loading = false;
      if (responce.status) {
        Swal.fire({
          type: 'success',
          title: responce.message,
          timer: 5000,
        });
        this.router.navigateByUrl('/login');
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
          timer: 5000,
        });
      }
    });
  }
  get f() {
    return this.RegisterForm.controls;
  }

  public GetRegisterData(UrlTenantId: number,UrlUserId:string) {
    this.TenantsServicesService.GetRegisterData(UrlTenantId,UrlUserId).subscribe((responce: any) => {
      if (responce.status) {
        let result = responce.data;
        Swal.fire({
          type: 'success',
          title: responce.message,
        })
        this.RegisterForm.patchValue({
          TenantId: result.tenantId,
          TenantName: result.tenantName,
          EmailId: result.emailId,
        })
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
        })
        this.router.navigateByUrl('/500');
      }


    });
  }
}
