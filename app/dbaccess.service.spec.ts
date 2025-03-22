import { TestBed } from '@angular/core/testing';

import { DBAccessService } from './dbaccess.service';

describe('DBAccessService', () => {
  let service: DBAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DBAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
