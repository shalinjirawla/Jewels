import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{CategoriesComponent} from './categories/categories.component';
//import { ProductComponent } from './product/product.component';


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
      // {
      //   path: 'product',
      //  // component: ProductComponent,
      //   data: {
      //     title: 'Product'
      //   }
      // },
      {
        path:'categories',
        component:CategoriesComponent,
        data:{
          title:'Product Categories'
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
