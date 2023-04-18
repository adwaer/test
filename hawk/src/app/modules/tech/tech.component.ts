import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-tech',
  templateUrl: './tech.component.html',
  styleUrls: ['./tech.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TechComponent {
}
