import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferFundsComponent } from './transfer-funds.component';

describe('TransferFundsComponent', () => {
  let component: TransferFundsComponent;
  let fixture: ComponentFixture<TransferFundsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransferFundsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransferFundsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
