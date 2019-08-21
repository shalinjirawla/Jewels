import { Component, OnInit,ViewChild } from '@angular/core';
import {ModalDirective} from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  @ViewChild('largeModal', {static: false}) public largeModal: ModalDirective;
  constructor() { }

  ngOnInit() {
  }
  
}
