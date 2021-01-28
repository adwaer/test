import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Select} from '@ngxs/store';
import {Observable} from 'rxjs';
import {AlertState} from '@core/state/alert.state';
import {IAlertModel} from '@core/state/alert.model';
import {Disposable} from 'src/app/shared/helpers';
import {filter, takeUntil} from 'rxjs/operators';

@Component({
  selector: 'app-notifications',
  template: '',
  styleUrls: ['./notifications.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NotificationsComponent extends Disposable {

  @Select(AlertState) alert$!: Observable<IAlertModel>;
  private durationInMs = 10000;

  constructor(private snackBar: MatSnackBar) {
    super();

    this.alert$
      .pipe(
        takeUntil(this.disposed$),
        filter(alert => !!alert.type)
      )
      .subscribe(alert => {
        this.showMsg(alert.message, alert.type);
      });
  }

  private showMsg(msg: string, style: 'none' | 'success' | 'warning' | 'error' | 'info'): void {
    this.snackBar.open(msg, undefined, {
      duration: this.durationInMs,
      panelClass: style,
      verticalPosition: 'top'
    });
  }

}
