import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SrvErrorComponent } from './srv-error.component';

describe('SrvErrorComponent', () => {
  let component: SrvErrorComponent;
  let fixture: ComponentFixture<SrvErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SrvErrorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SrvErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
