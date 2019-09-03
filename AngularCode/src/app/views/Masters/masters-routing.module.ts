import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {GeneralSetupComponent} from './general-setup/general-setup.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Masters'
    },
    children: [
      {
        path: '',
        redirectTo: 'general-setup'
      },
      {
        path: 'general-setup',
        component: GeneralSetupComponent,
        data: {
          title: 'General Setup'
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
