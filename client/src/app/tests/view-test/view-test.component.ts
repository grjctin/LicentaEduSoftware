import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Test } from 'src/app/_models/Test';
import { TestService } from 'src/app/_services/test.service';

@Component({
  selector: 'app-view-test',
  templateUrl: './view-test.component.html',
  styleUrls: ['./view-test.component.css']
})
export class ViewTestComponent implements OnInit, OnChanges {
  @Input() testId: number | undefined;
  test: Test | undefined;

  ngOnInit(): void {
    this.loadTest();    
  }

  constructor(private testService: TestService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.loadTest();  
  }

  loadTest() {
    if (this.testId) {
      this.testService.getTest(this.testId).subscribe({
        next: response => {
          this.test = response;
        }
      });
    }
  }

}
