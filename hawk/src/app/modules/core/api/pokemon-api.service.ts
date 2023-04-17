import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {
  Ability,
  Characteristic,
  EggGroup,
  Gender,
  GrowthRate,
  LocationAreaEncounter,
  Nature,
  PokeathlonStat,
  Pokemon, PokemonColor, PokemonForm, PokemonHabitat, PokemonShape, PokemonSpecies, Stat, Type
} from "./models/pokemon";
import {Endpoints} from "./constants";
import {NamedAPIResourceList} from "./models/common";

@Injectable()
/**
 * THIS FILE AND MODELS PARTIALLY COPIED FROM: https://github.com/Gabb-c/pokenode-ts
 * P.S. I don't wanna use axios, I don't like their logger, Promises.. etc..
 *
 * ### Pokemon Client
 *
 * Client used to access the Pokemon Endpoints:
 *  - [Abilities](https://pokeapi.co/docs/v2#abilities)
 *  - [Characteristics](https://pokeapi.co/docs/v2#characteristics)
 *  - [Egg Groups](https://pokeapi.co/docs/v2#egg-groups)
 *  - [Genders](https://pokeapi.co/docs/v2#genders)
 *  - [Growth Rates](https://pokeapi.co/docs/v2#growth-rates)
 *  - [Natures](https://pokeapi.co/docs/v2#natures)
 *  - [Pokeathlon Stats](https://pokeapi.co/docs/v2#pokeathlon-stats)
 *  - [Pokemon](https://pokeapi.co/docs/v2#pokemon)
 *  - [Pokemon Location Areas](https://pokeapi.co/docs/v2#pokemon-location-areas)
 *  - [Pokemon Colors](https://pokeapi.co/docs/v2#pokemon-colors)
 *  - [Pokemon Forms](https://pokeapi.co/docs/v2#pokemon-forms)
 *  - [Pokemon Habitats](https://pokeapi.co/docs/v2#pokemon-habitats)
 *  - [Pokemon Shapes](https://pokeapi.co/docs/v2#pokemon-shapes)
 *  - [Pokemon Species](https://pokeapi.co/docs/v2#pokemon-species)
 *  - [Stats](https://pokeapi.co/docs/v2#stats)
 *  - [Types](https://pokeapi.co/docs/v2#types)
 * ---
 * See [PokéAPI Documentation](https://pokeapi.co/docs/v2#pokemon-section)
 */
export class PokemonApiService {

  constructor(private httpClient: HttpClient) {
  }

  /**
   * Get an Ability by it's name
   * @param name The Ability name
   * @returns An Ability
   */
  public getAbilityByName(name: string): Observable<Ability> {
    const url = `${Endpoints.Ability}/${name}`;

    return this.httpClient.get<Ability>(url);
  }

  /**
   * Get an Ability by it's ID
   * @param id The Ability ID
   * @returns An Ability
   */
  public getAbilityById(id: number): Observable<Ability> {
    return this.httpClient.get<Ability>(`${Endpoints.Ability}/${id}`);
  }

  /**
   * Get a Characteristic by it's ID
   * @param id The Characteristic ID
   * @returns A Characteristic
   */
  public getCharacteristicById(id: number): Observable<Characteristic> {
    const url = `${Endpoints.Characteristic}/${id}`;
    return this.httpClient.get<Characteristic>(url);
  }

  /**
   * Get an Egg Group by it's name
   * @param name The Egg Group name
   * @returns An Egg Group
   */
  public getEggGroupByName(name: string): Observable<EggGroup> {
    const url = `${Endpoints.EggGroup}/${name}`;
    return this.httpClient.get<EggGroup>(url);
  }

  /**
   * Get an Egg Group by it's ID
   * @param id The Egg Group ID
   * @returns An Egg Group
   */
  public getEggGroupById(id: number): Observable<EggGroup> {
    const url = `${Endpoints.EggGroup}/${id}`;
    return this.httpClient.get<EggGroup>(url);
  }

  /**
   * Get a Gender by it's name
   * @param name The gender name
   * @returns An Egg Group
   */
  public getGenderByName(name: string): Observable<Gender> {
    const url = `${Endpoints.Gender}/${name}`;
    return this.httpClient.get<Gender>(url);
  }

  /**
   * Get a Gender by it's ID
   * @param id The Gender ID
   * @returns A Gender
   */
  public getGenderById(id: number): Observable<Gender> {
    const url = `${Endpoints.Gender}/${id}`;
    return this.httpClient.get<Gender>(url);
  }

  /**
   * Get a Growth Rate by it's name
   * @param name The Growth Rate name
   * @returns A Growth Rate
   */
  public getGrowthRateByName(name: string): Observable<GrowthRate> {
    const url = `${Endpoints.GrowthRate}/${name}`;
    return this.httpClient.get<GrowthRate>(url);
  }

  /**
   * Get a Growth Rate by it's ID
   * @param id The Growth Rate ID
   * @returns A Growth Rate
   */
  public getGrowthRateById(id: number): Observable<GrowthRate> {
    const url = `${Endpoints.GrowthRate}/${id}`;
    return this.httpClient.get<GrowthRate>(url);
  }

