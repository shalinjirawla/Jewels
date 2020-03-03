import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) { }
  v: any;
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let isLoggedIn = false;
    this.v = new Date();
    let p = localStorage.getItem("logintime");
    let diff = this.v.valueOf() - parseInt(p);
    let diffInHours = diff / 1000 / 60 / 60;
    if (diffInHours == 1 || diffInHours >= 1) {
      localStorage.clear();
    }
    // setTimeout(function () {
    //   localStorage.removeItem("AccessToken");
    //   localStorage.removeItem("UserId");
    //   localStorage.removeItem("TenantId");
    // }, 1000 * 60 * 60);
    var AccessToken = localStorage.getItem("AccessToken");
    var UserId = localStorage.getItem("UserId");
    var TenantId = localStorage.getItem("TenantId");
    if (AccessToken != null && AccessToken != '' && TenantId != null && TenantId != '' && UserId != null && UserId != '') {
      isLoggedIn = true;
    }
    if (isLoggedIn) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }


}
