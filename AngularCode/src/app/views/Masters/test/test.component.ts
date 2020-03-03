import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl } from '@angular/forms';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent implements OnInit {

  constructor(private fb: FormBuilder) { }

  productForm: FormGroup;

  ngOnInit() {

    /* Initiate the form structure */
    this.productForm = this.fb.group({
      title: [],
      selling_points: this.fb.array([this.fb.group({ point: '' })])
    })
  }
  addSellingPoint() {
    this.sellingPoints.push(this.fb.group({ point: '' }));
  }

  deleteSellingPoint(index) {
    this.sellingPoints.removeAt(index);
  }
  ///////// This is new ////////
  get sellingPoints() {
    return this.productForm.get('selling_points') as FormArray;
  }
  Save(productForm: any) {
  }
}
export class Product {
  name: string
  selling_points: SellingPoint[]
}

export class SellingPoint {
  selling_point: string
}