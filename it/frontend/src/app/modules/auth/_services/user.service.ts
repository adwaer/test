import {Injectable} from '@angular/core';
import 'rxjs/add/operator/map'
import {Client, LoginViewModel} from '../../../services/api/swagger';
import {Helpers} from '../../../services/api/apihelpers';

@Injectable()
export class UserService {
    private currentUser: LoginViewModel;

    constructor(private api: Client) {
    }

    public isAuthenticated(): boolean {
        if (!this.currentUser) {
            this.loadFromStore();
        }
        return !!this.currentUser;
    }

    get CurrentUser(): LoginViewModel {
        if (!this.currentUser) {
            this.loadFromStore();
        }
        return this.currentUser;
    }

    private loadFromStore() {
        const userData = JSON.parse(localStorage.getItem('currentUser'));
        this.currentUser = userData && new LoginViewModel(userData)
    }

    public async login(creds: LoginViewModel): Promise<any> {
        const result = await Helpers.withAsync(this.api.login_Post(creds));

        this.currentUser = creds;
        localStorage.setItem('currentUser', JSON.stringify(creds));

        return result;
    }

}
