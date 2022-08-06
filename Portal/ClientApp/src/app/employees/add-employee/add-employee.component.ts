import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { ActivatedRoute } from '@angular/router';
import { EmployeesService } from '../../services/employees.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styles: []
})
export class AddEmployeeComponent implements OnInit {


  desigantiondata: any;
  saveresp: any;
  messageclass = '';
  message = '';
  EditData: any;
  Employeeid: any;
  constructor(private service: EmployeesService, private route: ActivatedRoute) {
    this.Employeeid = this.route.snapshot.paramMap.get('id');
    if (this.Employeeid != null && this.Employeeid != 0) {
      this.UpdateEmployee(this.Employeeid);
    }
  }

  ngOnInit(): void {
  }

  UpdateEmployee(id: any) {
    this.service.getEmployee(id).subscribe(result => {
      this.EditData = result;
      if (this.EditData != null) {
        this.employeeform = new FormGroup({
          firstName: new FormControl(this.EditData.FirstName),
          LastName: new FormControl(this.EditData.LastName),
          email: new FormControl(this.EditData.Email),
          phone: new FormControl(this.EditData.Phone),
          position: new FormControl(this.EditData.Position),
        });

      }
    });
  }

  employeeform = new FormGroup({
    firstName: new FormControl('', Validators.required),
    LastName: new FormControl('', Validators.required),
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    phone: new FormControl(''),
    position: new FormControl('', Validators.required),
  });

  saveEmployee() {
    if (this.employeeform.valid) {

      if (this.Employeeid != undefined && this.Employeeid != null) {
        this.service.updateEmployee(this.Employeeid, this.employeeform.value).subscribe(result => {
          this.saveresp = result;
          if (this.saveresp.result == 'pass') {
            this.message = "Saved Sucessfully"
            this.messageclass = 'sucess'

          } else {
            this.message = "save failed"
            this.messageclass = 'error'
          }

        });
      } else {
        this.service.addEmployee(this.employeeform.value).subscribe(result => {
          this.saveresp = result;
          if (this.saveresp.result == 'pass') {
            this.message = "Saved Sucessfully"
            this.messageclass = 'sucess'

          } else {
            this.message = "save failed"
            this.messageclass = 'error'
          }

        });
      }
    } else {
      this.message = "Please enter valid data"
      this.messageclass = 'error'
    }
  }

  get firstName() {
    return this.employeeform.get('firstName');
  }

  get lastName() {
    return this.employeeform.get('firstName');
  }

  get phone() {
    return this.employeeform.get('phone');
  }

  get email() {
    return this.employeeform.get('email');
  }

  get position() {
    return this.employeeform.get('position');
  }

}
