import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }

  getCategoriesByClassNumber(classNumber: number) {
    return this.http.get<Category[]>(this.baseUrl + 'categories/' + classNumber);
  }

  addCategory(classNumber: number, name: string) {
    console.log(classNumber + " " + name);
    return this.http.post(this.baseUrl + 'categories', {classNumber, categoryName: name},{responseType: 'text'});    
  }

}
