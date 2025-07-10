import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PilotEditComponent } from './pilot-edit-component';

describe('PilotEditComponent', () => {
  let component: PilotEditComponent;
  let fixture: ComponentFixture<PilotEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PilotEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PilotEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
