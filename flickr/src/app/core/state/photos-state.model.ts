import {IFlickerPhoto} from "../services/flickr.service";

export interface IPhotosStateModel {
  search?: string;
  images: IFlickerPhoto[];
}
