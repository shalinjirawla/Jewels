import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{CategoriesComponent} from './categories/categories.component';
import { ProductComponent } from './product/product.component';

import {BrandComponent} from './brand/brand.component';
import {RawMaterialsComponent} from './raw-materials/raw-materials.component';
import {ServiceComponent} from './service/service.component';
import { AuthGuardService} from './../../shared/guards/auth-guard.service';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Products'
    },
    children: [
      {
        path: '',
        redirectTo: 'product'
      },
      {
        path: 'product',
        component: ProductComponent,
        canActivate: [AuthGuardService],
        data: {
          title: 'Product'
        }
      },
      {
        path:'categories',
        component:CategoriesComponent,
        canActivate: [AuthGuardService],
        data:{
          title:'Product Categories'
        }
      },
      {
        path:'brand',
        component:BrandComponent,
        canActivate: [AuthGuardService],
        data:{
          title:'Product Brand'
        }
      },
      {
        path:'raw-materials',
        component:RawMaterialsComponent,
        canActivate: [AuthGuardService],
        data:{
          title:'Raw Materails'
        }
      },
      {
        path:'service',
        component:ServiceComponent,
        canActivate: [AuthGuardService],
        data:{
          title:'Services'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
