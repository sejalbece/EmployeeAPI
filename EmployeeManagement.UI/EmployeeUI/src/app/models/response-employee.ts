import { Department } from './department';

export interface ResponseEmployee {
  employeeId: number;
  name: string;
  email: string;
  department: Department;
  salary: number;
  createdDate: string;
}
