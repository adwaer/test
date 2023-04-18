import {ChangeDetectionStrategy, Component, Input} from '@angular/core';

@Component({
  selector: 'app-pokemon-buy-card',
  templateUrl: './pokemon-buy-card.component.html',
  styleUrls: ['./pokemon-buy-card.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonBuyCardComponent {
  @Input() id!: number;
  @Input() name!: string;
  @Input() link!: string[];
}
