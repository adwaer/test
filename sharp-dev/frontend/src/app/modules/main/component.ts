import {Component, OnInit} from '@angular/core';
import {routerTransition} from '../../router.animations';
import {Helpers} from '../../services/api/apihelpers';
import {Client} from '../../services/api/swagger';

@Component({
    selector: 'app-main',
    templateUrl: './component.html',
    styleUrls: ['./component.scss'],
    animations: [routerTransition()]
})
export class MainComponent implements OnInit {
    public data: any = [];
    public isLoading = true;

    constructor(private client: Client) {
    }

    async ngOnInit() {
        try {
            // const results = await Helpers.withAsync(this.client.abInfo_Get());
            // const groups = this.groupBy(results, 'division');
            // for (const group in groups) {
            //     const items = groups[group];
            //     this.data.push({
            //         group,
            //         items
            //     });
            // }
            //
            // console.log('this.data', this.data);
        }
        finally {
            this.isLoading = false;
        }
    }

    private groupBy(array, prop) {
        return array.reduce(function (groups, item) {
            const val = item[prop];
            groups[val] = groups[val] || [];
            groups[val].push(item);
            return groups;
        }, {});
    }
}
