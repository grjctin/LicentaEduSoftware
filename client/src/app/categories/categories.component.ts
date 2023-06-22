import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/category';
import { environment } from 'src/environments/environment';
import { CategoryService } from '../_services/category.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  //baseUrl = environment.apiUrl
  categories: Category[] = [];
  classNumbers = [9,10,11,12];
  selectedClass: number | undefined;
  categoryForm: FormGroup = new FormGroup({}); //formul pentru adaugare categorie
  addCategoryMode = false;

  constructor(private categoryService : CategoryService, private toastr: ToastrService) {}


  ngOnInit(): void {
    this.loadCategories();
    //this.initializeForm();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: response => {
        this.categories = response;
      }
    });
  }

  loadCategoriesByClass(classNumber: number) {
    this.selectedClass = classNumber;
    this.categoryService.getCategoriesByClassNumber(classNumber).subscribe({
      next: response => this.categories = response
    });
  }

  initializeForm(){
    this.categoryForm = new FormGroup({
      classNumber: new FormControl('', [Validators.required]),
      name: new FormControl('',[Validators.required])
    });    
  }

  showForm(){
    this.initializeForm();
    this.addCategoryMode = true;
  }

  addCategory(){
    var classNumber = this.categoryForm.get('classNumber')?.value;
    var name = this.categoryForm.get('name')?.value;
    //console.log(classNumber + " " + name);
    this.categoryService.addCategory(classNumber, name).subscribe({
      next: () => {
        this.toastr.success('Category added successfully');
      }
    })
    this.loadCategories();
    this.addCategoryMode=false;
  }

  cancelAdd(){
    this.categoryForm.reset();
    this.addCategoryMode = false;
  }
}
