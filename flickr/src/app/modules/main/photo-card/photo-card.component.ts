import {Component, ChangeDetectionStrategy, Input} from '@angular/core';
import {IFlickerPhoto} from "../../../core/services/flickr.service";

@Component({
  selector: 'app-photo-card',
  templateUrl: './photo-card.component.html',
  styleUrls: ['./photo-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PhotoCardComponent {
  @Input() photo!: IFlickerPhoto;

  get url() {
    const {id, server, secret} = this.photo;
    return `https://live.staticflickr.com/${server}/${id}_${secret}.jpg`
  }

  get title() {
    return this.photo.title;
  }
}
