import {Action, State, StateContext} from '@ngxs/store';
import {Injectable} from '@angular/core';
import {AlertError, AlertSuccess} from './alert.actions';
import {IAlertModel} from './alert.model';

@State<IAlertModel>({
  name: 'alert',
})
@Injectable()
export class AlertState {

  @Action(AlertError)
  error(ctx: StateContext<IAlertModel>, {payload}: AlertError): void {

    ctx.setState({
      message: payload,
      type: 'error'
    });
  }

  @Action(AlertSuccess)
  success(ctx: StateContext<IAlertModel>, {payload}: AlertError): void {
    ctx.setState({
      message: payload,
      type: 'success'
    });
  }

}
