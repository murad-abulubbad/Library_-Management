import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../core/services/book.service';
import { Book } from '../models/book.model';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule]
})
export class BookListComponent implements OnInit {
  
  books: Book[] = [];
  displayedColumns: string[] = ['title', 'author', 'year', 'actions', 'categories'];
  

  constructor(private bookService: BookService, private router: Router) {}

  ngOnInit(): void {
    this.loadEf();    
  }
  loadEf(): void {
    this.bookService.getAll().subscribe({
      next: (res) => {
        this.books = res;
    
      },
      error: (err) => console.error(err)
    });
  }

  //  ADO.NET 
  loadAdo(): void {
    this.bookService.getAllWithCategoriesAdo().subscribe({
      next: (res) => {
        this.books = res;
      },
      error: (err) => console.error(err)
    });
  }

  deleteBook(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.bookService.delete(id).subscribe(() => this.loadEf());
    }
  }

  editBook(id: number): void {
    this.router.navigate(['/books/edit', id]);
  }

  addBook(): void {
    this.router.navigate(['/books/add']);
  }

  // ADO
  joinCategoryNames(b: Book): string {
    const cats = b?.categories ?? [];
    return cats.length ? cats.map(c => c.name).join(', ') : '—';
  }
}
