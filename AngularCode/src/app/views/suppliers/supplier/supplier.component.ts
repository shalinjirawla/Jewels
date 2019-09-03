import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  ModelTitleString:string="Add New Supplier";
  FormSubmitted:boolean=false;
  constructor() { }

  ngOnInit() {
    this.Onload()
  }
  public Onload()
  {

  }
  public ResetForm()
  {
    this.FormSubmitted=false;
    this.Onload();
    this.largeModal.hide();
  }
}
