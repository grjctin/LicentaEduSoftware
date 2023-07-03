import { Component, Input } from '@angular/core';
import { QuizQuestion } from 'src/app/_models/QuizQuestion';

@Component({
  selector: 'app-test-question',
  templateUrl: './test-question.component.html',
  styleUrls: ['./test-question.component.css']
})
export class TestQuestionComponent {
  @Input() question: QuizQuestion | undefined;
}
