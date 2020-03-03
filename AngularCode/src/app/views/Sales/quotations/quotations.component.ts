import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CategoriesModel } from '../../../Models/ProductModels/ProductsModel';
import { ProductCategoriesService } from '../../../Services/Products-Services/product-categories.service';
import Swal from 'sweetalert2'
import { TaxCodeService } from '../../../Services/Masters-Services/general-setup.service';
import { ServicesService } from './../../../Services/Products-Services/services.service';
import { finalize } from 'rxjs/operators';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,

})

@Component({
  selector: 'app-quotations',
  templateUrl: './quotations.component.html',
  styleUrls: ['./quotations.component.scss']
})
export class QuotationsComponent implements OnInit {

  constructor() { }
  numbers: any;
  ngOnInit() {
  }
  
}
