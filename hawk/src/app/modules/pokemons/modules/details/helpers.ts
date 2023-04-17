import {PokemonSprites} from "@core/api/models/pokemon";

export interface Sprites { avatars: string[], others: string[] }

export const spritesToImages = (sprites: PokemonSprites): Sprites => {
  const others: string[] = getImages(sprites);
  let avatars: string[] = [];
  if (sprites.other) {
    const {home, dream_world} = sprites.other;
    avatars = [...getImages(home), ...getImages(dream_world)];
  }

  return {avatars, others};
}

function getImages(obj: any): string[] {
  return Object.keys(obj)
    .filter(s => {
      const val = obj[s];
      return val && typeof val === 'string';
    })
    .map(s => obj[s]);
}
