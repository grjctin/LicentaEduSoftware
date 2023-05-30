import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/category';
import { environment } from 'src/environments/environment';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  baseUrl = environment.apiUrl
  categories: Category[] = [];
  //exemplu comunicare parent to child
  categoryId = 2;
  difficulty = 1;

  constructor(private http: HttpClient, private categoryService : CategoryService) {}


  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: response => {
        this.categories = response;
      }
    });
  }



}
