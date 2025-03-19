import { TestBed } from '@angular/core/testing';

import { MychangeemitterService } from './mychangeemitter.service';

describe('MychangeemitterService', () => {
  let service: MychangeemitterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MychangeemitterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
