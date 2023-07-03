import { Component, OnInit } from '@angular/core';
import { TestService } from '../_services/test.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent {
  mode: string = "view";
  selectedTestId: number = 0;

  constructor(private testService: TestService, private toastr: ToastrService) { };
  

  changeMode(value: string) {
    this.mode = value;
  }

  selectedTestChanged(){}

}
