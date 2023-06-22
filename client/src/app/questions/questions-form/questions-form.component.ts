import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-questions-form',
  templateUrl: './questions-form.component.html',
  styleUrls: ['./questions-form.component.css']
})
export class QuestionsFormComponent implements OnInit {
  @Input() categories: Category[] = [];
  @Output() removeForm = new EventEmitter<void>();
  @Output() questionAdded = new EventEmitter<void>();
  questionForm: FormGroup = new FormGroup({})
  answersForm: FormGroup = new FormGroup({})
  answerTypes = [{value: 1, display: 'Multiple choice'}, {value: 2, display: 'Open answer'}];
  multipleChoiceSelected = false;

  constructor (private questionService: QuestionService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.initializeQuestionsForm();
  }

  initializeQuestionsForm() {
    this.questionForm = new FormGroup({
      categoryId: new FormControl('',[Validators.required]),
      difficulty: new FormControl('',[Validators.required]),
      answerType: new FormControl('',[Validators.required]),
      text: new FormControl('',[Validators.required])      
    });

    this.answersForm = new FormGroup({
      correctAnswer: new FormControl('', [Validators.required]),
      answer2: new FormControl('', [Validators.required]),
      answer3: new FormControl('', [Validators.required]),
      answer4: new FormControl('', [Validators.required])
    })

    this.questionForm.get('answerType')?.valueChanges.subscribe({
      next: value => {
        if (value === '1') 
          this.addAnswersForm();      
        else 
          this.removeAnswersForm();
    }
    });
  }

  addAnswersForm() {
    this.multipleChoiceSelected = true;
    this.questionForm.addControl('answers', this.answersForm);
  }

  removeAnswersForm() {
    this.multipleChoiceSelected = false;
    this.questionForm.removeControl('answers');
  }

  addQuestion() {
    console.log(this.questionForm.value);
    if(this.questionForm.get('answerType')?.value === '1')
    {
      const question = {
        categoryId: this.questionForm.get('categoryId')?.value,
        difficulty: this.questionForm.get('difficulty')?.value,
        answerType: this.questionForm.get('answerType')?.value,
        text: this.questionForm.get('text')?.value,
        correctAnswer: this.answersForm.get('correctAnswer')?.value,
        answer2: this.answersForm.get('answer2')?.value,
        answer3: this.answersForm.get('answer3')?.value,
        answer4: this.answersForm.get('answer4')?.value
      }
      this.questionService.addQuestion(question).subscribe({
        next: _ => this.toastr.success('Question added successfully')
      });
    }
    else 
    {
      const question = {
        categoryId: this.questionForm.get('categoryId')?.value,
        difficulty: this.questionForm.get('difficulty')?.value,
        answerType: this.questionForm.get('answerType')?.value,
        text: this.questionForm.get('text')?.value
      }
      this.questionService.addQuestion(question).subscribe({
        next: _ => this.toastr.success('Question added successfully')
      });
    }
    
    this.questionAdded.emit();
  }
  
  cancelAdd() {
    console.log("cancelAdd");
    console.log(this.questionForm.value);
    this.removeForm.emit();
  }
}

