import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PilotDeleteComponent } from './pilot-delete-component';

describe('PilotDeleteComponent', () => {
  let component: PilotDeleteComponent;
  let fixture: ComponentFixture<PilotDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PilotDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PilotDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
