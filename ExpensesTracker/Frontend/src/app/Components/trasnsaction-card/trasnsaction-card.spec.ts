import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrasnsactionCard } from './trasnsaction-card';

describe('TrasnsactionCard', () => {
  let component: TrasnsactionCard;
  let fixture: ComponentFixture<TrasnsactionCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrasnsactionCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrasnsactionCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
