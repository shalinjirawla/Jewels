import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomerTypeService } from '../../../../Services/Customer-Services/customer-type.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-customer-type',
  templateUrl: './customer-type.component.html',
  styleUrls: ['./customer-type.component.scss']
})
export class CustomerTypeComponent implements OnInit {
  @ViewChild('largeModal', { static: false }) public largeModal: ModalDirective;
  constructor(private customerTypeService: CustomerTypeService, private formBuilder: FormBuilder) { }

  modeltitle: string = "Add New Customer Group";
  savebtntitle: string = "Add";

  AddCustomerTypeForm: FormGroup;

  submitted: boolean = false;
  CustomerTypelist: any;

  ngOnInit() {
    this.GetCustomertypelist();
    this.onLoad();
  }

  get f() { return this.AddCustomerTypeForm.controls; }

  onLoad() {
    this.AddCustomerTypeForm = this.formBuilder.group({
      customerTypeId: [0],
      customerTypeName: ['', Validators.required]

    })
  }

  ResetForm() {
    debugger
    this.largeModal.hide();
  }

  GetCustomertypelist() {
    this.customerTypeService.GetCustomertypelist().subscribe((responce: any) => {
      this.CustomerTypelist = responce.data;
    })
  }

  addnewcustomertype() {
    this.savebtntitle = "Add"
    this.modeltitle = "Add New Customer Group";
    this.onLoad();
    this.submitted = false;
    this.largeModal.show();
  }

  AddCustomerType(AddCustomerTypeForm: FormControl) {
    this.submitted = true;
    if (AddCustomerTypeForm.invalid) {
      return
    }
    this.customerTypeService.AddCustomerType(AddCustomerTypeForm.value).subscribe((responce: any) => {
      let Result = responce;
      debugger
      if (responce.status) {
        this.GetCustomertypelist();
        debugger
        if (this.AddCustomerTypeForm.value.customerTypeId == 0) {
          Swal.fire(
            'Customer Group Added Successfully',
            responce.message,
            'success'
          )
        }
        else if (this.AddCustomerTypeForm.value.customerTypeId >= 0) {
          Swal.fire(
            'Customer Group Updated Successfully',
            responce.message,
            'success'
          )
        }

      }
      this.largeModal.hide();
    })


  }

  EditCustomerType(i: any) {
    debugger
    this.customerTypeService.GetCustomerTypeById(i).subscribe((responce: any) => {
      debugger
      let Result = responce.data;
      this.AddCustomerTypeForm.patchValue({
        customerTypeId: Result.customerTypeId,
        customerTypeName: Result.customerTypeName
      })
      this.savebtntitle = "Update";
      this.modeltitle = "Update Customer Group";
      this.largeModal.show();
    })


  }

  DeleteCustomerType(i: any) {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.customerTypeService.DeleteCustomerTypeById(i).subscribe((responce: any) => {

          this.GetCustomertypelist();
          debugger
          if (responce.status) {
            Swal.fire(
              'Deleted!',
              responce.message,
              'success'
            )
          }
        });
      }
    })

  }

}
