import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Category } from '../_models/category';
import { Student } from '../_models/student';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GradesService } from '../_services/grades.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-grade',
  templateUrl: './add-grade.component.html',
  styleUrls: ['./add-grade.component.css']
})
export class AddGradeComponent implements OnInit {
  @Input() categories: Category[] = [];
  @Input() students: Student[] = [];
  @Input() mode = "false";  //false pt form inactive, addGrade pt adaugare nota, addCategoryGrade pt adaugare categoryGrade
  gradeForm: FormGroup = new FormGroup({})
  @Output() removeForm = new EventEmitter<void>();
  @Output() reloadStudentGrades = new EventEmitter<void>();

  constructor(private gradesService: GradesService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeAddGradeForm();
  }

  initializeAddGradeForm() {
    if (this.mode === "addCategoryGrade") {
      this.gradeForm = new FormGroup({
        studentId: new FormControl('', [Validators.required]),
        categoryId: new FormControl('', [Validators.required]),
        grade: new FormControl('', [Validators.required])
      });
    }
    else if (this.mode === "addGrade") {
      this.gradeForm = new FormGroup({
        studentId: new FormControl('', [Validators.required]),
        grade: new FormControl('', [Validators.required])
      });
    }
  }

  showForm(mode: string) {
    this.mode = mode;
    this.initializeAddGradeForm();
  }

  addGrade() {
    console.log("add grade" + this.gradeForm.get('studentId')?.value + this.gradeForm.get('categoryId')?.value + this.gradeForm.get('grade')?.value);
    
    var studentId = this.gradeForm.get('studentId')?.value;
    console.log("second method student id = " + studentId);

    var grade = this.gradeForm.get('grade')?.value;
    
    if (this.mode === "addCategoryGrade") {
      var categoryId = this.gradeForm.get('categoryId')?.value;
      this.gradesService.addCategoryGrade(studentId, categoryId, grade).subscribe({
        next: _ => this.toastr.success('Category Grade added successfully')
      })
    }
    else if (this.mode === "addGrade") {
      this.gradesService.addGrade(studentId, grade).subscribe({
        next: _ => this.toastr.success('Grade added successfully')
      })
    }

    this.reloadStudentGrades.emit();
  }

  cancelAdd() {
    //emit un event catre parent component pentru a elimina formularul de adaugare note
    this.removeForm.emit();
  }
}
