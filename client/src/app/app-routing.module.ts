import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { CategoriesComponent } from './categories/categories.component';
import { ClassesComponent } from './classes/classes.component';
import { StudentsComponent } from './students/students.component';
import { QuestionsComponent } from './questions/questions.component';
import { TestsComponent } from './tests/tests.component';
import { CreateTestComponent } from './tests/create-test/create-test.component';
import { ViewTestComponent } from './tests/view-test/view-test.component';
import { CorrectTestComponent } from './tests/correct-test/correct-test.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    //first match wins, de sus in jos
    children: [
      {path: 'categories', component: CategoriesComponent},
      {path: 'classes', component: ClassesComponent},
      {path: 'questions', component: QuestionsComponent},
      {path: 'tests', component: TestsComponent},
      {path: 'create-test', component: CreateTestComponent},
      {path: 'view-test',component: ViewTestComponent},
      {path: 'correct-test',component: CorrectTestComponent}

    ]
  }, 
  {path: 'errors', component: TestErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'} /*cazul pt ruta invalida*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
