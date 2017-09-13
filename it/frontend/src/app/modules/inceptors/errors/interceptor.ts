import {Injectable} from '@angular/core';
import {
    HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest
} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Router} from '@angular/router';
import {NotifyService} from './notifyService';

@Injectable()
export class ResponseErrorHandler implements HttpInterceptor {
    constructor(private router: Router, private notifyService: NotifyService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log('req', req)
        return next
            .handle(req)
            .catch((err: any) => {
                console.log('err', err)
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 401) {
                        this.router.navigate(['/login']);
                    }

                    const msg: string = err.error && err.error.message || 'user_errors|error_occured';
                    if (err.status === 400 && err.error.modelState) {
                        this.notifyService.notifyModelError(msg, err.error.modelState);
                    } else {
                        this.notifyService.notify(msg);
                    }

                    return Observable.throw(err);
                }
            });
    }
}
