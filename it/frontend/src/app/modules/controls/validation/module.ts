import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ValidationComponent} from './component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
    ],
    declarations: [ValidationComponent],
    exports: [ValidationComponent]
})

export class ValidationModule {
}
