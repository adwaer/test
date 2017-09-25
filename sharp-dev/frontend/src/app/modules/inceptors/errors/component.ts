import {Component} from '@angular/core';
import {NotifyService} from './notifyService';

@Component({
    selector: 'app-notify-error',
    templateUrl: 'component.html',
    styleUrls: ['./component.scss']
})
export class NotifyComponent {
    public message: string;

    constructor() {
        NotifyService.messagePublished$.subscribe(data => {
            if (data.status === 200) {
                this.message = null;
            }
            if (data.status === 401) {
                this.message = 'You dont have any rights for doing this';
            } else {
                this.message = data.message;
            }
        });
        // NotifyService.messagePublished$.subscribe(message => console.log(message));
    }

    public clearMessage(): void {
        this.message = null;
    }
}
