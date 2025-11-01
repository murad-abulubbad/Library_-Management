import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class BaseCrudService<T> {
  protected baseUrl = 'https://localhost:7175/api';

  constructor(protected http: HttpClient) {}

  protected abstract get endpoint(): string;
  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${this.endpoint}`);
  }

  add(item: T): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.endpoint}`, item);
  }

  update(item: T & { id: number }): Observable<any> {
    return this.http.put(`${this.baseUrl}/${this.endpoint}/${item.id}`, item);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${this.endpoint}/${id}`);
  }
}
