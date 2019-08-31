import { Component, OnInit ,ViewChild} from '@angular/core';
import {ModalDirective} from 'ngx-bootstrap/modal';
import {FormGroup,FormBuilder,FormControl} from '@angular/forms';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @ViewChild('largeModal', {static: false}) public largeModal: ModalDirective;
  constructor(private FormBuilder :FormBuilder) { }

  //Title For Model Start..
  ModelTitleString:string="Add New Product";
  //Title For Model End...
  //Form List for Product Start..
  ProductForm:FormGroup;
  //Form List for Product End//

  ngOnInit() {
  }
 public OnLoad()
  {
    this.ProductForm=this.FormBuilder.group({

    });
  }
}
