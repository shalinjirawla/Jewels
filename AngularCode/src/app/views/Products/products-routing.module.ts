import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{CategoriesComponent} from './categories/categories.component';
import { ProductComponent } from './product/product.component';
import {BrandComponent} from './brand/brand.component';
import {RawMaterialsComponent} from './raw-materials/raw-materials.component';
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
        data: {
          title: 'Product'
        }
      },
      {
        path:'categories',
        component:CategoriesComponent,
        data:{
          title:'Product Categories'
        }
      },
      {
        path:'brand',
        component:BrandComponent,
        data:{
          title:'Product Brand'
        }
      },
      {
        path:'raw-materials',
        component:RawMaterialsComponent,
        data:{
          title:'Raw Materails'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MastersRoutingModule { }
