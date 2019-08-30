/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.0.4.0 (NJsonSchema v10.0.21.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import {mergeMap as _observableMergeMap, catchError as _observableCatch} from 'rxjs/operators';
import {Observable, throwError as _observableThrow, of as _observableOf} from 'rxjs';
import {Injectable, Inject, Optional, InjectionToken} from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class Client {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl ? baseUrl : '';
  }

  /**
   * Get all countries
   * @return Success
   */
  getAll(): Observable<CountryViewModel[]> {
    let url_ = this.baseUrl + '/api/Countries';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    };

    return this.http.request('get', url_, options_).pipe(_observableMergeMap((response_: any) => {
      return this.processGetAll(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGetAll(<any> response_);
        } catch (e) {
          return <Observable<CountryViewModel[]>> <any> _observableThrow(e);
        }
      } else {
        return <Observable<CountryViewModel[]>> <any> _observableThrow(response_);
      }
    }));
  }

  protected processGetAll(response: HttpResponseBase): Observable<CountryViewModel[]> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (<any> response).error instanceof Blob ? (<any> response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    ;
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        if (Array.isArray(resultData200)) {
          result200 = [] as any;
          for (let item of resultData200) {
            result200!.push(CountryViewModel.fromJS(item));
          }
        }
        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        return throwException('An unexpected server error occurred.', status, _responseText, _headers);
      }));
    }
    return _observableOf<CountryViewModel[]>(<any> null);
  }

  /**
   * Get provinces
   * @return Success
   */
  get(countryId: number): Observable<ProvinceViewModel[]> {
    let url_ = this.baseUrl + '/api/Countries/{countryId}/Provinces';
    if (countryId === undefined || countryId === null) {
      throw new Error('The parameter \'countryId\' must be defined.');
    }
    url_ = url_.replace('{countryId}', encodeURIComponent('' + countryId));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    };

    return this.http.request('get', url_, options_).pipe(_observableMergeMap((response_: any) => {
      return this.processGet(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGet(<any> response_);
        } catch (e) {
          return <Observable<ProvinceViewModel[]>> <any> _observableThrow(e);
        }
      } else {
        return <Observable<ProvinceViewModel[]>> <any> _observableThrow(response_);
      }
    }));
  }

  protected processGet(response: HttpResponseBase): Observable<ProvinceViewModel[]> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (<any> response).error instanceof Blob ? (<any> response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    ;
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        if (Array.isArray(resultData200)) {
          result200 = [] as any;
          for (let item of resultData200) {
            result200!.push(ProvinceViewModel.fromJS(item));
          }
        }
        return _observableOf(result200);
      }));
    } else if (status === 400) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result400: any = null;
        let resultData400 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result400 = ErrorResponse.fromJS(resultData400);
        return throwException('A server error occurred.', status, _responseText, _headers, result400);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        return throwException('An unexpected server error occurred.', status, _responseText, _headers);
      }));
    }
    return _observableOf<ProvinceViewModel[]>(<any> null);
  }

  /**
   * Creates a new user.
   * @param createAccountRequest (optional) Query model 'Create account'.
   * @return Success
   */
  createUser(createAccountRequest: CreateAccountRequest | null | undefined): Observable<UserTokenQueryResult> {
    let url_ = this.baseUrl + '/api/Users';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(createAccountRequest);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    };

    return this.http.request('post', url_, options_).pipe(_observableMergeMap((response_: any) => {
      return this.processCreateUser(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processCreateUser(<any> response_);
        } catch (e) {
          return <Observable<UserTokenQueryResult>> <any> _observableThrow(e);
        }
      } else {
        return <Observable<UserTokenQueryResult>> <any> _observableThrow(response_);
      }
    }));
  }

  protected processCreateUser(response: HttpResponseBase): Observable<UserTokenQueryResult> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body :
        (<any> response).error instanceof Blob ? (<any> response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    ;
    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result200 = UserTokenQueryResult.fromJS(resultData200);
        return _observableOf(result200);
      }));
    } else if (status === 400) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result400: any = null;
        let resultData400 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result400 = ErrorResponse.fromJS(resultData400);
        return throwException('A server error occurred.', status, _responseText, _headers, result400);
      }));
    } else if (status === 401) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result401: any = null;
        let resultData401 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result401 = ProblemDetails.fromJS(resultData401);
        return throwException('A server error occurred.', status, _responseText, _headers, result401);
      }));
    } else if (status === 404) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result404: any = null;
        let resultData404 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result404 = ErrorResponse.fromJS(resultData404);
        return throwException('A server error occurred.', status, _responseText, _headers, result404);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        return throwException('An unexpected server error occurred.', status, _responseText, _headers);
      }));
    }
    return _observableOf<UserTokenQueryResult>(<any> null);
  }
}

