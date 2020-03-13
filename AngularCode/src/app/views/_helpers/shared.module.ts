import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';

import {FixedToDigitZero} from './FixedToDigitZero.pipe';
@NgModule({
    declarations:[
        FixedToDigitZero
    ],
    exports:[FixedToDigitZero],
   
    
})
export class SharedModule{

}