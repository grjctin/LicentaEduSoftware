import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { NavHomeSharedService } from '../nav-home-shared.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  registerMode=false;
  users: any;
  isLoggedIn: boolean = false;

  constructor(private navHomeService: NavHomeSharedService) { }

  ngOnInit(): void{
    const user = localStorage.getItem('user');
    this.isLoggedIn = !!user;
    this.navHomeService.triggerEvent$.subscribe(
      () => this.isLoggedIn = false
    )
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}
