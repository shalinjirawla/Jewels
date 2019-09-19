import { TestBed } from '@angular/core/testing';

import { RawMaterailsService } from './raw-materails.service';

describe('RawMaterailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RawMaterailsService = TestBed.get(RawMaterailsService);
    expect(service).toBeTruthy();
  });
});
