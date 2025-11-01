import { Injectable } from '@angular/core';
import { BaseCrudService } from './base-crud.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../../features/books/models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService extends BaseCrudService<Book> {

  constructor(http: HttpClient) {
    super(http);
  }

  override get endpoint(): string {
    return 'Books';
  }
 
  getById(id: number): Observable<Book> {
  return this.http.get<Book>(`${this.baseUrl}/${this.endpoint}/${id}`);
}

getAllWithCategoriesAdo(): Observable<Book[]> {
  return this.http.get<Book[]>(`${this.baseUrl}/${this.endpoint}/with-categories-ado`);
}


}
