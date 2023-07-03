import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Test } from '../_models/Test';
import { TestCorrect } from '../_models/testCorrect';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createTests(testParams: any) {
    return this.http.post(this.baseUrl + 'tests', testParams, { responseType: 'text' });
  }

  getTest(testId: number) {
    return this.http.get<Test>(this.baseUrl + 'tests/testId=' + testId);
  }

  correctTest(testCorrect: TestCorrect) {
    return this.http.post(this.baseUrl + 'tests/correct', testCorrect, { responseType: 'text'});
  }

}
