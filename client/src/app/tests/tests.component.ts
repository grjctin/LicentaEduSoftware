import { Component, OnInit } from '@angular/core';
import { TestService } from '../_services/test.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit {
  mode: string = "";

  constructor(private testService: TestService, private toastr: ToastrService) { };
  
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  changeMode(value: string) {
    this.mode = value;
  }

}
