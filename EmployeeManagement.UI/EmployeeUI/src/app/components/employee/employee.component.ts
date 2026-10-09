import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Department } from '../../models/department';
import { EmployeeService } from '../../services/employee.service';
import { CreateEmployeeRequest } from '../../models/create-employee-request';
import { ResponseEmployee } from '../../models/response-employee';
import { EmployeeQueryParameters } from '../../models/employee-query-parameters';
import { PageResponse } from '../../models/page-response';
import { UpdateEmployeeRequest } from '../../models/update-employee-request';

@Component({
  selector: 'app-employee',
  standalone: false,
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css',
})
export class EmployeeComponent {
  employeeForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
  ) {}
  departments = Object.values(Department);
  employees: ResponseEmployee[] = [];

  currentPage = 1;
  pageSize = 10;
  totalRecords = 0;
  totalPages = 0;
  search = '';

  selectedEmployeeId: number | null = null;
  selectedDepartment: Department | null = null;
  sortBy = '';
  sortOrder = 'asc';

  ngOnInit(): void {
    this.employeeForm = this.fb.group({
      employeeId: [null],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      department: [Department.IT, Validators.required],
      salary: [0, Validators.required],
    });
    this.getEmployees();
  }

  applyFilters(): void {
    this.currentPage = 1;
    this.getEmployees();
  }
  clearFilters(): void {
    this.search = '';
    this.selectedDepartment = null;
    this.sortBy = '';
    this.sortOrder = 'asc';

    this.currentPage = 1;
    this.getEmployees();
  }
  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }

    this.currentPage = page;
    this.getEmployees();
  }
  cancelEdit(): void {
    this.employeeForm.reset({
      employeeId: null,
      name: '',
      email: '',
      department: Department.IT,
      salary: 0,
    });
    this.selectedEmployeeId = null;
    this.employeeForm.markAsPristine();
    this.employeeForm.markAsUntouched();
  }

  getEmployees(): void {
    const queryParam: EmployeeQueryParameters = {
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      search: this.search.trim(),
      department: this.selectedDepartment,
      sortBy: this.sortBy || null,
      sortOrder: this.sortBy ? this.sortOrder : null,
    };
    this.employeeService.getEmployee(queryParam).subscribe({
      next: (response: PageResponse) => {
        this.employees = response.data;
        this.currentPage = response.pageNumber;
        this.pageSize = response.pageSize;
        this.totalRecords = response.totalRecords;
        this.totalPages = response.totalPages;
      },
      error: (error) => {
        console.log('Error loading employees', error);
      },
    });
  }

  deleteEmployee(id: number): void {
    if (!confirm('Are you sure want to delete this employee?')) {
      return;
    }
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        console.log('Employee deleted');

        //refresh the employee list
        this.getEmployees();
      },
      error: (error) => {
        console.error('Error deleting employee', error);
      },
    });
  }

  editEmployee(id: number): void {
    this.selectedEmployeeId = id;
    this.employeeService.getEmployeeById(id).subscribe({
      next: (employee: ResponseEmployee) => {
        // Fills the form; the bound inputs update automatically
        this.employeeForm.patchValue({
          employeeId: employee.employeeId,
          name: employee.name,
          email: employee.email,
          department: employee.department,
          salary: employee.salary,
        });
      },
      error: (error) => {
        // A 429 from the API's rate limiter shows up here (often as a CORS error in the browser)
        console.log('Error getting employee', error);
        this.selectedEmployeeId = null;
      },
    });
  }

  saveEmployee(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    const { name, email, department, salary } = this.employeeForm.value;

    if (this.selectedEmployeeId === null) {
      const request: CreateEmployeeRequest = {
        name,
        email,
        department,
        salary,
      };
      this.employeeService.createEmployee(request).subscribe({
        next: () => {
          this.cancelEdit();
          this.getEmployees();
        },
        error: (error) => console.log(error),
      });
    } else {
      const request: UpdateEmployeeRequest = {
        employeeId: this.selectedEmployeeId,
        name,
        email,
        department,
        salary,
      };
      this.employeeService.updateEmployee(request).subscribe({
        next: () => {
          this.cancelEdit();
          this.getEmployees();
        },
        error: (error) => console.log('Error updating employee', error),
      });
    }
  }

}
