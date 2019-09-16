import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ApplicationUserService } from '../../Services/ApplicationUser/application-user.service';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
import { element } from 'protractor';
import { Router } from '@angular/router';
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
  myVar: any;
  FormSubmitted: boolean = false;
  constructor(private FormBuilder: FormBuilder, private ApplicationUserService: ApplicationUserService,
    private router: Router) {
      setInterval(() => {
        this.LogOut()
    }, 3600000); }
  ngOnInit() {
    this.Onload();
    this.LogOut();
  }
  public LogOut() {
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }
  public Onload() {
    this.LoginForm = this.FormBuilder.group({
      UserName: ['brijesh@ncoresoft.com', Validators.compose([Validators.required])],
      Password: ['Ravi@4404', Validators.compose([Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{6,}')])],
      AccessToken: [''],
    });
  }
  public LoginProcess(LoginForm: FormControl) {
    this.FormSubmitted = true;
    if (this.LoginForm.invalid) {
      return;
    }
    this.ApplicationUserService.LogInProcess(LoginForm.value).subscribe((responce: any) => {
      if (responce != null && responce.status) {
        debugger
        this.Responce = responce.data;
        if (this.Responce != null && this.Responce != undefined) {
          localStorage.setItem('AccessToken', this.Responce.accessToken);
          localStorage.setItem('UserId', this.Responce.userId);
          localStorage.setItem('TenantId', this.Responce.tenantId);
          this.router.navigateByUrl('/dashboard');
        } else {
          Swal.fire({
            type:'error',
            title:responce.message,
          });
        }
      } else {
        Swal.fire({
          type: 'error',
          title: responce.message,
          timer: 2000,
        })
      }
    });
  }
  get f() { return this.LoginForm.controls; }

  
 

}  
