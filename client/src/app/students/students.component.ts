import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { StudentService } from '../_services/student.service';
import { Student } from '../_models/student';
import { SchoolClass } from '../_models/class';
import { CategoryService } from '../_services/category.service';
import { StudentsGrades } from '../_models/studentGrades';
import { Category } from '../_models/category';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit, OnChanges {
  @Input() schoolClass: SchoolClass | undefined;
  @Input() mode: string | undefined;
  students: Student[] = [];
  studentsGrades: StudentsGrades[] = [];
  categories: Category[] = [];
  addGradeMode = "false";
  showAddGradeForm: boolean = false;



  constructor(private studentService: StudentService, private categoryService: CategoryService) { }


  ngOnChanges(changes: SimpleChanges): void {
    console.log("ngOnChanges");
    if (changes['schoolClass']) {
      //Cand se schimba clasa, reincarc studentii si categoriile aferente
      console.log("selected class changed");
      this.loadStudentsByClass();
      //this.studentsList.emit(this.students); //Trimit spre componenta classes lista actualizata
      if(this.schoolClass)
        this.loadCategoriesByClassNumber(this.schoolClass?.classNumber);
    }
    if (changes['mode']) {
      console.log("view mode changed");
    }
  }

  ngOnInit(): void {
    this.loadStudents();
    this.loadCategoriesByClassNumber(9);
  }

  loadCategoriesByClassNumber(nr: number){
    this.categoryService.getCategoriesByClassNumber(nr).subscribe({
      next: categories => {
         this.categories = categories;
      }
    })
  }

  getCategoryNameById(id: number){
    if(this.categories) {
      const cat = this.categories.filter(category => category.id === id);
      if (cat.length > 0) {
        console.log(cat[0].name)
        return cat[0].name;
      }
      else
        console.log("category not found");
    }    
    return "category not found";
  }


  loadStudentsByClass() {
    console.log("reloading students");
    if (this.schoolClass) {
      this.studentService.getStudentsDetailsByClassId(this.schoolClass.id).subscribe({
        next: s => this.students = s
      })
      this.studentService.getStudentGradesByClassId(this.schoolClass.id).subscribe({
        next: s => this.studentsGrades = s
      })
    }
  }

  loadStudents() {
    if (!this.schoolClass) {
      this.studentService.getStudents().subscribe({
        next: s => this.students = s
      })
    }
  }

  toggleAddGradeMode(mode: string) {
    this.addGradeMode = mode;
    this.showAddGradeForm = true;
  }

  hideComponent() {
    this.addGradeMode = "false";
    this.showAddGradeForm = false;
  }

  gradeAdded() {
    //reactualizez notele studentilor
    //ascund formularul pentru adaugare note
    this.loadStudentsByClass();
    this.hideComponent();
  }

}
