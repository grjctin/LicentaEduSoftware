import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Question } from '../_models/question';
import { QuestionParams } from '../_models/questionParams';
import { map, of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  baseUrl = environment.apiUrl;
  questionParams: QuestionParams | undefined;
  questions: Question[] = [];
  questionsCache = new Map();

  constructor(private http: HttpClient) {
    this.questionParams = new QuestionParams();
   }

  getQuestions() {
    return this.http.get<Question[]>(this.baseUrl + 'questions');
  }

  getQuestionsByCategoryDifficulty(categoryId: number, difficulty: number) {
    let params = new HttpParams()
      .set('categoryId', categoryId)
      .set('difficulty', difficulty);
    return this.http.get<Question[]>(this.baseUrl + 'questions/category-difficulty', { params })
  }

  getPaginatedQuestions(questionParams: QuestionParams) {
    //Cheia din map va fi o insiruire de prop despartite de "-"
    const response = this.questionsCache.get(Object.values(questionParams).join('-'));

    if(response) return of(response);

    let params = getPaginationHeaders(questionParams.pageNumber, questionParams.pageSize);
    console.log("difficulty = " + questionParams.difficulty);
    params = params.append('categoryId', questionParams.categoryId);
    params = params.append('answerType', questionParams.answerType);
    params = params.append('difficulty', questionParams.difficulty);
    params = params.append('orderBy', questionParams.orderBy);

    return getPaginatedResult<Question []>(this.baseUrl + 'questions/paginated', params, this.http).pipe(
      map(response => {
        this.questionsCache.set(Object.values(questionParams).join('-'), response);
        return response;
      })
    )
  }

  getQuestionParams() {
    return this.questionParams;
  }

  setQuestionParams(params: QuestionParams) {
    this.questionParams = params;
  }

  resetQuestionParams() {
    this.questionParams = new QuestionParams();
    return this.questionParams;
  }
}
