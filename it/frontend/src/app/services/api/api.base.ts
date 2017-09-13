import 'rxjs/add/operator/toPromise';
import {Observable} from 'rxjs/Observable';
import {Response, ResponseOptions} from '@angular/http';
import {Headers} from '@angular/http';
import {HttpHeaders, HttpResponse} from '@angular/common/http';
import {NotifyService} from "../../modules/inceptors/errors/notifyService";

export abstract class ApiBase {
    constructor() {
    }

    protected transformResult(url: string, response: HttpResponse<Object> | Response, processResponse: any): Observable<any> {
        if (response instanceof Response) {
            NotifyService.messagePublished$.emit({status: 200});
            return processResponse(response);
        }

        const newHeaders: Headers = new Headers();

        if (response.headers && response.headers instanceof HttpHeaders) {
            const headerKeys: string[] = response.headers.keys();
            if (!!headerKeys) {
                headerKeys.forEach((key: string, index: number, arr: string[]) => {
                    const headerValues: string[] = response.headers.getAll(key);
                    headerValues.forEach((value: string, i: number, values: string[]) => {
                        newHeaders.append(key, value);
                    })
                });
            }
        }

        const newOptions: ResponseOptions = new ResponseOptions({
            status: response.status,
            statusText: response.statusText,
            url: response.url,
            headers: newHeaders,
            body: JSON.stringify(response.body)
        });
        const newResponse: Response = new Response(newOptions);

        const result = processResponse(newResponse);

        return result;
    }
}
