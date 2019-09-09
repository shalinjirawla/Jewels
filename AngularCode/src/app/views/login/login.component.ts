import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
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
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {

  LoginForm: FormGroup;
  Responce: any;
  FormSubmitted: boolean = false;
  constructor(private FormBuilder: FormBuilder, private ApplicationUserService: ApplicationUserService) { }
  ngOnInit() {
    this.Onload();
  }
  public Onload() {
    this.LoginForm = this.FormBuilder.group({
      UserName: ['', Validators.compose([Validators.required])],
      Password: ['', Validators.compose([Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{6,}')])],
      AccessToken: [''],
    });
  }
  public LoginProcess(LoginForm:FormControl){
    this.FormSubmitted=true;
    if(this.LoginForm.invalid){
      return;
    }
    this.ApplicationUserService.LogInProcess(LoginForm.value).subscribe((responce:any)=>{
      this.Responce=responce;
    });
  }
  get f(){return this.LoginForm.controls;}
}  
