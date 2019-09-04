import { Component, ViewChild, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CurrencyService } from '../../../Services/Masters-Services/general-setup.service';
import Swal from 'sweetalert2'
import { from } from 'rxjs';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 3000,
})

@Component({
  selector: 'app-general-setup',
  templateUrl: './general-setup.component.html',
  styleUrls: ['./general-setup.component.scss']
})
export class GeneralSetupComponent implements OnInit {

  // Currency Model start
  @ViewChild('CurrencyModal', { static: false }) public CurrencyModal: ModalDirective;
  CurrencyModelTitleString: string = "Add Currency";
  CurrencySubmiited: boolean = false;
  CurrencyForm: FormGroup;
  CurrencyResponce: any;
  CurrencyList: any[];
  CurrencybtnText = "Save Change";
  // Currency Model end

  // Credit Term start
  @ViewChild('CreditTermModal', { static: false }) public CreditTermModal: ModalDirective;
  CreditTermModalModelTitleString: string = "Credit Terms";
  CreditTermSubmiited: boolean = false;
  CreditTermForm: FormGroup;
  // Credit Term end
  // Payment Term start
  @ViewChild('PaymentTermModal', { static: false }) public PaymentTermModal: ModalDirective;
  PaymentTermModalModelTitleString: string = "Payment Terms";
  PaymentTermSubmiited: boolean = false;
  PaymentTermForm: FormGroup;
  // Payment Term end
  constructor(private FormBuilder: FormBuilder,
    private CurrencyService: CurrencyService,

  ) { }

  ngOnInit() {
    this.OnloadCurrency();
    this.GetCurrencyList();

    this.OnloadCrediTTerms();
    this.OnloadPaymentTerms();
  }
  //#region Currency Section Start 

  public OnloadCurrency() {
    this.CurrencyForm = this.FormBuilder.group({
      CurrencyId: [0],
      CurrencyName: ['', Validators.required],
      Code: ['', Validators.required],
      Status: [''],
    });
  }
  public GetCurrencyList() {
    this.CurrencyService.GetCurrencyList().subscribe((responce: any) => {
      if (responce.status) {
        this.CurrencyList = responce.data;
        console.log(responce.data);
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        })
      }
    })
  }
  public AddCurrency(CurrencyForm: FormControl) {
    this.CurrencySubmiited = true;
    if (this.CurrencyForm.invalid) {
      return;
    }
    this.CurrencyService.SaveCurrency(this.CurrencyForm.value).subscribe((responce: any) => {
      if (responce.status) {
        this.OnloadCurrency();
        Toast.fire({
          type: 'success',
          title: responce.message,
        })
      } else {
        Toast.fire({
          type: 'error',
          title: responce.message,
        })
      }
      this.CurrencySubmiited = false;
      this.CurrencybtnText = "Save Change";
    });

  }
  public GetCurrency(CurrencyId: any) {
    if (CurrencyId != 0) {
      this.CurrencyService.GetCurrency(CurrencyId).subscribe((responce: any) => {
        if (responce.status) {
          this.CurrencybtnText = "Update Change";
          this.CurrencyResponce = responce.data;
          this.CurrencyForm.patchValue({
            CurrencyId: this.CurrencyResponce.currencyId,
            CurrencyName: this.CurrencyResponce.currencyName,
            Code: this.CurrencyResponce.code,
            Status: this.CurrencyResponce.status,
          })
        } else {
          Toast.fire({
            type: 'error',
            title: responce.message,
          })
        }
      })
    }
  }
  public DeleteCurrency(CurrencyId: any) {
    if (CurrencyId != 0) {
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
          this.CurrencyService.DeleteCurrency(CurrencyId).subscribe((responce: any) => {
            this.GetCurrencyList();
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
  public CurrencyChange(event, CurrencyId) {
    if (CurrencyId != 0) {
      this.CurrencyService.CureencyStatusChange(CurrencyId, event).subscribe((responce: any) => {
        if (responce.status) {
          this.GetCurrencyList();
          Toast.fire({
            type:'success',
            title:responce.message,
          })
        }else{
          Toast.fire({
            type:'error',
            title:responce.message,
          })
        }
      });
    }
  }
  get fCurrency() { return this.CurrencyForm.controls; }
  //#endregion Currency Section End 
  public OnloadCrediTTerms() {
    this.CreditTermForm = this.FormBuilder.group({
      CreditTermsId: [0],
      CreditTermsCode: ['', Validators.required],
      Duration: ['', Validators.required],
      Description: [''],
    });
  }
  public AddCreditTerms(CreditTermForm: FormControl) {
    this.CreditTermSubmiited = true;
    if (CreditTermForm.invalid) {
      return;
    }
  }
  get fCreditTerms() { return this.CreditTermForm.controls; }

  //#region Payment Section Start
  public OnloadPaymentTerms() {
    this.PaymentTermForm = this.FormBuilder.group({
      PaymentTermsId: [0],
      PaymentTermsCode: ['', Validators.required],
      Duration: ['', Validators.required],
      Description: [''],
    });
  }
  public AddPaymentTerms(PaymentTermForm: FormControl) {
    this.PaymentTermSubmiited = true;
    if (PaymentTermForm.invalid) {
      return;
    }
  }
  get fPaymentTerms() { return this.PaymentTermForm.controls; }
  //#endregion Payment Section Edn
}
