import {AfterViewInit, Component, Input} from '@angular/core';
import {FormControl, NgForm} from '@angular/forms';

@Component({
    selector: 'app-validate',
    templateUrl: './component.html',
    styleUrls: ['./component.scss'],
})
export class ValidationComponent {
    @Input() element: FormControl;
    @Input() form: NgForm;

    constructor() {
    }

}