/** Country model */
export class CountryViewModel implements ICountryViewModel {
  /** Id */
  id?: number | undefined;
  /** Name */
  name?: string | undefined;

  constructor(data?: ICountryViewModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.id = data['id'];
      this.name = data['name'];
    }
  }

  static fromJS(data: any): CountryViewModel {
    data = typeof data === 'object' ? data : {};
    let result = new CountryViewModel();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['id'] = this.id;
    data['name'] = this.name;
    return data;
  }
}

/** Country model */
export interface ICountryViewModel {
  /** Id */
  id?: number | undefined;
  /** Name */
  name?: string | undefined;
}

/** Province model */
export class ProvinceViewModel implements IProvinceViewModel {
  /** Name */
  name?: string | undefined;

  constructor(data?: IProvinceViewModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.name = data['name'];
    }
  }

  static fromJS(data: any): ProvinceViewModel {
    data = typeof data === 'object' ? data : {};
    let result = new ProvinceViewModel();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['name'] = this.name;
    return data;
  }
}

/** Province model */
export interface IProvinceViewModel {
  /** Name */
  name?: string | undefined;
}

/** Response model 'Error'. */
export class ErrorResponse implements IErrorResponse {
  /** Error description. */
  message?: string | undefined;

  constructor(data?: IErrorResponse) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.message = data['message'];
    }
  }

  static fromJS(data: any): ErrorResponse {
    data = typeof data === 'object' ? data : {};
    let result = new ErrorResponse();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['message'] = this.message;
    return data;
  }
}

/** Response model 'Error'. */
export interface IErrorResponse {
  /** Error description. */
  message?: string | undefined;
}

/** Query model 'Create account'. */
export class CreateAccountRequest implements ICreateAccountRequest {
  /** Email */
  email?: string | undefined;
  /** Password */
  password?: string | undefined;
  /** Password Confirmation */
  confirmPassword?: string | undefined;

  constructor(data?: ICreateAccountRequest) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.email = data['email'];
      this.password = data['password'];
      this.confirmPassword = data['confirmPassword'];
    }
  }

  static fromJS(data: any): CreateAccountRequest {
    data = typeof data === 'object' ? data : {};
    let result = new CreateAccountRequest();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['email'] = this.email;
    data['password'] = this.password;
    data['confirmPassword'] = this.confirmPassword;
    return data;
  }
}

/** Query model 'Create account'. */
export interface ICreateAccountRequest {
  /** Email */
  email?: string | undefined;
  /** Password */
  password?: string | undefined;
  /** Password Confirmation */
  confirmPassword?: string | undefined;
}

export class UserTokenQueryResult implements IUserTokenQueryResult {
  readonly token?: string | undefined;

  constructor(data?: IUserTokenQueryResult) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      (<any> this).token = data['token'];
    }
  }

  static fromJS(data: any): UserTokenQueryResult {
    data = typeof data === 'object' ? data : {};
    let result = new UserTokenQueryResult();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['token'] = this.token;
    return data;
  }
}

export interface IUserTokenQueryResult {
  token?: string | undefined;
}

export class ProblemDetails implements IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;

  constructor(data?: IProblemDetails) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) {
          (<any> this)[property] = (<any> data)[property];
        }
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.type = data['type'];
      this.title = data['title'];
      this.status = data['status'];
      this.detail = data['detail'];
      this.instance = data['instance'];
    }
  }

  static fromJS(data: any): ProblemDetails {
    data = typeof data === 'object' ? data : {};
    let result = new ProblemDetails();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['type'] = this.type;
    data['title'] = this.title;
    data['status'] = this.status;
    data['detail'] = this.detail;
    data['instance'] = this.instance;
    return data;
  }
}

export interface IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;
}

export class ApiException extends Error {
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

  protected isApiException = true;

  static isApiException(obj: any): obj is ApiException {
    return obj.isApiException === true;
  }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
  if (result !== null && result !== undefined) {
    return _observableThrow(result);
  } else {
    return _observableThrow(new ApiException(message, status, response, headers, null));
  }
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = event => {
        observer.next((<any> event.target).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}
