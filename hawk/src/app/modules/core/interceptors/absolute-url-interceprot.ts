import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {BaseURL} from "../api/constants";

@Injectable()
export class AbsoluteUrlInterceprot implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      url: `${BaseURL.REST}${request.url}`
    });

    return next.handle(request);
  }

}
