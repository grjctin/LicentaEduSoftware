import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  //<User | null> union type, poate sa fie user sau null
  //currentUser$: Observable<User | null> = of(null); //of = operator rxjs, observable of something
  

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    //this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    console.log("nav.component login()");
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.log(error);
      }
    })
  }

  logout() {
    console.log("nav.component logout()");
    this.accountService.logout();
  }



}
