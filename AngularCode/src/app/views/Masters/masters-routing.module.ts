import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {GeneralSetupComponent} from './general-setup/general-setup.component';
import {TestComponent} from './test/test.component';
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
      {
        path: 'test',
        component: TestComponent,
        data: {
          title: 'test',
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
