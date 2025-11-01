import { Routes } from '@angular/router';
import { BookListComponent } from './features/books/components/book-list.component';
import { CategoryListComponent } from './features/categories/components/category-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'books', pathMatch: 'full' },
  { path: 'books', component: BookListComponent },
  {
    path: 'books/add',
    loadComponent: () =>
      import('./features/books/components/book-form.component').then(m => m.BookFormComponent),
  },
  {
    path: 'books/edit/:id',
    loadComponent: () =>
      import('./features/books/components/book-form.component').then(m => m.BookFormComponent),
  },

  { path: 'categories', component: CategoryListComponent },
  {
    path: 'categories/add',
    loadComponent: () =>
      import('./features/categories/components/category-form.component').then((m) => m.CategoryFormComponent),
  },
  {
    path: 'categories/edit/:id',
    loadComponent: () =>
      import('./features/categories/components/category-form.component').then((m) => m.CategoryFormComponent),
  },
];
