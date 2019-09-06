import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

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
export class RegisterComponent  implements OnInit {

  constructor(private FormBuilder:FormBuilder) { }
  RegisterForm:FormGroup;
  FormSubmitted:boolean=false;

   ngOnInit(){
  this.Onload();
  }
  public Onload()
  {
    this.RegisterForm=this.FormBuilder.group({
      TenantId:[0],
      TenantName:['',Validators.required],
      EmailId:['',Validators.required,Validators],
      Password:['',Validators.required],
      ConfirmPassword:['',Validators.required],
    });

  }
  public SaveRegsiter(RegisterForm:FormControl)
  {
    this.FormSubmitted=true;
   if(this.RegisterForm.invalid){
     return;
   }
  }
  get f(){
     return this.RegisterForm.controls;
    }
}
