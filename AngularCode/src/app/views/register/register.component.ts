import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MustMatch } from '../_helpers/must-match';
import { ApplicationUserService } from '../../Services/ApplicationUser/application-user.service';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
import { element } from 'protractor';
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

  constructor(private FormBuilder: FormBuilder,
    private ApplicationUserService: ApplicationUserService,
  ) { }
  RegisterForm: FormGroup;
  FormSubmitted: boolean = false;
  Responce: any;
  UrlTenantId:number;
  ngOnInit() {
    this.Onload();
    this.GetRegisterData(this.getQueryParameter())
  }

  public getQueryParameter():number {
    const url = window.location.href;
    if (url.includes('?')) {
      let httpParams = url.split('?')[1];
      this.UrlTenantId = parseInt(httpParams.split("=")[1]);
      return this.UrlTenantId;
    }
   
  }

  public Onload() {
    this.RegisterForm = this.FormBuilder.group({
      TenantId: [0],
      TenantName: ['', Validators.required],
      EmailId: ['', Validators.compose([Validators.required, Validators.email])],
      Password: ['', Validators.compose([Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{6,}')])],
      ConfirmPassword: ['', Validators.required],
      BusinessRegisterNumber:[0],
      TaxRegisterNumber:[0],
      IsActive:[true],
      Logo:[''],
      IsInTrialPeriod:[''],
      SubscriptionEndDateUtc:['']
    },
      {
        validator: MustMatch('Password', 'ConfirmPassword')
      });

  }
  public SaveRegsiter(RegisterForm: FormControl) {
    this.FormSubmitted = true;
    if (this.RegisterForm.invalid) {
      return;
    }
    this.ApplicationUserService.SaveRegister(RegisterForm.value).subscribe((responce: any) => {
      return this.Responce = responce;
    });
  }
  get f() {
    return this.RegisterForm.controls;
  }

  public GetRegisterData(UrlTenantId:number) {
    this.ApplicationUserService.GetRegisterData(UrlTenantId).subscribe((responce: any) => {
      debugger
      let result;
      result=responce.data;

      this.RegisterForm.patchValue({
        TenantId: result.tenantId,
      TenantName: result.tenantName,
      EmailId: result.emailId,
      Password: [''],
      ConfirmPassword: [''],
      BusinessRegisterNumber:[0],
      TaxRegisterNumber:[0],
      IsActive:[true],
      Logo:[''],
      IsInTrialPeriod:[''],
      SubscriptionEndDateUtc:['']
      })

    });
  }
}
