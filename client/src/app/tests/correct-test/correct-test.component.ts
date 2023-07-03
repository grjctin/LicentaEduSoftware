import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Test } from 'src/app/_models/Test';
import { TestCorrect } from 'src/app/_models/testCorrect';
import { TestService } from 'src/app/_services/test.service';

@Component({
  selector: 'app-correct-test',
  templateUrl: './correct-test.component.html',
  styleUrls: ['./correct-test.component.css']
})
export class CorrectTestComponent implements OnInit, OnChanges {
  @Input() testId: number | undefined;
  test: Test | undefined;
  testAnswers: {
    questionNumber: number;
    givenAnswer: string;
  } [] = [];
  testCorrect: TestCorrect |undefined;

  ngOnInit(): void {
    this.loadTest();
    this.initialize();
  }

  constructor(private testService: TestService, private toastr: ToastrService) { }

  initialize(){
    if(this.test) {
      this.test.questions.forEach((question) => {
        this.testAnswers.push({questionNumber: question.questionNumber, givenAnswer: ''});
      })
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.loadTest();
  }

  async loadTest() {
    if (this.testId) {
      // this.testService.getTest(this.testId).subscribe({
      //   next: response => {
      //     this.test = response;
      //   }
      // });
      const response = await this.testService.getTest(this.testId).toPromise();
      this.test = response;
      this.testAnswers = [];
      this.initialize();
    }
  }

  collectAnswers(){
    if(this.testId){
      this.testCorrect = {
        testId: this.testId,
        answers: this.testAnswers,
      }
    }

    console.log(this.testCorrect);
    
    if(this.testCorrect)
    {
      this.testService.correctTest(this.testCorrect).subscribe({
        next: response => this.toastr.success(response)
      })
    }
    
    //trimit request cu obiectul this.testCorrect catre server
    
  }
}
