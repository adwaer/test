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
     * Get balance
     * @return OK
     */
    balance_Get(): Observable<any> {
        let url_ = this.baseUrl + "/Balance";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processBalance_Get(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processBalance_Get(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processBalance_Get(response: Response): Observable<any> {
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
     * Change balance
     * @return OK
     */
    balance_Post(model: MovementViewModel): Observable<any> {
        let url_ = this.baseUrl + "/Balance";
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
            return this.transformResult(url_, response_, (r) => this.processBalance_Post(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processBalance_Post(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processBalance_Post(response: Response): Observable<any> {
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
     * @model_login login
     * @model_password password
     * @return OK
     */
    login_Get(model_login: string, model_password: string): Observable<any> {
        let url_ = this.baseUrl + "/Login?";
        if (model_login === undefined || model_login === null)
            throw new Error("The parameter 'model_login' must be defined and cannot be null.");
        else
            url_ += "model.login=" + encodeURIComponent("" + model_login) + "&";
        if (model_password === undefined || model_password === null)
            throw new Error("The parameter 'model_password' must be defined and cannot be null.");
        else
            url_ += "model.password=" + encodeURIComponent("" + model_password) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processLogin_Get(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processLogin_Get(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processLogin_Get(response: Response): Observable<any> {
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
     * Create user
     * @return OK
     */
    login_Post(model: RegViewModel): Observable<any> {
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

    /**
     * Get user data
     * @return OK
     */
    users_Get(): Observable<any> {
        let url_ = this.baseUrl + "/Users";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processUsers_Get(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processUsers_Get(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processUsers_Get(response: Response): Observable<any> {
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
     * Get users
     * @return OK
     */
    userSearch_Get(condition: string): Observable<any> {
        let url_ = this.baseUrl + "/UserSearch?";
        if (condition === undefined || condition === null)
            throw new Error("The parameter 'condition' must be defined and cannot be null.");
        else
            url_ += "condition=" + encodeURIComponent("" + condition) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = {
            method: "get",
            headers: new Headers({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request(url_, options_).flatMap((response_) => {
            return this.transformResult(url_, response_, (r) => this.processUserSearch_Get(r));
        }).catch((response_: any) => {
            if (response_ instanceof Response) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processUserSearch_Get(r));
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processUserSearch_Get(response: Response): Observable<any> {
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

/** Movement model */
export class MovementViewModel implements IMovementViewModel {
    /** amount */
    amount: number;
    /** target email */
    targetEmail: string;

    constructor(data?: IMovementViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.amount = data["amount"];
            this.targetEmail = data["targetEmail"];
        }
    }

    static fromJS(data: any): MovementViewModel {
        let result = new MovementViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["amount"] = this.amount;
        data["targetEmail"] = this.targetEmail;
        return data;
    }
}

/** Movement model */
export interface IMovementViewModel {
    /** amount */
    amount: number;
    /** target email */
    targetEmail: string;
}

/** login model */
export class LoginViewModel implements ILoginViewModel {
    /** login */
    login: string;
    /** password */
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

/** login model */
export interface ILoginViewModel {
    /** login */
    login: string;
    /** password */
    password: string;
}

/** reg model */
export class RegViewModel implements IRegViewModel {
    /** user name */
    userName: string;
    /** email */
    email: string;
    /** password */
    password: string;

    constructor(data?: IRegViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.userName = data["userName"];
            this.email = data["email"];
            this.password = data["password"];
        }
    }

    static fromJS(data: any): RegViewModel {
        let result = new RegViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["email"] = this.email;
        data["password"] = this.password;
        return data;
    }
}

/** reg model */
export interface IRegViewModel {
    /** user name */
    userName: string;
    /** email */
    email: string;
    /** password */
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
        let errData = JSON.parse(response);
        NotifyService.messagePublished$.emit({message: errData.exceptionMessage ? errData.exceptionMessage : errData.message, status});
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
