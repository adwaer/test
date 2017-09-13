import {Injectable} from '@angular/core';
import 'rxjs/add/operator/map'
import * as http from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {LoginViewModel} from '../api/swagger';

@Injectable()
export class HttpWrapperService {

    constructor(private http: http.Http) {
    }

    public request(url: string | http.Request, options?: http.RequestOptionsArgs): Observable<http.Response> {

        const userData = JSON.parse(localStorage.getItem('currentUser'));
        const user = new LoginViewModel(userData);

        if (user) {
            const headers: http.Headers = options.headers || new http.Headers();
            this.createAuthorizationHeader(headers, `${user.login}:${user.password}`);
            options.headers = headers;
        }

        return this.http.request(url, options);
    }

    private createAuthorizationHeader(headers: http.Headers, data) {
        headers.append('auth', 'Basic ' +
            btoa(data));
    }
}
