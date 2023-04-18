import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonBuyCardComponent } from './pokemon-buy-card.component';

describe('PokemonBuyCardComponent', () => {
  let component: PokemonBuyCardComponent;
  let fixture: ComponentFixture<PokemonBuyCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PokemonBuyCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PokemonBuyCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
