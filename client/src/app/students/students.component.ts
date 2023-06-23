import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { StudentService } from '../_services/student.service';
import { Student } from '../_models/student';
import { SchoolClass } from '../_models/class';
import { CategoryService } from '../_services/category.service';
import { StudentsGrades } from '../_models/studentGrades';
import { Category } from '../_models/category';
import { StudentAttendance } from '../_models/studentAttendance';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

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
  studentsAttendance: StudentAttendance[] = [];
  categories: Category[] = [];
  addGradeMode = "false";
  showAddGradeForm: boolean = false;
  attendanceMode: boolean = false;
  selectedIds: number[] = [];
  testMode = false;



  constructor(private studentService: StudentService, private categoryService: CategoryService, private toastr: ToastrService, private router: Router) { }


  ngOnChanges(changes: SimpleChanges): void {
    console.log("ngOnChanges");
    if (changes['schoolClass']) {
      //Cand se schimba clasa, reincarc studentii si categoriile aferente
      console.log("selected class changed");
      this.loadStudentsByClass();
      this.selectedIds = [];
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
      this.studentService.getStudentAttendanceByClassId(this.schoolClass.id).subscribe({
        next: s => this.studentsAttendance = s
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

  toggleAttendanceMode(value: boolean) {
    this.attendanceMode = value;
    this.selectedIds = [];
  }

  toggleSelectedId(id: number) {
    const index = this.selectedIds.indexOf(id);
    if (index > -1) {
      this.selectedIds.splice(index, 1);
    } else {
      this.selectedIds.push(id);
    }
  }

  isSelected(id: number): boolean {
    return this.selectedIds.includes(id);
  }

  addAbsences() {
    //request adaugare
    this.studentService.addAbsences(this.selectedIds).subscribe({
      next: _ => this.toastr.success('Absents added')
    })
    //actualizare
    this.loadStudentsByClass();
    //golire selectedIds
    this.selectedIds = [];
    //ascundere butoane
    this.attendanceMode = false;
  }

  toggleTestMode(value: boolean) {
    this.testMode = true;
  }

  createTests() {
    this.testMode = false;
    //de aici trebuie sa merg in create test component si sa trimit selectedIds ca parametru
    const categoriesArray = JSON.stringify(this.categories);
    const params = {selectedIds: this.selectedIds.join(','), categories: categoriesArray}
    //this.router.navigate(['/create-test'], {queryParams: {selectedIds: this.selectedIds.join(',')}});
    this.router.navigate(['/create-test'], {queryParams: params});
    console.log(this.selectedIds);
    this.selectedIds = [];
  }

  cancelCreateTests() {
    this.testMode = false;
    this.selectedIds = [];
  }

  studentIdsContains(id: number): boolean {
    return this.selectedIds.includes(id);
  }

}
