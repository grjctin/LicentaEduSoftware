import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GradesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addGrade(studentId: number, grade: number) {
    return this.http.post(this.baseUrl + 'grades', {studentId, grade});
  }

  addCategoryGrade(studentId: number, categoryId: number, grade: number) {
    return this.http.post(this.baseUrl + 'grades/categoryGrades', {studentId, categoryId, grade}, {responseType: 'text'});
  }
}
