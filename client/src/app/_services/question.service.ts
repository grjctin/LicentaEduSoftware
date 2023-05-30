import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Question } from '../_models/question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getQuestions() {
    return this.http.get<Question[]>(this.baseUrl + 'questions');
  }

  getQuestionsByCategoryDifficulty(categoryId: number, difficulty: number) {
    let params = new HttpParams()
      .set('categoryId', categoryId)
      .set('difficulty', difficulty);
    return this.http.get<Question[]>(this.baseUrl + 'questions/category-difficulty', {params})
  }
}
