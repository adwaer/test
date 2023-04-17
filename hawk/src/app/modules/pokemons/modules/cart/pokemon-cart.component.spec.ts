import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonCartComponent } from './pokemon-cart.component';

describe('PokemonCartComponent', () => {
  let component: PokemonCartComponent;
  let fixture: ComponentFixture<PokemonCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PokemonCartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PokemonCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
