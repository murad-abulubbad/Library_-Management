import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../core/services/book.service';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../../categories/models/category.model';
import { Book } from '../models/book.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-form',
  standalone: true,
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class BookFormComponent implements OnInit {
  form!: FormGroup;
  categories: Category[] = [];
  bookId?: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private bookService: BookService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      year: [null],
      categoryIds: [[]],
    });

    this.categoryService.getAll().subscribe((res) => (this.categories = res));

    this.bookId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.bookId) {
      this.bookService.getById(this.bookId).subscribe((res) => {
        this.form.patchValue({
          title: res.title,
          author: res.author,
          year: res.year,
          categoryIds: res.categoryIds,    
        });
      });
    }
  }
cancel(): void {
  this.router.navigate(['/books']);
}

  save(): void {
   
    const book: Book = this.form.value;

    if (this.bookId) {
      book.id = this.bookId;
      this.bookService.update(book).subscribe(() => this.router.navigate(['/books']));
    } else {
      this.bookService.add(book).subscribe(() => this.router.navigate(['/books']));
    }
  }
}