  /**
   * Get a Nature by it's name
   * @param name The Nature name
   * @returns A Nature
   */
  public getNatureByName(name: string): Observable<Nature> {
    const url = `${Endpoints.Nature}/${name}`;
    return this.httpClient.get<Nature>(url);
  }

  /**
   * Get a Nature by it's ID
   * @param id The Nature ID
   * @returns A Nature
   */
  public getNatureById(id: number): Observable<Nature> {
    const url = `${Endpoints.Nature}/${id}`;
    return this.httpClient.get<Nature>(url);
  }

  /**
   * Get a Pokeathlon Stat by it's name
   * @param name The Pokeathlon Stat name
   * @returns A Pokeathlon Stat
   */
  public getPokeathlonStatByName(name: string): Observable<PokeathlonStat> {
    const url = `${Endpoints.PokeathlonStat}/${name}`;
    return this.httpClient.get<PokeathlonStat>(url);
  }

  /**
   * Get a Pokeathlon Stat by it's ID
   * @param id The Pokeathlon Stat ID
   * @returns A Pokeathlon Stat
   */
  public getPokeathlonStatById(id: number): Observable<PokeathlonStat> {
    const url = `${Endpoints.PokeathlonStat}/${id}`;
    return this.httpClient.get<PokeathlonStat>(url);
  }

  /**
   * Get a Pokemon by it's name
   * @param name The Pokemon name
   * @returns A Pokemon Stat
   */
  public getPokemonByName(name: string): Observable<Pokemon> {
    const url = `${Endpoints.Pokemon}/${name}`;
    return this.httpClient.get<Pokemon>(url);
  }

  /**
   * Get a Pokemon by it's ID
   * @param id The Pokemon ID
   * @returns A Pokemon
   */
  public getPokemonById(id: number): Observable<Pokemon> {
    const url = `${Endpoints.Pokemon}/${id}`;
    return this.httpClient.get<Pokemon>(url);
  }

  /**
   * Get a Pokemon Location Area by it's ID
   * @param id The Pokemon Location Area ID
   * @returns A Pokemon Location Area
   */
  public getPokemonLocationAreaById(id: number): Observable<LocationAreaEncounter[]> {
    const url = `${Endpoints.PokemonLocationArea.replace(':id', id.toString())}`;
    return this.httpClient.get<LocationAreaEncounter[]>(url);
  }

  /**
   * Get a Pokemon Color by it's name
   * @param name The Pokemon Color name
   * @returns A Pokemon Color
   */
  public getPokemonColorByName(name: string): Observable<PokemonColor> {
    const url = `${Endpoints.PokemonColor}/${name}`;
    return this.httpClient.get<PokemonColor>(url);
  }

  /**
   * Get a Pokemon Color by it's ID
   * @param id The Pokemon Color ID
   * @returns A Pokemon Color
   */
  public getPokemonColorById(id: number): Observable<PokemonColor> {
    const url = `${Endpoints.PokemonColor}/${id}`;
    return this.httpClient.get<PokemonColor>(url);
  }

  /**
   * Get a Pokemon Form by it's name
   * @param name The Pokemon Form name
   * @returns A Pokemon Form
   */
  public getPokemonFormByName(name: string): Observable<PokemonForm> {
    const url = `${Endpoints.PokemonForm}/${name}`;
    return this.httpClient.get<PokemonForm>(url);
  }

  /**
   * Get a Pokemon Form by it's ID
   * @param id The Pokemon Form ID
   * @returns A Pokemon Form
   */
  public getPokemonFormById(id: number): Observable<PokemonForm> {
    const url = `${Endpoints.PokemonForm}/${id}`;
    return this.httpClient.get<PokemonForm>(url);
  }

  /**
   * Get a Pokemon Habitat by it's name
   * @param name The Pokemon Habitat name
   * @returns A Pokemon Habitat
   */
  public getPokemonHabitatByName(name: string): Observable<PokemonHabitat> {
    const url = `${Endpoints.PokemonHabitat}/${name}`;
    return this.httpClient.get<PokemonHabitat>(url);
  }

  /**
   * Get a Pokemon Habitat by it's ID
   * @param id The Pokemon Habitat Form ID
   * @returns A Pokemon Habitat
   */
  public getPokemonHabitatById(id: number): Observable<PokemonHabitat> {
    return this.httpClient.get<PokemonHabitat>(`${Endpoints.PokemonHabitat}/${id}`);
  }

  /**
   * Get a Pokemon Shape by it's name
   * @param name The Pokemon Shape name
   * @returns A Pokemon Shape
   */
  public getPokemonShapeByName(name: string): Observable<PokemonShape> {
    return this.httpClient.get<PokemonShape>(`${Endpoints.PokemonShape}/${name}`);
  }

  /**
   * Get a Pokemon Shape by it's ID
   * @param id The Pokemon Shape Form ID
   * @returns A Pokemon Shape
   */
  public getPokemonShapeById(id: number): Observable<PokemonShape> {
    return this.httpClient.get<PokemonShape>(`${Endpoints.PokemonShape}/${id}`);
  }

