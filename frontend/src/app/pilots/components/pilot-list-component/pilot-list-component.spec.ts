import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PilotListComponent } from './pilot-list-component';

describe('PilotListComponent', () => {
  let component: PilotListComponent;
  let fixture: ComponentFixture<PilotListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PilotListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PilotListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
