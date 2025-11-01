import { Category } from "../../categories/models/category.model";

export class Book {
  id!: number;
  title!: string;
  author!: string;
  year?: number;
  categoryIds!: number[];
  categories !: Category[];
}