import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpWrapperService} from './service';

@NgModule({
    imports: [
        CommonModule
    ]
})

export class HttpWrapperModule {
    static forRoot() {
        return {
            ngModule: HttpWrapperModule,
            providers: [HttpWrapperService]
        };
    }
}
