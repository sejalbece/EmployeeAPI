import { Department } from './department';

export interface CreateEmployeeRequest {
  name: string;
  email: string;
  department: Department;
  salary: number;
}
