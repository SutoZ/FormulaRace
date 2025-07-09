import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PilotDeleteComponent } from './pilot-delete-component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('PilotDeleteComponent', () => {
  let component: PilotDeleteComponent;
  let fixture: ComponentFixture<PilotDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PilotDeleteComponent, NoopAnimationsModule]
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
