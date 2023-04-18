import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LandingComponent{
}
