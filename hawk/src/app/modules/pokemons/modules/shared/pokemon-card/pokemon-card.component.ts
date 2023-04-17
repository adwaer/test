import {ChangeDetectionStrategy, Component, Input} from '@angular/core';

@Component({
  selector: 'app-pokemon-card',
  templateUrl: './pokemon-card.component.html',
  styleUrls: ['./pokemon-card.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonCardComponent {
  @Input() link!: any[];
  @Input() id!: number;
  @Input() price!: number;
  @Input() title!: string;
}
