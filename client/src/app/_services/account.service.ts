import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  //camp privat pentru ca doar acest service sa il poata modifica
  private currentUserSource = new BehaviorSubject<User | null>(null); 
  //public, prin acest observable putem vedea daca si cine este logat
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient) { }

  login(model: any) {
    console.log("account.service login()");
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          console.log("account.service adding/updating user to local storage")
          this.setCurrentUser(user);
        }
      })
    )
    //post returneaza un observable catre loginul din navcomponent
    //RxJS in corpul pipe() putem folosi datele din observable
    //pune in local storage user-ul ca si string(accepta doar perechi de string) 
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user){
         this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout(){
    console.log("account.service logout()");
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
