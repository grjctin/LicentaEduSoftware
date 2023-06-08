import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { StudentService } from '../_services/student.service';
import { Student } from '../_models/student';
import { SchoolClass } from '../_models/class';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit, OnChanges {
  @Input() schoolClass: SchoolClass | undefined;
  @Input() mode: string | undefined;
  students: Student[] = [];


  constructor(private studentService: StudentService) { }


  ngOnChanges(changes: SimpleChanges): void {
    console.log("ngOnChanges");
    if (changes['schoolClass']) {
      console.log("selected class changed");
      this.loadStudentsByClass();
    }
    if (changes['mode']) {
      console.log("view mode changed");
    }
  }

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudentsByClass() {
    console.log("reloading students");
    if (this.schoolClass) {
      this.studentService.getStudentsByClassId(this.schoolClass.id).subscribe({
        next: s => this.students = s
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



}
