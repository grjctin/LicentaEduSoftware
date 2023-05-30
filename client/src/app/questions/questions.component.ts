import { Component, Input, OnInit } from '@angular/core';
import { QuestionService } from '../_services/question.service';
import { Question } from '../_models/question';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {
  questions: Question[] = [];
  @Input() categoryId : number | undefined;
  @Input() difficulty : number | undefined;

  constructor(private questionService: QuestionService) { }

  ngOnInit(): void {
    this.loadQuestions();
    this.getQuestionsByCategoryDifficulty(2, 1);
  }

  loadQuestions() {
    this.questionService.getQuestions().subscribe({
      next: response => {
        this.questions = response;
      }
    })
  }

  getQuestionsByCategoryDifficulty(categoryId: number, difficulty: number) {
    if(this.categoryId && this.difficulty){
      this.questionService.getQuestionsByCategoryDifficulty(this.categoryId, this.difficulty).subscribe({
        next: response => {
          //this.questions = response
          response.forEach(q => this.questions.push(q))
        }
      })
    }
    
  }
}
