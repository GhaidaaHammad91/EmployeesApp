import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { EmployeesService } from '../services/employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styles: []
})
export class EmployeesComponent implements OnInit {

  employeesdata$: any;
  contact$: any;
  constructor(private service: EmployeesService) {
    this.LoadEmployees();
  }

  ngOnInit(): void {

  }

  LoadEmployees() {
    this.service.getEmployees().subscribe(result => {
      this.employeesdata$ = result;
    });
  }

  delete(code: any) {
    if (confirm("Are you sure you want delete this employee?")) {
      this.service.deleteEmployee(code).subscribe(result => {
        this.LoadEmployees();
      });
    }
  }
}
