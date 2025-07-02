import { TestBed } from '@angular/core/testing';

import { PilotsService } from './pilots.service';
import { beforeEach, describe, it } from 'node:test';

describe('PilotsService', () => {
  let service: PilotsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PilotsService);
  });  
});
