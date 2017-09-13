import {NgModule} from '@angular/core';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {ResponseErrorHandler} from './interceptor';
import {NotifyService} from './notifyService';
import {CommonModule} from '@angular/common';
import {NotifyComponent} from './component';
import {RouterModule} from '@angular/router';

@NgModule({
    imports: [
        CommonModule,
        RouterModule
    ],
    declarations: [NotifyComponent],
    exports: [NotifyComponent]
})

export class ResponseHandlingModule {
    static forRoot() {
        return {
            ngModule: ResponseHandlingModule,
            providers: [NotifyService, {
                provide: HTTP_INTERCEPTORS,
                useClass: ResponseErrorHandler,
                multi: true
            }]
        };
    }
}
