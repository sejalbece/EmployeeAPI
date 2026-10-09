import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ResponseEmployee } from '../models/response-employee';
import { EmployeeQueryParameters } from '../models/employee-query-parameters';
import { PageResponse } from '../models/page-response';
import { CreateEmployeeRequest } from '../models/create-employee-request';
import { UpdateEmployeeRequest } from '../models/update-employee-request';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiUrl = `${environment.apiUrl}/Employee`;

  constructor(private http: HttpClient) {}

  getEmployee(params: EmployeeQueryParameters) {
    let httpParams = new HttpParams()
      .set('pageNumber', params.pageNumber)
      .set('pageSize', params.pageSize);

    if (params.search) {
      httpParams = httpParams.set('search', params.search);
    }
    if (params.department) {
      httpParams = httpParams.set('department', params.department);
    }

    if (params.sortBy) {
      httpParams = httpParams.set('sortBy', params.sortBy);
    }
    if (params.sortOrder) {
      httpParams = httpParams.set('sortOrder', params.sortOrder);
    }

    return this.http.get<PageResponse>(this.apiUrl, {
      params: httpParams,
    });
  }

  getEmployeeById(id: number) {
    return this.http.get<ResponseEmployee>(`${this.apiUrl}/${id}`);
  }

  createEmployee(request: CreateEmployeeRequest) {
    return this.http.post<ResponseEmployee>(this.apiUrl, request);
  }

  updateEmployee(updateRequest: UpdateEmployeeRequest) {
    return this.http.put<ResponseEmployee>(this.apiUrl, updateRequest);
  }

  deleteEmployee(id: number) {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
