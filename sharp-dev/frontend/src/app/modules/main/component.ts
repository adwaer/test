import {Component, OnInit, ViewChild} from '@angular/core';
import {routerTransition} from '../../router.animations';
import {Helpers} from '../../services/api/apihelpers';
import {Client, MovementsQueryCondition} from '../../services/api/swagger';
import {MovementDlgComponent} from '../controls/movement/dlg.component';
import {NgForm} from '@angular/forms';

@Component({
    selector: 'app-main',
    templateUrl: './component.html',
    styleUrls: ['./component.scss'],
    animations: [routerTransition()]
})
export class MainComponent implements OnInit {
    public movements: any = [];
    public criteria: MovementsQueryCondition = new MovementsQueryCondition();

    public isLoading = true;
    @ViewChild('form') form: NgForm;

    public searchUsers = [];

    constructor(private client: Client) {
    }

    async ngOnInit() {

        await this.fetch();
        await this.valuechange('');

        MovementDlgComponent.messagePublished$.subscribe(async () => {
            await this.fetch();
        });
    }

    async valuechange(newVal) {
        newVal = newVal || '';
        const response = await Helpers.withAsync(this.client.userSearch_Get(newVal));
        this.searchUsers = response.data;
    }

    async searchUserPatternChange(newVal) {
        this.criteria.correspond = newVal;
    }

    private async fetch() {
        try {
            const result = await Helpers.withAsync(this.client.movements_Get(this.criteria))

            const m = result.data;
            for (let i = 0; i < m.length; i++) {
                m[i].date += 'Z';
            }

            this.movements = m;

        }
        finally {
            this.isLoading = false;
        }
    }

    async onSubmit() {
        if (this.form.invalid) {
            return;
        }

        await this.fetch();
    }
}
