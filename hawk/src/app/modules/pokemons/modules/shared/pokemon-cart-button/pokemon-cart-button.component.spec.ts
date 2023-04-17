import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonCartButtonComponent } from './pokemon-cart-button.component';

describe('PokemonCartButtonComponent', () => {
  let component: PokemonCartButtonComponent;
  let fixture: ComponentFixture<PokemonCartButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PokemonCartButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PokemonCartButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
