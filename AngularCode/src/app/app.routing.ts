import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { TenantsComponent } from './views/Tenants/tenants/tenants.component';
import { AuthGuardService} from './shared/guards/auth-guard.service';
  import { from } from 'rxjs';
export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: 'registertenants',
    component: TenantsComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuardService],
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'customers',
        loadChildren: () => import('./views/customers/customers.module').then(m => m.CustomerModule),
        canActivate: [AuthGuardService],
      },
     
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule),
        canActivate: [AuthGuardService],
      },
      {
        path: 'sales',
        loadChildren: () => import('./views/Sales/sales.module').then(m => m.SalesModule),
        canActivate: [AuthGuardService],
      },
      {
        path:'products',
        loadChildren:()=>import('./views/products/products.module').then(m=>m.ProductsModule),
        canActivate: [AuthGuardService],
      },
      {
        path:'suppliers',
        loadChildren:()=>import('./views/suppliers/suppliers.module').then(m=>m.SuppliersModule),
        canActivate: [AuthGuardService],
      },
      {
        path:'masters',
        loadChildren:()=>import('./views/Masters/masters.module').then(m=>m.MastersModule),
        canActivate: [AuthGuardService],
      }

      
    ]
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
