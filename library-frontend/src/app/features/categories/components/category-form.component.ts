import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-category-form',
  standalone: true,
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class CategoryFormComponent implements OnInit {
  form!: FormGroup;
  categoryId?: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
    });

    this.categoryId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.categoryId) {
      this.categoryService.getAll().subscribe({
        next: (res) => {
          const category = res.find(c => c.id === this.categoryId);
          if (category) {
            this.form.patchValue(category);
          }
        },
        error: (err) => console.error('Error loading categories:', err)
      });
    }
  }
cancel(): void {
  this.router.navigate(['/categories']);
}
  save(): void {
    if (this.form.invalid) {
    alert('⚠️ Please enter a category name before saving.');
    return;
  }
    const category: Category = this.form.value;

    if (this.categoryId) {
      category.id = this.categoryId;
      this.categoryService.update(category).subscribe(() => this.router.navigate(['/categories']));
    } else {
      this.categoryService.add(category).subscribe(() => this.router.navigate(['/categories']));
    }
  }
}
