import { Component, Input, OnInit } from '@angular/core';
import { QuestionService } from '../_services/question.service';
import { Question } from '../_models/question';
import { Pagination } from '../_models/pagination';
import { QuestionParams } from '../_models/questionParams';
import { CategoryService } from '../_services/category.service';
import { Category } from '../_models/category';
import { Answer } from '../_models/answer';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {
  //@Input() categoryId : number | undefined;
  //@Input() difficulty : number | undefined;
  questions: Question[] = [];
  answers: Answer[] = [];
  pagination: Pagination | undefined;
  questionParams: QuestionParams | undefined;
  difficultiesList = [1, 2, 3];
  answerTypeList = [{ value: 1, display: 'Multiple choice' }, { value: 2, display: 'Open answer' }];
  categories: Category[] = [];
  addQuestionMode = false;

  constructor(private questionService: QuestionService, private categoryService: CategoryService) {
    this.questionParams = this.questionService.getQuestionParams();
  }

  ngOnInit(): void {
    this.loadCategories();
    //this.loadQuestions();
    //this.getQuestionsByCategoryDifficulty(2, 1);
    this.getPaginatedQuestions();
  }

  loadQuestions() {
    this.questionService.getQuestions().subscribe({
      next: response => {
        this.questions = response;
      }
    })
  }

  getQuestionsByCategoryDifficulty(categoryId: number, difficulty: number) {
    this.questionService.getQuestionsByCategoryDifficulty(categoryId, difficulty).subscribe({
      next: response => {
        //this.questions = response
        response.forEach(q => this.questions.push(q))
      }
    })
  }


  getPaginatedQuestions() {
    console.log("getPaginatedQuestions component");
    if (this.questionParams) {
      console.log("getPaginatedQuestions component after if");
      this.questionService.setQuestionParams(this.questionParams);
      this.questionService.getPaginatedQuestions(this.questionParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.questions = response.result.questions;
            this.answers = response.result.answers;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  resetFilters() {
    this.questionParams = this.questionService.resetQuestionParams();
    this.getPaginatedQuestions();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: response => {
        this.categories = response;
      }
    });
  }

  getAnswerTypeDisplay(answerType: number) {
    const display = this.answerTypeList.find(option => option.value == answerType)?.display;
    return display;
  }

  showForm() {
    this.addQuestionMode = true;
  }

  hideForm() {
    this.addQuestionMode = false;
  }

  questionAdded() {
    this.getPaginatedQuestions();
    this.hideForm();
  }

  pageChanged(event: any) {
    if (this.questionParams) {
      if (this.questionParams?.pageNumber !== event.page) {
        this.questionParams.pageNumber = event.page;
        this.getPaginatedQuestions();
      }
    }
  }

  getCorrectAnswer(questionId: number) {
    if (questionId >= 9) {
      const correctAnswer = this.answers.find(a => a.isCorrect && a.questionId === questionId);
      return correctAnswer ? correctAnswer.text : '';
    }
    return null;
  }

  getAnswer(questionId: number, index: number): string | undefined {
    if (questionId >= 9) {
      const questionAnswers = this.answers.filter(a => a.questionId === questionId && !a.isCorrect);
      return questionAnswers[index].text;
    }
    return '';
  }

  getCategoryName(categoryId: number) {
    const category = this.categories.find(c => c.id == categoryId)
    return category ? category.name : '';
  }
}
