import { Component, Input } from '@angular/core';
import { Student } from 'src/app/_models/student';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent {
  @Input() student: Student | undefined;
  @Input() className: string | undefined;
}
