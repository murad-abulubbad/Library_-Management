import { Injectable } from '@angular/core';
import { BaseCrudService } from './base-crud.service';
import { Category } from '../../features/categories/models/category.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseCrudService<Category> {

  constructor(http: HttpClient) {
    super(http);
  }

  override get endpoint(): string {
    return 'Categories';
  }

  
}
