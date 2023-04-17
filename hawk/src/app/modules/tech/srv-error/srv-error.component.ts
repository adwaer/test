import {ChangeDetectionStrategy, Component} from '@angular/core';

@Component({
  selector: 'app-srv-error',
  templateUrl: './srv-error.component.html',
  styleUrls: ['./srv-error.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SrvErrorComponent {
}
