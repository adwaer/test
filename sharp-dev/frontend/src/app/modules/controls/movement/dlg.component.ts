import {Component, EventEmitter, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {Client, MovementViewModel} from '../../../services/api/swagger';
import {Helpers} from '../../../services/api/apihelpers';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {NgForm} from '@angular/forms';
import {NotifyService} from '../../inceptors/errors/notifyService';

@Component({
    selector: 'app-movement-dlg',
    templateUrl: './dlg.component.html',
    // styleUrls: ['./header.component.scss']
})
export class MovementDlgComponent implements OnInit {

    public static messagePublished$: EventEmitter<boolean> = new EventEmitter();
    public isLoading = true;
    public searchUsers = [];
    public movementModel: MovementViewModel = new MovementViewModel();
    @ViewChild('form') form: NgForm;

    constructor(public router: Router, private client: Client, public activeModal: NgbActiveModal) {
    }

    async ngOnInit() {
        this.movementModel.amount = 0;
        await this.valuechange('');
        this.isLoading = false;
    }

    async valuechange(newVal) {
        newVal = newVal || '';
        const response = await Helpers.withAsync(this.client.userSearch_Get(newVal));
        this.searchUsers = response.data;
    }

    async searchUserPatternChange(newVal) {
        this.movementModel.targetEmail = newVal;
    }

    public close() {
        this.activeModal.close();
    }

    public async sendMoney() {
        if (this.form.invalid) {
            return;
        }

        const email = this.searchUsers.filter(u => u === this.movementModel.targetEmail);
        if (!email.length) {
            NotifyService.messagePublished$.emit({message: 'Please select user from list', status: 400});
            return;
        }
        if (!this.movementModel.amount || this.movementModel.amount < 0) {
            NotifyService.messagePublished$.emit({message: 'Amount must be positive value', status: 400});
            return;
        }

        this.isLoading = true;
        try {
            await Helpers.withAsync(this.client.movements_Post(this.movementModel));
            MovementDlgComponent.messagePublished$.emit();
        }
        finally {
            this.isLoading = false;
        }
        this.activeModal.close();
    }
}
