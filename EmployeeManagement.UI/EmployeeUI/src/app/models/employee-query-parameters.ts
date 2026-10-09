import { Department } from './department';

export interface EmployeeQueryParameters {
  pageNumber: number;
  pageSize: number;
  search: string | null;
  department: Department | null;
  sortBy: string | null;
  sortOrder: string | null;
}
