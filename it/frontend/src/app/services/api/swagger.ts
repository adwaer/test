﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v11.5.1.0 (NJsonSchema v9.4.10.0) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/catch';
import {Observable} from 'rxjs/Observable';
import {Injectable, Inject} from '@angular/core';
import {Http, Headers, Response} from '@angular/http';
import {environment} from "../../../environments/environment";
import {ApiBase} from "./api.base";
import {NotifyService} from "../../modules/inceptors/errors/notifyService";
import {HttpWrapperService} from "../http-wrapper/service";

@Injectable()
export class Client extends ApiBase {
    private http: HttpWrapperService;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpWrapperService) http: HttpWrapperService) {
        super();
        this.http = http;
        this.baseUrl = environment.dataApi;
    }

    /**
     * gets address books
     * @return OK
     */
    abInfo_Get(): Observable<any> {
        let url_ = this.baseUrl + "/AbInfo";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processAbInfo_Get(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processAbInfo_Get(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processAbInfo_Get(response: Response): Observable<any> {
        const status = response.status;

        let _headers: any = response.headers ? response.headers.toJSON() : {};
        if (status === 200) {
            const _responseText = response.text();
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);

            return Observable.of(resultData200);
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.text();
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Observable.of<any>(<any>null);
    }

    /**
     * gets address books by id
     * @return OK
     */
    abInfo_Get2(id: number): Observable<any> {
        let url_ = this.baseUrl + "/AbInfo/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processAbInfo_Get2(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processAbInfo_Get2(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processAbInfo_Get2(response: Response): Observable<any> {
        const status = response.status;

        let _headers: any = response.headers ? response.headers.toJSON() : {};
        if (status === 200) {
            const _responseText = response.text();
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (resultData200) {
                result200 = {};
                for (let key in resultData200) {
                    if (resultData200.hasOwnProperty(key))
                        result200[key] = resultData200[key] !== undefined ? resultData200[key] : <any>null;
                }
            }
            return Observable.of(result200);
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.text();
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Observable.of<any>(<any>null);
    }

    /**
     * Validate login password pair
     * @return OK
     */
    login_Post(model: LoginViewModel): Observable<any> {
        let url_ = this.baseUrl + "/Login";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ = {
            body: content_,
            method: "post",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processLogin_Post(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processLogin_Post(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processLogin_Post(response: Response): Observable<any> {
        const status = response.status;

        let _headers: any = response.headers ? response.headers.toJSON() : {};
        if (status === 200) {
            const _responseText = response.text();
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (resultData200) {
                result200 = {};
                for (let key in resultData200) {
                    if (resultData200.hasOwnProperty(key))
                        result200[key] = resultData200[key] !== undefined ? resultData200[key] : <any>null;
                }
            }
            return Observable.of(result200);
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.text();
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Observable.of<any>(<any>null);
    }
}

export class LoginViewModel implements ILoginViewModel {
    login: string;
    password: string;

    constructor(data?: ILoginViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.login = data["login"];
            this.password = data["password"];
        }
    }

    static fromJS(data: any): LoginViewModel {
        let result = new LoginViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["login"] = this.login;
        data["password"] = this.password;
        return data;
    }
}

export interface ILoginViewModel {
    login: string;
    password: string;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined) {
        NotifyService.messagePublished$.emit(result);
        return Observable.throw(result);
    }
    else {
        NotifyService.messagePublished$.emit({message: JSON.parse(response).message, status});
        return Observable.throw(new SwaggerException(message, status, response, headers, null));
    }
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        let reader = new FileReader();
        reader.onload = function () {
            observer.next(this.result);
            observer.complete();
        }
        reader.readAsText(blob);
    });
}
