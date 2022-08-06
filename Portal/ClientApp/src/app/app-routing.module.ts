import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { EmployeesComponent } from './employees/employees.component';
import { AddEmployeeComponent } from './employees/add-employee/add-employee.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: '', component: LoginComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'employees', component: EmployeesComponent},
  { path: 'employees/add', component: AddEmployeeComponent},
  { path: 'employees/edit/:id', component: AddEmployeeComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
