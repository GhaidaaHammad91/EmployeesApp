import { TestBed } from '@angular/core/testing';

import { EmployeesService } from './employees.service';

describe('EmployeeServiceService', () => {
  let service: EmployeesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
