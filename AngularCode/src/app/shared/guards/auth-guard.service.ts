import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let isLoggedIn = false;
    debugger
    // ... your login logic here 
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
