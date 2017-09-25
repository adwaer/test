import {Component, Input} from '@angular/core';

@Component({
    selector: 'app-loader',
    templateUrl: './component.html',
    styleUrls: ['./component.scss']
})
export class LoaderComponent {
    @Input() state: boolean;

    constructor() {
    }

}
