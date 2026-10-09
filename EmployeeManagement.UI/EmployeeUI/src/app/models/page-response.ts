import { ResponseEmployee } from './response-employee';

export interface PageResponse {
  data: ResponseEmployee[];
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
}
