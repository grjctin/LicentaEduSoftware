import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Student } from '../_models/student';
import { SchoolClass } from '../_models/class';
import { SchoolClassService } from './school-class.service';
import { map } from 'rxjs';
import { StudentsGrades } from '../_models/studentGrades';
import { StudentAttendance } from '../_models/studentAttendance';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = environment.apiUrl;
  students: Student[] = [];
  classes: SchoolClass[] = []
  studentsCache = new Map(); //kvp, key = clasa, value = lista studenti


  constructor(private http: HttpClient, private classService: SchoolClassService) { }

  // loadStudents() {
  //   this.classService.getAllClasses().subscribe({
  //     next: response => this.classes = response
  //   })

  //   if(this.classes) {
  //     this.classes.forEach( c => {
  //       this.http.get(this.baseUrl + 'students/classId=' + c.id).pipe(
  //         map( response => {
  //           this.studentsCache.set(c.id, response);
  //           //return response;
  //         })
  //       )
  //     })
  //   }
  //   console.log(this.studentsCache);    
  // }

  getStudents() {
    return this.http.get<Student[]>(this.baseUrl + 'students');
  }

  getStudentsDetailsByClassId(id: number) {
    return this.http.get<Student[]>(this.baseUrl + 'students/details/classId=' + id);
  }

  getStudentGradesByClassId(id: number) {
    return this.http.get<StudentsGrades[]>(this.baseUrl + 'students/grades/classId=' + id);
  }

  getStudentAttendanceByClassId(id: number) {
    return this.http.get<StudentAttendance[]>(this.baseUrl + 'students/attendance/classId=' + id);
  }

  addAbsences(studentIds: number[]) {
    return this.http.post(this.baseUrl + 'students/add-attendance', studentIds, {responseType: 'text'});
  }
}
