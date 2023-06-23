import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MemberDetailedResolver } from './_resolvers/member-detailed.resolver';
import { CategoriesComponent } from './categories/categories.component';
import { ClassesComponent } from './classes/classes.component';
import { StudentsComponent } from './students/students.component';
import { QuestionsComponent } from './questions/questions.component';
import { TestsComponent } from './tests/tests.component';
import { CreateTestComponent } from './tests/create-test/create-test.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    //first match wins, de sus in jos
    children: [
      {path: 'members', component: MemberListComponent},
      {path: 'members/:username', component: MemberDetailComponent, resolve: {member: MemberDetailedResolver}},
      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'categories', component: CategoriesComponent},
      {path: 'classes', component: ClassesComponent},
      {path: 'questions', component: QuestionsComponent},
      {path: 'tests', component: TestsComponent},
      {path: 'create-test', component: CreateTestComponent},
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
