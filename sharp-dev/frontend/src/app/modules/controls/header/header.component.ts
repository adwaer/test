import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Client} from '../../../services/api/swagger';
import {Helpers} from '../../../services/api/apihelpers';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {UserService} from '../../auth/_services/user.service';
import {MovementDlgComponent} from '../movement/dlg.component';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    public user: any;
    public isLoading = true;

    constructor(public router: Router, private client: Client, private modalService: NgbModal, private userService: UserService) {
        // this.router.events.subscribe((val) => {
        //     if (val instanceof NavigationEnd && window.innerWidth <= 992) {
        //         this.toggleSidebar();
        //     }
        // });
    }

    async ngOnInit() {
        this.user = await Helpers.withAsync(this.client.users_Get());
        this.isLoading = false;
    }

    open() {
        this.modalService.open(MovementDlgComponent).result.then(async result => {
            this.isLoading = true;
            await this.ngOnInit();
        });
    }

    onLoggedout() {
        this.userService.logout();
    }
}
