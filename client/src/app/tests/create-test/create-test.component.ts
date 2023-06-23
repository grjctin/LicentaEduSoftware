import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private route: ActivatedRoute, private testService: TestService) {}

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
    this.categories.forEach(c => console.log(c.name));
    this.selectedIds.forEach(id => console.log(id));
     
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
    this.questionConfigs.push(questionConfig);
  }

  testFormSubmit() {
    const testParams = {
      questionConfigurations: this.questionConfigs,
      studentIds: this.selectedIds,
      startDate: this.testForm.get('startDate')?.value,
      duration: this.testForm.get('duration')?.value
    }
    console.log(testParams);
    //request catre testService sa creeze testul
  }

  getCategoryName(id: number) {
    return this.categories.find(c => c.id == id)?.name;
  }

  getAnswerTypeDisplay(value:number) {
    return this.answerTypes.find(a => a.value = value)?.display;
  }
  
}
