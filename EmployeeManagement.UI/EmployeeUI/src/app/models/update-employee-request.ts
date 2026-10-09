import { Department } from './department';

export interface UpdateEmployeeRequest {
  employeeId: number;
  name: string;
  email: string;
  department: Department;
  salary: number;
}
