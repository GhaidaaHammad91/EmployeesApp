import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  apiurl = 'https://localhost:44368/api/Employees'

  constructor(private http: HttpClient) {

  }
  
  getEmployees(): Observable<object> {
    return this.http.get(this.apiurl);
  }

  getEmployee(id: any) {
    return this.http.get(this.apiurl + '/' + id);
  }

  deleteEmployee(id: any) {
    return this.http.delete(this.apiurl + '/' + id);
  }

  addEmployee(inputdata: any) {
    return this.http.post(this.apiurl, inputdata);
  }

  updateEmployee(id: any , inputdata: any) {
    return this.http.put(this.apiurl + '/' + id, inputdata);
  }


}
