import { TestBed } from '@angular/core/testing';

import { TmaccessService } from './tmaccess.service';

describe('TmaccessService', () => {
  let service: TmaccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TmaccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
