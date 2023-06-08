import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SchoolClass } from '../_models/class';

@Injectable({
  providedIn: 'root'
})
export class SchoolClassService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllClasses() {
    return this.http.get<SchoolClass[]>(this.baseUrl + 'schoolClass');
  }

  getProfessorClasses() {
    return this.http.get<SchoolClass[]>(this.baseUrl + 'schoolClass/professorId');
  }
}
