import { Component, OnInit } from '@angular/core';
import { SchoolClassService } from '../_services/school-class.service';
import { SchoolClass } from '../_models/class';

@Component({
  selector: 'app-classes',
  templateUrl: './classes.component.html',
  styleUrls: ['./classes.component.css']
})
export class ClassesComponent implements OnInit {
  classes: SchoolClass[] = [];
  students: any[] = [];
  selectedClass: SchoolClass | undefined;
  viewMode: string = "details"; //details pt student details, grades pt student grades + category grades

  constructor(private classService: SchoolClassService) {}

  ngOnInit(): void {
    //this.loadAllClasses();
    this.loadProfessorClasses();
  }

  loadAllClasses() {
    // this.classService.getAllClasses().subscribe({
    //   next: response => this.classes = response
    // })
  }

  loadProfessorClasses() {
    this.classService.getProfessorClasses().subscribe({
      next: response => this.classes = response
    })
  }

  onClassSelection(schoolClass : SchoolClass) {
    console.log("onClassSelection     changing selected class");
    this.selectedClass = schoolClass;    
  }

  changeViewMode(viewMode: string) {
    this.viewMode = viewMode;
  }

}