  /**
   * Get a Pokemon Species by it's name
   * @param name The Pokemon Species name
   * @returns A Pokemon Species
   */
  public getPokemonSpeciesByName(name: string): Observable<PokemonSpecies> {
    return this.httpClient.get<PokemonSpecies>(`${Endpoints.PokemonSpecies}/${name}`);
  }

  /**
   * Get a Pokemon Species by it's ID
   * @param id The Pokemon Species Form ID
   * @returns A Pokemon Species
   */
  public getPokemonSpeciesById(id: number): Observable<PokemonSpecies> {
    return this.httpClient
      .get<PokemonSpecies>(`${Endpoints.PokemonSpecies}/${id}`);
  }

  /**
   * Get a Stat by it's name
   * @param name The Stat name
   * @returns A Stat
   */
  public getStatByName(name: string): Observable<Stat> {
    return this.httpClient
      .get<Stat>(`${Endpoints.Stat}/${name}`);
  }

  /**
   * Get a Stat by it's ID
   * @param id The Stat ID
   * @returns A Stat
   */
  public getStatById(id: number): Observable<Stat> {
    return this.httpClient
      .get<Stat>(`${Endpoints.Stat}/${id}`);
  }

  /**
   * Get a Type by it's name
   * @param name The Type name
   * @returns A Type
   */
  public getTypeByName(name: string): Observable<Type> {
    return this.httpClient
      .get<Type>(`${Endpoints.Type}/${name}`);
  }

  /**
   * Get a Type by it's ID
   * @param id The Type ID
   * @returns A Type
   */
  public getTypeById(id: number): Observable<Type> {
    return this.httpClient
      .get<Type>(`${Endpoints.Type}/${id}`);
  }

  /**
   * List Abilities
   * @param offset The first item that you will get
   * @param limit How many Abilities per page
   * @returns A list of Abilities
   */
  public listAbilities(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.Ability}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Characteristics
   * @param offset The first item that you will get
   * @param limit How many Characteristics per page
   * @returns A list of Characteristics
   */
  public listCharacteristics(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.Characteristic}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Egg Groups
   * @param offset The first item that you will get
   * @param limit How many Egg Groups per page
   * @returns A list of Egg Groups
   */
  public listEggGroups(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.EggGroup}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Genders
   * @param offset The first item that you will get
   * @param limit How many Genders per page
   * @returns A list of Genders
   */
  public listGenders(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(`${Endpoints.Gender}?offset=${offset || 0}&limit=${limit || 20}`);
  }

  /**
   * List Growth Rates
   * @param offset The first item that you will get
   * @param limit How many Growth Rates per page
   * @returns A list of Growth Rates
   */
  public listGrowthRates(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.GrowthRate}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Natures
   * @param offset The first item that you will get
   * @param limit How many Growth Natures per page
   * @returns A list of Natures
   */
  public listNatures(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(`${Endpoints.Nature}?offset=${offset || 0}&limit=${limit || 20}`);
  }

  /**
   * List Pokeathlon Stats
   * @param offset The first item that you will get
   * @param limit How many Pokeathlon Stats per page
   * @returns A list of Pokeathlon Stats
   */
  public listPokeathlonStats(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokeathlonStat}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemons
   * @param offset The first item that you will get
   * @param limit How many Pokemons Stats per page
   * @returns A list of Pokemons
   */
  public listPokemons(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.Pokemon}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemon Colors
   * @param offset The first item that you will get
   * @param limit How many Pokemon Colors per page
   * @returns A list of Pokemon Colors
   */
  public listPokemonColors(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokemonColor}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemon Forms
   * @param offset The first item that you will get
   * @param limit How many Pokemon Forms per page
   * @returns A list of Pokemon Forms
   */
  public listPokemonForms(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokemonForm}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemon Habitats
   * @param offset The first item that you will get
   * @param limit How many Pokemon Habitats per page
   * @returns A list of Pokemon Habitats
   */
  public listPokemonHabitats(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokemonHabitat}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemon Shapes
   * @param offset The first item that you will get
   * @param limit How many Pokemon Shapes per page
   * @returns A list of Pokemon Shapes
   */
  public listPokemonShapes(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokemonShape}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Pokemon Species
   * @param offset The first item that you will get
   * @param limit How many Pokemon Species per page
   * @returns A list of Pokemon Species
   */
  public listPokemonSpecies(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(
        `${Endpoints.PokemonSpecies}?offset=${offset || 0}&limit=${limit || 20}`
      );
  }

  /**
   * List Stats
   * @param offset The first item that you will get
   * @param limit How many Stats per page
   * @returns A list of Stats
   */
  public listStats(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(`${Endpoints.Stat}?offset=${offset || 0}&limit=${limit || 20}`);
  }

  /**
   * List Types
   * @param offset The first item that you will get
   * @param limit How many Types per page
   * @returns A list of Types
   */
  public listTypes(offset?: number, limit?: number): Observable<NamedAPIResourceList> {
    return this.httpClient
      .get<NamedAPIResourceList>(`${Endpoints.Type}?offset=${offset || 0}&limit=${limit || 20}`);
  }

}
