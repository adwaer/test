import {EventEmitter, Injectable} from '@angular/core';

@Injectable()
export class NotifyService {
    public static messagePublished$: EventEmitter<any> = new EventEmitter();

    public notify(message: any) {
        NotifyService.messagePublished$.emit(message);
    }

    public notifyModelError(message: string, modelState: any) {
        let resultMessage: string = message;
        modelState.forEach(function (element) {
            resultMessage += ' [' + element + '] ';
        });
        this.notify(resultMessage);
    }
}
