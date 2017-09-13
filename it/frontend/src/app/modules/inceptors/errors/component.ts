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
            console.log('this', this);
            if (data.status === 200) {
                this.message = null;
            }
            if (data.status === 401) {
                this.message = 'Недостаточно прав';
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
