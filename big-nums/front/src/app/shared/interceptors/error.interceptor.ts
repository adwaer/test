import {Injectable} from '@angular/core';
import {HttpRequest, HttpHandler, HttpEvent, HttpInterceptor} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Store} from '@ngxs/store';
import {AlertError} from '@core/state/alert.actions';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private store: Store) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(catchError(err => {
        this.alertErr(err).then();

        return throwError(err);
      }));
  }

  async alertErr(err: any): Promise<void> {
    let msg = err.message;
    if (err.error instanceof Blob) {
      const content = await err.error.text();
      msg = (JSON.parse(content)).message;
    }

    this.store.dispatch(new AlertError(msg));
  }
}
