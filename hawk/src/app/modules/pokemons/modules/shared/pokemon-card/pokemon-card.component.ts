import {ChangeDetectionStrategy, Component, Input} from '@angular/core';

@Component({
  selector: 'app-pokemon-card',
  templateUrl: './pokemon-card.component.html',
  styleUrls: ['./pokemon-card.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonCardComponent {
  @Input() link!: string[];
  @Input() id!: number;
  @Input() title!: string;
}
