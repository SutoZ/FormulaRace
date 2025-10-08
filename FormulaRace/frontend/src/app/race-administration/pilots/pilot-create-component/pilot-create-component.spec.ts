import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PilotCreateComponent } from './pilot-create-component';

describe('PilotCreateComponent', () => {
  let component: PilotCreateComponent;
  let fixture: ComponentFixture<PilotCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PilotCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PilotCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
