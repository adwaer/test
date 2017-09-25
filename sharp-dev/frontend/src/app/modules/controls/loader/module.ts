import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {LoaderComponent} from './component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
    ],
    declarations: [LoaderComponent],
    exports: [LoaderComponent]
})

export class LoaderModule {
}
