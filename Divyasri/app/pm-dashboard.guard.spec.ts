import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { pmDashboardGuard } from './pm-dashboard.guard';

describe('pmDashboardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => pmDashboardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
