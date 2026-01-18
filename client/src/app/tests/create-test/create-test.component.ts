import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { TestQuestion } from 'src/app/_models/testQuestion';
import { TestService } from 'src/app/_services/test.service';

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit{
  selectedIds : number[] = [];
  questionConfigs: TestQuestion[] = [];
  questionForm: FormGroup = new FormGroup({});
  testForm: FormGroup = new FormGroup({});
  answerTypes = [{value: 1, display: 'Multiple choice'}, {value: 2, display: 'Open answer'}];
  categories: Category[] = []

  constructor(private route: ActivatedRoute, private testService: TestService, private toastr : ToastrService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(
      params => {
        const idsArray = params['selectedIds']
        if(idsArray) {
          this.selectedIds = idsArray.split(',').map(Number);
        }
        const categoriesArray = params['categories']
        if(categoriesArray) {
          this.categories = JSON.parse(categoriesArray)
        }
      }
    )
     
    this.questionForm = new FormGroup({
      categoryId: new FormControl('',[Validators.required]),
      difficulty: new FormControl('',[Validators.required]),
      answerType: new FormControl('',[Validators.required]),
      numberOfQuestions: new FormControl('',[Validators.required]),
    })  

    this.testForm = new FormGroup({
      startDate: new FormControl('',[Validators.required]),
      duration: new FormControl('',[Validators.required])
    })
  }

  questionFormSubmit() {
    const questionConfig = {
      categoryId: this.questionForm.get('categoryId')?.value,
      difficulty: this.questionForm.get('difficulty')?.value,
      answerType: this.questionForm.get('answerType')?.value,
      numberOfQuestions: this.questionForm.get('numberOfQuestions')?.value
    };
    console.log(this.questionForm.value);
    console.log(this.getAnswerTypeDisplay(this.questionForm.get('answerType')?.value))
    this.questionConfigs.push(questionConfig);
  }

  testFormSubmit() {
    
    const testParams = {
      questionConfigurations: this.questionConfigs,
      studentIds: this.selectedIds,
      startDate: new Date(this.testForm.get('startDate')?.value),
      duration: this.testForm.get('duration')?.value
    }
    console.log(testParams);
    this.testService.createTests(testParams).subscribe({
      next: response => this.toastr.success(response)
    })
  }

  getCategoryName(id: number) {
    return this.categories.find(c => c.id == id)?.name;
  }

  getAnswerTypeDisplay(value:number) {
    return this.answerTypes.find(a => a.value == value)?.display;
  }
  
}
